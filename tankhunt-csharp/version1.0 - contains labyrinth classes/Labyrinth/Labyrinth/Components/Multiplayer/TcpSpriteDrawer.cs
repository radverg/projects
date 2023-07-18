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
    public class TcpSpriteDrawer : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Labyrinth labyrinth;

        public List<Sprite> sprites = new List<Sprite>();
        const float interpolation = 0.2f;

        public TcpSpriteDrawer(Labyrinth game)
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

        protected override void LoadContent()
        {
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            foreach (Sprite s in sprites)
            {
               // Interpolate(gameTime, s);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Moves local sprite to it's remote position slowly, causing less lag occurrence
        /// </summary>
        /// <param name="s">Sprite to interpolate</param>
        private void Interpolate(GameTime game_time, Sprite s)
        {
            Vector2 difference = s.Remote_position - s.Position;
            if (difference.X < 2 ||difference.Y < 2)
                s.Position = new Vector2(s.Remote_position.X, s.Remote_position.Y); // Jump to remote position immediately
            else
                s.Position += difference * interpolation; // Move sprite slowly towards remote position

            
        }


        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, labyrinth.container.Srl_c.camera.transform);
            foreach (Sprite s in sprites)
            {
              //  s.DrawRotated(labyrinth.spriteBatch);
            }
            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdatePlayer(int id, Vector2 position, float rotation)
        {
            TankPlayerSprite player = ReturnPlayer(id);
            if (player != null)
            {
                player.Remote_position = position;
                player.Rotation = rotation;
            }
            else if (labyrinth.container.Player_tank_c.Player.Net_ID != id)
                AddPlayer(position, id);
        }

        public void AddPlayer(Vector2 position, int id)
        {
            TankPlayerSprite new_tank = new TankPlayerSprite(labyrinth.container.Player_tank_c.tank_sprite, position, new Vector2(40.0f, 40.0f) * SC.resv_ratio, Color.White, -1);
            sprites.Add(new_tank);
            new_tank.Net_ID = id; // Set ID - color will be set automaticly
            new_tank.Origin = new Vector2(new_tank.Texture.Width / 2, new_tank.Texture.Height / 2);           
        }

        /// <summary>
        /// Gets reference to sprite contained in sprite list with specifed id
        /// </summary>
        /// <returns>Sprite reference</returns>
        private TankPlayerSprite ReturnPlayer(int id)
        {
            var player = from j in sprites
                         where ((j is TankPlayerSprite) && (j as TankPlayerSprite).Net_ID == id)
                         select (j as TankPlayerSprite);
            if (player.ToArray().Length >= 1)
               return player.ToArray()[0];

            return null;
           
        }
    }
}
