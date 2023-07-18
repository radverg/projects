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
    public class GameInterfaceComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Labyrinth labyrinth;
        private PlayerComponent player_component;
        private Level level_component;
        private Compass compass;
        private SpriteFont normalfont;

        public GameInterfaceComponent(Labyrinth game, PlayerComponent player, Level level)
            : base(game)
        {
            labyrinth = game;
            player_component = player;
            level_component = level;
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
            compass = new Compass(labyrinth.Content.Load<Texture2D>("Sprites\\arrow"), labyrinth.Content.Load<Texture2D>("Sprites\\compass"));
            normalfont = labyrinth.Content.Load<SpriteFont>("Font1");
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            compass.CountAngle(player_component.Player.Position, level_component.Level_set.Selected_level.Finish.Position);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin();

            compass.DrawCompass(labyrinth.spriteBatch);
            labyrinth.spriteBatch.DrawString(normalfont, MathHelper.ToDegrees((float)compass.arrow_sprite.Rotation).ToString() + "   " + compass.counter.ToString(), new Vector2(300, 10), Color.White);

            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
