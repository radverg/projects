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
    public class SimpleRandomLevelComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Labyrinth labyrinth;
        public RandomLevel Level { get; private set; }
        public Camera2D camera;
        public SimpleRandomLevelComponent(Labyrinth game)
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
            camera = new Camera2D(labyrinth.GraphicsDevice.Viewport);
  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            try
            {
                RandomLevel.Vertical_wall = labyrinth.Content.Load<Texture2D>("Sprites\\wall_vertical");
                RandomLevel.Horizontal_wall = labyrinth.Content.Load<Texture2D>("Sprites\\wall_horizontal");
            }
            catch (Exception ex)
            {
                labyrinth.KillingExceptionCapture(ex);
            }

            Level = new RandomLevel(false);
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Level.Walls.Count == 0 && labyrinth.container.Network_c.user_type == NetworkComponent.UserType.ServerUser)
                CreateNewRandomLevel();
            // TODO: Add your update code here
            if (SC.keystate.IsKeyDown(Keys.Space) && SC.previous_keystate.IsKeyUp(Keys.Space) && labyrinth.container.Network_c.user_type == NetworkComponent.UserType.ServerUser)
            {
                CreateNewRandomLevel();

            }
  
            base.Update(gameTime);
        }
        
           

           
        

        public void CreateNewRandomLevel()
        {
            Level.CreateRandomLevel();
            labyrinth.container.Player_tank_c.Player.Position = GenerateRandomPosition(labyrinth.container.Player_tank_c.Player.Size);
            double angle = SC.Random_angle;
            labyrinth.container.Player_tank_c.Player.Rotation = angle;
            labyrinth.container.Player_tank_c.Player.Movement_angle = angle;
            labyrinth.container.Network_c.SendPlayerAllData(labyrinth.container.Player_tank_c.Player);
            labyrinth.container.Items_c.ResetItems();
            labyrinth.container.Network_c.SendLevel(Level);
            SetCamera();
        }



        public void SetExistingLevel(int square_size, Vector2 size, List<Sprite> walls)
        {
            Level.SetExistingLevel(square_size, size, walls);
            labyrinth.container.Player_tank_c.Player.Position = GenerateRandomPosition(labyrinth.container.Player_tank_c.Player.Size);
            double angle = SC.Random_angle;
            labyrinth.container.Player_tank_c.Player.Rotation = angle;
            labyrinth.container.Player_tank_c.Player.Movement_angle = angle;
            labyrinth.container.Items_c.ResetItems();

            SetCamera();
        }

        public void SetCamera()
        {
            camera.absoulute_pos = new Vector2(-(SC.screen_center.X - ((Level.Size.X / 2) * Level.Square_size)), -(SC.screen_center.Y - ((Level.Size.Y / 2) * Level.Square_size)));
            SC.Camera_screen_rectangle = new Rectangle((int)camera.absoulute_pos.X, (int)camera.absoulute_pos.Y, SC.screen_rectangle.Width, SC.screen_rectangle.Height);
            camera.Update();
        }

        public Vector2 GenerateRandomPosition(Vector2 generated_item_size)
        {
            int position_x, position_y;
            bool done = true;
            do
            {
                done = true;
                position_x = SC.rnd.Next(0, (int)Level.Size.X * Level.Square_size);
                position_y = SC.rnd.Next(0, (int)Level.Size.Y * Level.Square_size);
                foreach (Sprite s in Level.Walls)
	            {
                    if (s.Rectangle.Intersects(new Rectangle(position_x, position_y, (int)generated_item_size.X, (int)generated_item_size.Y)))
                        done = false;		 
	            }
            }
            while (!done);

            return new Vector2(position_x, position_y);
        }
        public override void Draw(GameTime gameTime)
        {
            labyrinth.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            foreach (Sprite s in Level.Walls)
            {
                s.Draw(labyrinth.spriteBatch);
            }
            labyrinth.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
