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
    public class ExceptionComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Labyrinth labyrinth;
        private Exception exception;
        private string Exception_message { get; set; }
        private GameScreen Sender { get; set; }
        private Texture2D Alert_sprite { get; set; }
        private Rectangle Alert_rectangle { get; set; }

        public ExceptionComponent(Labyrinth game)
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
            base.Initialize();
        }

        public void SetException(Exception ex)
        {
            this.exception = ex;
        }

        protected override void LoadContent()
        {
            Alert_sprite = labyrinth.Content.Load<Texture2D>("Sprites\\alert");
            Alert_rectangle = new Rectangle((int)(SC.screen_center.X - (SC.ChangeX(Alert_sprite.Width) / 2)), 20, (int)SC.ChangeX(Alert_sprite.Width), (int)SC.ChangeY(Alert_sprite.Height));

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

            labyrinth.spriteBatch.Draw(Alert_sprite, Alert_rectangle, Color.White);
            labyrinth.spriteBatch.DrawString(labyrinth.main_font, string.Format("An exception has been caught!\nPress esc to exit application or press enter to try to continue.\nException message:\n {0}\n\n Full exception message:\n {1}",
                exception.Message, exception.ToString()), new Vector2(Alert_rectangle.X + 20, 20), Color.Red);
              

            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
