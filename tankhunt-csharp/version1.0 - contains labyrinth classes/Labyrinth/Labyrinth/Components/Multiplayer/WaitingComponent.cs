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
    public class WaitingComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Labyrinth labyrinth;
        public WaitingComponent(Labyrinth game)
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
            
            base.Initialize();
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
            labyrinth.spriteBatch.DrawString(labyrinth.main_font, "You are connected!\nYou will be allowed to play after they finish current game.", new Vector2(100, 100), Color.Blue);
            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
