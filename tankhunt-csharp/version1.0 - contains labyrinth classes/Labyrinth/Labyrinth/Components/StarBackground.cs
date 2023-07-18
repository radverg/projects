using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
    public class StarBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Labyrinth labyrinth;
        private Texture2D star;
        List<Sprite> stars = new List<Sprite>();
        private int starcount;

        public StarBackground(Labyrinth game)
            : base(game)
        {
            labyrinth = game;
  
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {

            star = labyrinth.Content.Load<Texture2D>(@"Sprites\star");
            Create_stars();
          
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
            foreach (Sprite star in stars)
            {
                labyrinth.spriteBatch.Draw(star.Texture, star.Rectangle, star.Color);
            }
            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
            
        }

        public void Create_stars()
        {
            stars.Clear();

            starcount = (int)(SC.ChangeX(600));
            int size;
            float transparency;
            for (int i = 0; i < starcount; i++)
            {
                size = SC.rnd.Next(4, 10);

                if ((transparency = (float)SC.rnd.NextDouble()) < 0.3f)
                    transparency = 0.3f;

                stars.Add(new Sprite(star, new Vector2(SC.rnd.Next(-300, SC.screen_rectangle.Width + 300),
                    SC.rnd.Next(-300, SC.screen_rectangle.Height + 300)), new Vector2(size, size), Color.White * transparency, -1));
            }

        }
    }
}
