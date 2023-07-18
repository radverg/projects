using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace Labyrinth_editor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Editor : Microsoft.Xna.Framework.Game
    {

       
        // Graphics
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        Settings settings;

        // fps
        public int fps, fps2 = 0;

        // objects
        public Timer fps_timer = new Timer(1000);

        // Fonts
        public SpriteFont main_font;

        // sreens
        GameScreen EditScreen;

        // 2D camera
        public Camera2D camera;

        public Editor()
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
            // TODO: Add your initialization logic here

            // screens & components
            Level component_level = new Level(this);
            EditScreen = new GameScreen(this, new StarBackground(this), component_level, new EditInterface(this, component_level));
            EditScreen.TurnScreenOn(); // turns on components included in editscreen

            settings = new Settings(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labyrinth", "settings") + ".dat");
            settings.LoadSettings();
      

            // sets screen
            Set_screen(settings.Screen_width, settings.Screen_height, settings.Is_full_screen, settings.Show_mouse);

            fps_timer.active = true;

            // folders / files
            SC.appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
          
            

            // set 2d camera
            camera = new Camera2D(GraphicsDevice.Viewport);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            main_font = Content.Load<SpriteFont>("Font1");
           

            
            

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            this.Exit();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
           // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
           //     this.Exit();

            // TODO: Add your update logic here
            fps_timer.gametime = gameTime;

            // keyboared
            SC.previous_keystate = SC.keystate;
            SC.keystate = Keyboard.GetState();


            // mouse
            SC.previous_mousestate = SC.mousestate;
            SC.mousestate = Mouse.GetState();



     
            // end?
            if (SC.keystate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                UnloadContent();

            //  fullscreen
            if (SC.keystate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F11) && SC.previous_keystate.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F11))
                graphics.ToggleFullScreen();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

           
            base.Draw(gameTime);
            // TODO: Add your drawing code here

            spriteBatch.Begin();
            


            spriteBatch.End();
            if (fps_timer.Check_tick())
            {
                fps = fps2;
                fps2 = 0;
            }

            fps2 += 1;


           
        }

        public void Set_screen(int width, int height, bool fullscreen, bool mouse)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = (int)((width / 16.0f) * 9);
            graphics.IsFullScreen = fullscreen;
            IsMouseVisible = mouse;
            graphics.ApplyChanges();

            SC.screen_rectangle = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            SC.screen_center = new Vector2(SC.screen_rectangle.Width / 2, SC.screen_rectangle.Height / 2);
            SC.res_ratio = SC.screen_rectangle.Width / 1920.0f;
            SC.square_size = (int)(SC.ChangeX(80));  
        }

       
       
    }
}
