using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Labyrinth_editor
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Level : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /// <summary>
        /// Instance of the main program
        /// </summary>
        private Editor editor;
        /// <summary>
        /// Map being edited
        /// </summary>
        public Map Map { get; set; }
        /// <summary>
        /// 2D camera
        /// </summary>
        public Camera2D Camera { get; set; }

        public LevelSet Level_set { get; set; }

        public List<Texture2D> Level_sprites { get; set; }

        public Level(Editor game)
            : base(game)
        {
            editor = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            
            Level_sprites = new List<Texture2D>();

            // Set 2D camera
            Camera = new Camera2D(editor.GraphicsDevice.Viewport);
            Camera.absoulute_pos = Vector2.Zero;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            try
            {
                Level_sprites.Add(null);
                Level_sprites.Add(editor.Content.Load<Texture2D>("Sprites//human"));
                Level_sprites.Add(editor.Content.Load<Texture2D>("Sprites//basicwall"));
                Level_sprites.Add(editor.Content.Load<Texture2D>("Sprites//green_wall"));
                Level_sprites.Add(editor.Content.Load<Texture2D>("Sprites//grass"));
                Level_sprites.Add(editor.Content.Load<Texture2D>("Sprites//finish"));
            }
            catch
            {

            }

            using (StreamReader rd = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labyrinth", "temporary") + ".dat"))
            {
                string name_set = rd.ReadLine();
                if (name_set == "none")
                    editor.Exit();
                Level_set = new LevelSet(name_set, Level_sprites, rd.ReadLine());
               
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
            // Move scene when mouse is on edge
            if (SC.keystate.IsKeyDown(Keys.Left))
                Camera.absoulute_pos.X = MathHelper.Clamp(Camera.absoulute_pos.X - 30, 0, (SC.square_size * Level_set.Selected_level.Size.X) - SC.screen_rectangle.Width);

            if (SC.keystate.IsKeyDown(Keys.Right))
                Camera.absoulute_pos.X = MathHelper.Clamp(Camera.absoulute_pos.X + 30, 0, (SC.square_size * Level_set.Selected_level.Size.X) - SC.screen_rectangle.Width);


            if (SC.keystate.IsKeyDown(Keys.Up))
                Camera.absoulute_pos.Y = MathHelper.Clamp(Camera.absoulute_pos.Y - 30, 0, (SC.square_size * Level_set.Selected_level.Size.Y) - SC.screen_rectangle.Height);


            if (SC.keystate.IsKeyDown(Keys.Down))
                Camera.absoulute_pos.Y = MathHelper.Clamp(Camera.absoulute_pos.Y + 30, 0, (SC.square_size * Level_set.Selected_level.Size.Y) - SC.screen_rectangle.Height);


            Camera.Update(); // update camera position
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            editor.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.transform);

            for (int X = 0; X < Level_set.Selected_level.Field.GetLength(0); X++)
            {
                for (int Y = 0; Y < Level_set.Selected_level.Field.GetLength(1); Y++)
                {
                    if (Level_set.Selected_level.Field[X, Y].Texture != null && Level_set.Selected_level.Field[X, Y].Rectangle.Intersects(new Rectangle((int)Camera.absoulute_pos.X, (int)Camera.absoulute_pos.Y, SC.screen_rectangle.Width, SC.screen_rectangle.Height)))
                    {
                        Level_set.Selected_level.Field[X, Y].Draw(editor.spriteBatch);
                    }
                     
                }
            }

            editor.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
