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
    public class SimpleRandomLevelComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private TankHunt tankhunt;
        public RandomLevel Level { get; private set; }
        public Camera2D Camera { get; private set; }
        public byte Darkness_coeff { get; set; }

        public bool Follow_camera { get; private set; }
        public bool RotateCamera { get; private set; }
        public Vector2 Max_min_size { get; set; }
        public Vector2 Max_min_square_size { get; set; }

        public SimpleRandomLevelComponent(TankHunt game)
            : base(game)
        {
            tankhunt = game;
            RotateCamera = false;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            Camera = new Camera2D(tankhunt.GraphicsDevice.Viewport);
  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            try
            {
                Wall.Vertical_wall = tankhunt.Content.Load<Texture2D>("Sprites\\vertical_wall_white");
                Wall.Horizontal_wall = tankhunt.Content.Load<Texture2D>("Sprites\\horizontal_wall_white");
            }
            catch (Exception ex)
            {
                tankhunt.KillingExceptionCapture(ex);
            }

            Level = new RandomLevel();
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Level.Walls.Count == 0 && tankhunt.container.Network_c.user_type == NetworkComponent.UserType.ServerUser)
                CreateNewRandomLevel(from p in tankhunt.container.Player_tank_c.Players select p.Color);

           /* if (SC.CheckKeyPressed(Keys.Y, false))
            {
                RotateCamera = !RotateCamera;
                if (RotateCamera)
                    Camera.DrawableField = new Rectangle(0, 0, (int)Math.Sqrt(Camera.view_field.Width ^ 2 + Camera.view_field.Height ^ 2), (int)Math.Sqrt(Camera.view_field.Width ^ 2 + Camera.view_field.Height ^ 2));
                SetCamera();
            }*/

            if (SC.CheckKeyPressed((Keys)SC.KeysAssociation["new"], false) && (tankhunt.container.Network_c.user_type == NetworkComponent.UserType.ServerUser || 
                tankhunt.container.Network_c.user_type == NetworkComponent.UserType.LeadingClient))
            {
                CreateNewRandomLevel(from p in tankhunt.container.Player_tank_c.Players select p.Color);
            }

            if (Follow_camera && tankhunt.container.Player_tank_c.Player.IsAlive) // Set camera position
            {
                Camera.absoulute_pos = tankhunt.container.Player_tank_c.Player.Position - SC.screen_center + tankhunt.container.Player_tank_c.Player.Size / 2;
                Camera.Rotation = -tankhunt.container.Player_tank_c.Player.Rotation;
            }
            else if (!tankhunt.container.Player_tank_c.Player.IsAlive)
            {
                Vector2 delta_camera = new Vector2((float)gameTime.ElapsedGameTime.TotalMilliseconds) * SC.resv_ratio * new Vector2(0.7f);
                if (SC.keystate.IsKeyDown((Keys)SC.KeysAssociation["right"]))
                    Camera.absoulute_pos += new Vector2(1, 0) * delta_camera;

                if (SC.keystate.IsKeyDown((Keys)SC.KeysAssociation["left"]))
                    Camera.absoulute_pos -= new Vector2(1, 0) * delta_camera;

                if (SC.keystate.IsKeyDown((Keys)SC.KeysAssociation["forward"]))
                    Camera.absoulute_pos -= new Vector2(0, 1) * delta_camera;

                if (SC.keystate.IsKeyDown((Keys)SC.KeysAssociation["back"]))
                    Camera.absoulute_pos += new Vector2(0, 1) * delta_camera;
            }
            Camera.Update(RotateCamera);
           
  
            base.Update(gameTime);
        }
        
           

           
        

        public void CreateNewRandomLevel(IEnumerable<Color> players_colors)
        {
            if (tankhunt.container.Player_tank_c.Player.IsAlive && tankhunt.container.Player_tank_c.Players.FindAll((p) => p.IsAlive).Count == 1 && tankhunt.container.Player_tank_c.Players.Count > 1)
                tankhunt.container.Player_tank_c.Player.Wins++;

            Level.CreateRandomLevel(players_colors, Max_min_square_size, Max_min_size);
            Level.Players = tankhunt.container.Player_tank_c.Players;
            Level.Darkness = SC.rnd.Next(1, 101) <= Darkness_coeff;
            tankhunt.container.Darkness_c.Active = Level.Darkness;
            Level.RandomizeDefaultPosition(players_colors.Count(), tankhunt.container.Player_tank_c.Player.Size);
            tankhunt.container.Player_tank_c.Player.Position = Level.Default_position;
            double angle = SC.Random_angle;
            tankhunt.container.Player_tank_c.Player.Rotation = angle;
            tankhunt.container.Player_tank_c.Player.Movement_angle = angle;
            tankhunt.container.Items_c.ResetItems();
            tankhunt.container.Network_c.SendLevel(Level);
            tankhunt.container.Player_tank_c.StartLevelBeginTimer(2000);
            tankhunt.container.Network_c.SendPlayerAllData(tankhunt.container.Player_tank_c.Player);


            SetCamera();
        }



        public void SetExistingLevel(RandomLevel level, double level_creation_time)
        {
            Level = level;
            Level.Players = tankhunt.container.Player_tank_c.Players;
            tankhunt.container.Darkness_c.Active = Level.Darkness;
            tankhunt.container.Player_tank_c.Player.Position = level.GetUsableTiledRndPosition(tankhunt.container.Player_tank_c.Player.Size);
            double angle = SC.Random_angle;
            tankhunt.container.Player_tank_c.Player.Rotation = angle;
            tankhunt.container.Player_tank_c.Player.Movement_angle = angle;
            tankhunt.container.Player_tank_c.StartLevelBeginTimer(2000);
           /* if ((tankhunt.container.Network_c.Server_time.TotalMilliseconds - level_creation_time) < 0)
                tankhunt.container.Player_tank_c.StartLevelBeginTimer(2000);
            else
                tankhunt.container.Player_tank_c.StartLevelBeginTimer(2000 - (tankhunt.container.Network_c.Server_time.TotalMilliseconds - level_creation_time));
            */

            tankhunt.container.Network_c.SendPlayerAllData(tankhunt.container.Player_tank_c.Player);
            tankhunt.container.Items_c.ResetItems();
            

            SetCamera();
        }

        public void SetCamera()
        {
            if ((Level.Size.X * Level.Square_size > SC.screen_rectangle.Width - tankhunt.container.Screen_in_c.Players_panel.Size.X || Level.Size.Y * Level.Square_size > SC.screen_rectangle.Height) || RotateCamera)
            {
                Follow_camera = true;
            }
            else
            {
                Camera.absoulute_pos = new Vector2(-(SC.screen_center.X + (tankhunt.container.Screen_in_c.Players_panel.Size.X / 2) - ((Level.Size.X / 2) * Level.Square_size)), -(SC.screen_center.Y - ((Level.Size.Y / 2) * Level.Square_size)));
                Follow_camera = false;
            }
            SC.Level_rectangle = new Rectangle(0, 0, (int)(Level.Size.X * Level.Square_size), (int)(Level.Size.Y * Level.Square_size));

            Camera.Update(RotateCamera);
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
            tankhunt.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.transform);
            foreach (Sprite s in Level.Walls)
            {
                if (Camera.DrawableField.Intersects(s.Rectangle))
                    s.Draw(tankhunt.spriteBatch);
            }
            tankhunt.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
