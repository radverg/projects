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


namespace Labyrinth
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScreenInterfaceComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Labyrinth labyrinth;
        private Sprite players_panel;
        private SpriteFont font;
        private Vector2[][] positions = new Vector2[8][];

        public ScreenInterfaceComponent(Labyrinth game)
            : base(game)
        {
            labyrinth = game;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {

            try
            {
                Texture2D player_panel_texture = labyrinth.Content.Load<Texture2D>("Sprites\\Panels\\panel_players");
                players_panel = new Sprite(player_panel_texture, Vector2.Zero, new Vector2(player_panel_texture.Width + 30, 1080) * SC.resv_ratio);
                font = labyrinth.Content.Load<SpriteFont>("Fonts\\SmallFont");
                MessageLog.Message_font = labyrinth.Content.Load<SpriteFont>("Fonts\\LogFont");

            }
            catch (Exception ex)
            {
                labyrinth.KillingExceptionCapture(ex);
            }
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin();
            players_panel.Draw(labyrinth.spriteBatch);
            byte index = 0;
            foreach (TankPlayerSprite player in labyrinth.container.Player_tank_c.Players)
            {
                labyrinth.spriteBatch.Draw(player.Texture, new Rectangle((int)positions[index][0].X, (int)positions[index][0].Y, (int)player.Size.X, (int)player.Size.Y), player.Color);
                labyrinth.spriteBatch.DrawString(font, string.Format("{0}\nWins: {1}\nKills: {2}\nDeaths: {3}", player.Player_name, player.Wins, player.Kills, player.Deaths), positions[index][1], Color.Black);

                index++;    
            }

            // Draw messages
            foreach (MessageLog mes in MessageLog.Messages)
            {
                mes.DrawMessage(labyrinth.spriteBatch);
            }

            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }

       
    }
}
