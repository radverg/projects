using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;
using System.Net;

namespace Labyrinth
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Labyrinth : Microsoft.Xna.Framework.Game
    {
        // Graphics
        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private Settings settings;
        private Color background_color = new Color(244, 248, 251);

        // fps
        public int fps, fps2 = 0;

        // objects
        public Timer fps_timer = new Timer(1000);
        public Container container;
        // Fonts
        public SpriteFont main_font;

        // sreens
        public GameScreen PlayingScreen, PauseScreen, PlayingMScreen, MenuMScreen1, DisconnectScreen, WaitingScreen, ExceptionScreen;

        private Form GameWindowForm;

        // 2D camera
        public Camera2D camera;

        public Labyrinth()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            try
            {
               // CheckForUpdates();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

            GameWindowForm = (Form)Form.FromHandle(Window.Handle);
          
            /*
            try // Load settings
            {
                settings = new Settings(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labyrinth", "settings") + ".dat");
                settings.LoadSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
            */
            Exiting += new EventHandler<EventArgs>(GameExitingEventMethod);

            Set_screen(1280, 720, false, true); // Sets screen

            /* Set playing screen
             Level level_component = new Level(this);
            PlayerComponent player_component = new PlayerComponent(this, level_component);
            EnemiesComponent enemy_component = new EnemiesComponent(this, level_component, player_component);
            PlayingScreen = new GameScreen(this, new StarBackground(this), level_component, player_component, new GameInterfaceComponent(this, player_component, level_component), enemy_component);
             */


            // Loades all multiplayer components and saves them to the container ----------------------------------------------
            container = new Container();
            container.Labyrinth = this;
            container.Srl_c = new SimpleRandomLevelComponent(this);
            container.Player_tank_c = new PlayerTankComponent(this, container.Srl_c);
            container.Network_c = new NetworkComponent(this);
            container.Tcp_sprite_drawer_c = new TcpSpriteDrawer(this);
            container.Disconnect_c = new DisconnectComponent(this);
            container.Mshots_c = new MShotsComponent(this);
            container.Exception_c = new ExceptionComponent(this);
            container.Items_c = new ItemsComponent(this);
            container.Screen_in_c = new ScreenInterfaceComponent(this);
            // -----------------------------------------------------------------------------------------------------------------
            
            // Creates multiplayer screens -------------------------------------------------------------------------------------
            PlayingMScreen = new GameScreen(this, container.Network_c, container.Srl_c, container.Tcp_sprite_drawer_c, container.Mshots_c, container.Items_c, container.Player_tank_c, container.Screen_in_c);
            DisconnectScreen = new GameScreen(this, container.Disconnect_c);
            WaitingScreen = new GameScreen(this, container.Network_c, new WaitingComponent(this));
            ExceptionScreen = new GameScreen(this, container.Exception_c);
            // -----------------------------------------------------------------------------------------------------------------

            foreach (GameComponent comp in Components) // Turn off all components
            {
                comp.Enabled = false;
                if (comp is DrawableGameComponent)
                    ((DrawableGameComponent)comp).Visible = false;
            }
            SwitchScreen(PlayingMScreen, true); // Turn on multiplayer menu screen

            // Run fps timer
            fps_timer.Start();

            base.Initialize();
        }

        private void GameExitingEventMethod(object sender, EventArgs e)
        {
            container.Network_c.Disconnect();
        }


        private void CheckForUpdates()
        {
            double current_version = double.Parse(Assembly.GetExecutingAssembly().GetName().Version.ToString(2), CultureInfo.InvariantCulture);
            double last_version;
            string net_file_path;

            // Download info from net
            WebRequest wr = WebRequest.Create("http://tankhunt.wz.cz/update_info.txt");
            WebResponse resp = wr.GetResponse();

            using (StreamReader reader = new StreamReader(resp.GetResponseStream())) 
            {
                last_version = double.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                net_file_path = reader.ReadLine();
            }

            if (last_version > current_version)
            {
                if (MessageBox.Show("New version is available! \nDo you wish to update automatically now?", "Update available!", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ProcessStartInfo p_info = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "Updater.exe");
                    p_info.Arguments = net_file_path;
                    System.Diagnostics.Process.Start(p_info);
                    
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            try
            {
                main_font = Content.Load<SpriteFont>("Fonts\\Font1");
                base.LoadContent();
            }
            catch (Exception e)
            {
                KillingExceptionCapture(e);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
               //  Not defined yet         
        }
        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (SC.Game_paused)
                return;
          
            fps_timer.Update(gameTime);

            // Update keyboard state and mouse state ----------------------------------------------------
            SC.previous_keystate = SC.keystate;
            SC.keystate = Keyboard.GetState();
               // 
            SC.previous_mousestate = SC.mousestate;
            SC.mousestate = Mouse.GetState();
            // ------------------------------------------------------------------------------------------

            if (SC.keystate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape)) // Allows game to exit by pressing esc key
                UnloadContent();

            if (SC.keystate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F11) && SC.previous_keystate.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F11)) // Toggles fulscreen if F11 key was pressed
                graphics.ToggleFullScreen();

           
            base.Update(gameTime); // Update components
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background_color); // Clear everthing on screen to background color
            if (SC.Game_paused)
                return;

 
            spriteBatch.Begin();
            spriteBatch.DrawString(main_font, fps.ToString(), Vector2.Zero, Color.Black); // Draw fps to the corner
            spriteBatch.End();
            if (fps_timer.IsTicked)
            {
                fps = fps2;
                fps2 = 0;
            }

            fps2 += 1;

            base.Draw(gameTime); // Draw components
        }

        public void Set_screen(int width, int height, bool fullscreen, bool mouse)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = (int)((width / 16.0f) * 9);
            graphics.IsFullScreen = false;
            IsMouseVisible = mouse;
            graphics.ApplyChanges();

            SC.screen_rectangle = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            SC.screen_center = new Vector2(SC.screen_rectangle.Width / 2, SC.screen_rectangle.Height / 2);
            SC.res_ratio = SC.screen_rectangle.Width / 1920.0f;
            SC.resv_ratio = new Vector2(SC.res_ratio, SC.res_ratio);
            SC.square_size = (int)(SC.ChangeX(80));
            
        }

        public void SwitchScreen(GameScreen screen, bool turn)
        {
            GameComponent[] scomponents = screen.ReturnComponents();
            foreach (GameComponent comp in Components)
            {
                if (scomponents.Contains(comp))
                {
                    comp.Enabled = turn;
                    if (comp is DrawableGameComponent)
                        ((DrawableGameComponent)comp).Visible = turn;
                }
                else
                {
                    comp.Enabled = false;
                    if (comp is DrawableGameComponent)
                        ((DrawableGameComponent)comp).Visible = false;
                }
            }
        }

        public void ExceptionCapture(Exception ex)
        {
            container.Exception_c.SetException(ex);
            SwitchScreen(ExceptionScreen, true);
        }

        /// <summary>
        /// Turns off fullscreen and shows message box with exception message. Then exits the game.
        /// </summary>
        /// <param name="ex">exception</param>
        public void KillingExceptionCapture(Exception ex)
        {
            if (graphics.IsFullScreen)
                graphics.ToggleFullScreen();

            MessageBox.Show(string.Format("An exception has been caught! The game is unable to continue and will be aborted! \n{0}\n\n{1}", ex.Message, ex.ToString()),
                "Error", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

            Environment.Exit(0); // Terminate process
        }

        /// <summary>
        /// This method toggles fullscreen to false if necessary, shows connection form, pauzes game loop and waits for user input, if dialog is not confirmed, game will be aborted. If dialog is confirmed,
        /// method will return input stored in ConnectionInfo class
        /// </summary>
        /// <param name="con_info">ConnectionInfo instance that should be modified. Set null for new instance.</param>
        /// <returns>ConnectionInfo instance with values from ConnectionForm</returns>
        public ConnectionInfo ShowConnectionScreen(ConnectionInfo con_info)
        {
            SC.Game_paused = true;
            if (graphics.IsFullScreen)
                graphics.ToggleFullScreen();
            ConnectionInfo connect_info = con_info;
            if (connect_info == null)
               connect_info = new ConnectionInfo();
            ConnectionForm form = new ConnectionForm(connect_info);
            GameWindowForm.Hide();
            if (form.ShowDialog() != DialogResult.OK)
            {
                Exit();
                return null;
            }
            GameWindowForm.Show();
            SC.Game_paused = false;
            return connect_info;
        }

       
    }
}
