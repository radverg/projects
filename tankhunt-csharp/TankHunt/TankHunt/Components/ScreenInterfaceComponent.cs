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


namespace TankHunt
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScreenInterfaceComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private TankHunt tankhunt;
        public Sprite Players_panel { get; private set; }
        private SpriteFont font, mega_timer_font;
        private Vector2[][] positions = new Vector2[8][];
        private Vector2 In_cool_time_pos;
        private string In_cool_string;
        private Color In_cool_color;

        private Song backgroundSong;
        private Timer songStartTimer = new Timer(60000);

        private SimpleTextBox Chat_box;

        private TextSprite fps_text, ping_text, invisibility_text, counter_text, observer_text;

        private Texture2D cross_texture;

        public ScreenInterfaceComponent(TankHunt game)
            : base(game)
        {
            tankhunt = game;
        }

        void Darkness_c_DarknessActiveChanged(object sender, EventArgs e)
        {
            // Change texts colors
            Color new_color = tankhunt.container.Darkness_c.Preferred_text_color;
            
            Chat_box.Text.Color = new_color;
            Chat_box.Name.Color = new_color;

            foreach (MessageLog mess in MessageLog.Messages)
            {
                mess.Message.Color = new_color;
            }

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            Vector2 basic_position = new Vector2(10, 90) * SC.resv_ratio;
            positions[0] = new Vector2[] { basic_position, basic_position + new Vector2(50, 0) * SC.resv_ratio };
            for (int i = 1; i < positions.Length; i++)
            {
                positions[i] = new Vector2[] { new Vector2(10, basic_position.Y + (i * 150)) * SC.resv_ratio, new Vector2(60, basic_position.Y + i * 150) * SC.resv_ratio };
            }
            tankhunt.container.Darkness_c.DarknessActiveChanged += new EventHandler(Darkness_c_DarknessActiveChanged);
          
            base.Initialize();
        }

        protected override void LoadContent()
        {

            try
            {
                Texture2D player_panel_texture = tankhunt.Content.Load<Texture2D>("Sprites\\Panels\\panel_players");
                Players_panel = new Sprite(player_panel_texture, Vector2.Zero, new Vector2(player_panel_texture.Width + 30, 1080) * SC.resv_ratio);

                font = tankhunt.Content.Load<SpriteFont>("Fonts\\SmallFont");
                MessageLog.Message_font = tankhunt.Content.Load<SpriteFont>("Fonts\\LogFont");
                mega_timer_font = tankhunt.Content.Load<SpriteFont>("Fonts\\MegaTimerFont");
                cross_texture = tankhunt.Content.Load<Texture2D>("Sprites\\cross");
                observer_text = new TextSprite("Observer mode!", font, Color.Green, new Vector2(Players_panel.Size.X + 20, 100 * SC.res_ratio), 500);
                In_cool_time_pos = new Vector2(Players_panel.Size.X + 20, 20);
                backgroundSong = tankhunt.Content.Load<Song>("Sounds\\tankhuntmusic");
           
            }
            catch (Exception ex)
            {
                tankhunt.KillingExceptionCapture(ex);
            }

            Chat_box = new SimpleTextBox(new Vector2(Players_panel.Size.X + (20 * SC.res_ratio), SC.screen_rectangle.Height - (60 * SC.res_ratio)), font, "Chat: ", (int)(SC.screen_rectangle.Width - Players_panel.Size.X + (20 * SC.res_ratio) - 60 * SC.res_ratio));
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!songStartTimer.Running)
                songStartTimer.Start();
            songStartTimer.Update(gameTime);
            // TODO: Add your update code here
            if (!tankhunt.container.Player_tank_c.Player.Invisibility_cooldown_timer.Running)
            {
                In_cool_string = "Invisibility ready!";
                if (tankhunt.container.Player_tank_c.Player.Invisibility_timer.Running)
                   In_cool_string = "Invisibility being used!";
            }
            else
            {
                In_cool_string = string.Format("Invisibility available in: {0} ms ", ((int)(tankhunt.container.Player_tank_c.Player.Invisibility_cooldown_time - tankhunt.container.Player_tank_c.Player.Invisibility_cooldown_timer.Temporary_time)).ToString());
            }

            if (tankhunt.container.Darkness_c.Active)
                In_cool_color = Color.White;
            else
                In_cool_color = Color.Black;

            // Observer interface -------------------------------------
            if (tankhunt.container.Player_tank_c.Player.Observer)
            {
                if (tankhunt.container.Player_tank_c.Player.Keep_observing)
                    observer_text.Text = "Observer mode! \nYou're going to stay as observer in the next level (press P to change).";
                else
                    observer_text.Text = "Observer mode! \nYou're going to play in the next level (press P to change).";
                if (SC.CheckKeyPressed((Keys)SC.KeysAssociation["pause"], false))
                    tankhunt.container.Player_tank_c.Player.Keep_observing = !tankhunt.container.Player_tank_c.Player.Keep_observing;
            }

            if (SC.CheckKeyPressed((Keys)SC.KeysAssociation["pause"], false) && !tankhunt.container.Player_tank_c.Player.Observer && tankhunt.container.Player_tank_c.Player.Controlable)
            {
                tankhunt.container.Player_tank_c.Die(-1);
                tankhunt.container.Player_tank_c.BecomeObs(true);
            }


            // --------------------------------------------------------

            // Chat ---------------------------------------------------
            if (SC.CheckKeyPressed((Keys)SC.KeysAssociation["chat"], true))
            {
                if (Chat_box.Activated && Chat_box.Text.Text.Length > 0)
                {
                    if (Chat_box.Text.Text[0] == '-')
                    {
                        new Query(Chat_box.Text.Text, tankhunt, tankhunt.container.Network_c.user_type, tankhunt.container.Player_tank_c.Player);
                    }
                    else
                    {
                        Chat_box.Text.Text = string.Format("[{0}] {1}", tankhunt.container.Player_tank_c.Player.Player_name, Chat_box.Text.Text);
                        MessageLog.CreateMessage(Chat_box.Text.Text);
                    }
                    tankhunt.container.Network_c.SendChatMessage(Chat_box.Text.Text, tankhunt.container.Player_tank_c.Player); // Send over network
                    Chat_box.Text.Text = "";
                }

                Chat_box.Activated = !Chat_box.Activated;
                SC.Keyboard_lock = Chat_box.Activated;
            }

            if (Chat_box.Activated)
                Chat_box.Update();
            // -------------------------------------------------------

            // Update messages
            MessageLog.Messages.RemoveAll((m) => m.Delete);
   
            foreach (MessageLog mess in MessageLog.Messages)
            {
                mess.UpdateMessage(gameTime, tankhunt.container.Darkness_c.Preferred_text_color);
            }

            // ------------------


            // Disconnect or abort chat if key esc was pressed
            if (SC.CheckKeyPressed(Keys.Escape, true))
            {
                if (Chat_box.Activated)
                {
                    Chat_box.Activated = false;
                    SC.Keyboard_lock = false;
                }
                else
                    tankhunt.container.Network_c.Disconnect();
            }

            // Run song
            if (songStartTimer.IsTicked)
            {
                songStartTimer.Stop();
                songStartTimer.Interval = SC.rnd.Next(450000, 1000000);
                songStartTimer.Start();
                try
                {
                    MediaPlayer.Play(backgroundSong);
                }
                catch
                {
                    songStartTimer.Stop();
                    MessageLog.CreateMessage("Can't play song, something is wrong (are you missing win media player?)");
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            tankhunt.spriteBatch.Begin();
            Players_panel.Draw(tankhunt.spriteBatch);
            byte index = 0;
          
            foreach (TankPlayerSprite player in tankhunt.container.Player_tank_c.Players)
            {               
                tankhunt.spriteBatch.Draw(player.Texture, new Rectangle((int)positions[index][0].X, (int)positions[index][0].Y, (int)player.Size.X, (int)player.Size.Y), player.Color);
                tankhunt.spriteBatch.DrawString(font, string.Format("{0} ({1})\nWins: {2}\nKills: {3}\nDeaths: {4}", player.Player_name, player.Net_ID, player.Wins, player.Kills, player.Deaths), positions[index][1], Color.Black, 0, Vector2.Zero, SC.res_ratio, SpriteEffects.None, 0);
                tankhunt.spriteBatch.DrawString(font, In_cool_string,
                    In_cool_time_pos, In_cool_color);

                if (!player.IsAlive)
                {
                    tankhunt.spriteBatch.Draw(cross_texture, new Rectangle((int)positions[index][0].X + (int)(30 * SC.res_ratio), (int)positions[index][0].Y - (int)(20 * SC.res_ratio), 100, 100), Color.Red * 0.4f);
                }
              
                index++;    
            }

            tankhunt.spriteBatch.DrawString(font, "Ping: " + tankhunt.container.Network_c.Current_ping.ToString() + " ms", new Vector2(10, SC.screen_rectangle.Height - 30), Color.Black, 0, Vector2.Zero, SC.res_ratio, SpriteEffects.None, 0);
            tankhunt.spriteBatch.DrawString(font, "Fps: " + tankhunt.fps.ToString(), new Vector2(10, SC.screen_rectangle.Height - 60), Color.Black, 0, Vector2.Zero, SC.res_ratio, SpriteEffects.None, 0);


            if (tankhunt.container.Player_tank_c.Level_begin_timer.Running)
            {
                double remaining_time = Math.Ceiling(((tankhunt.container.Player_tank_c.Level_begin_timer.Interval - tankhunt.container.Player_tank_c.Level_begin_timer.Temporary_time) * 2) / 1000);
                tankhunt.spriteBatch.DrawString(mega_timer_font, remaining_time.ToString(), SC.screen_center - mega_timer_font.MeasureString(remaining_time.ToString()) / 2, Color.Navy);
            }

            if (tankhunt.container.Player_tank_c.Player.Observer)
            {
                observer_text.Draw(tankhunt.spriteBatch);
            }

            // Draw messages
            foreach (MessageLog mes in MessageLog.Messages)
            {
                mes.DrawMessage(tankhunt.spriteBatch);
            }

            // Draw chat box ----------------------------------
            if (Chat_box.Activated)
                Chat_box.Draw(tankhunt.spriteBatch);

            tankhunt.spriteBatch.End();
            base.Draw(gameTime);
        }

       
    }
}
