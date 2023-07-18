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
    public class PlayerComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Labyrinth labyrinth;
        public BouncingSprite Player { get; set; }
        Level Level_component { get; set; }
        
        public PlayerComponent(Labyrinth game, Level level_component)
            : base(game)
        {
            labyrinth = game;
            Level_component = level_component;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (Sprite s in Level_component.Level_set.Selected_level.Field)
            {
                if (s.Writenumber == 1) // Look for player block
                {
                    Player = new BouncingSprite(s.Texture, new Vector2(s.Rectangle.X + 10, s.Rectangle.Y + 10),
                        new Vector2(50 * SC.res_ratio, 50 * SC.res_ratio), Color.White, 1, new Vector2(0.2f, 0.2f), Level_component);
                    Player.Origin = new Vector2(Player.Size.X / 2, Player.Size.Y / 2);
                }
            }          
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {          
            Player.SetBounces();
            Player.CheckBounces();

            if (SC.mousestate.RightButton == ButtonState.Pressed && SC.previous_mousestate.RightButton == ButtonState.Released)
                Player.ChangeDirection(); // Changes direction of player movement if right button is pressed

            if (SC.keystate.IsKeyDown(Keys.C))
            {
               // Player.Velocity_const = 0.1;
            }

            // rotate player
            if (Player.Velocity_coefficient.X < 0 && Player.Velocity_coefficient.Y < 0.5f && Player.Velocity_coefficient.Y > -0.5f)
                Player.Rotation = MathHelper.PiOver2 * 3;
            if (Player.Velocity_coefficient.X > 0 && Player.Velocity_coefficient.Y < 0.5f && Player.Velocity_coefficient.Y > -0.5f)
                Player.Rotation = MathHelper.PiOver2;
            if (Player.Velocity_coefficient.Y < 0 && Player.Velocity_coefficient.X < 0.5f && Player.Velocity_coefficient.X > -0.5f)
                Player.Rotation = 0;
            if (Player.Velocity_coefficient.Y > 0 && Player.Velocity_coefficient.X < 0.5f && Player.Velocity_coefficient.X > -0.5f)
                Player.Rotation = MathHelper.Pi;

            Player.Move(gameTime);

            // Move camera
            Level_component.Camera.absoulute_pos = new Vector2(SC.GetCenter(Player.Rectangle).X - SC.screen_rectangle.Width / 2, SC.GetCenter(Player.Rectangle).Y - SC.screen_rectangle.Height / 2);
      
            base.Update(gameTime);
        }

       

        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin();            
            Player.DrawToCenter(labyrinth.spriteBatch);

            for (int i = 0; i < 4; i++) // Draw block positions
                labyrinth.spriteBatch.DrawString(labyrinth.main_font, Player.Block_positions[i].ToString(), new Vector2(100, 10 + (i * 100)), Color.WhiteSmoke);

         
            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }

       
    }
}
