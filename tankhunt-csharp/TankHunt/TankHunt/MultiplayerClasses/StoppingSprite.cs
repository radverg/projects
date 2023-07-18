using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class StoppingSprite : Sprite
    {
        public bool Go_ahead { get; set; }
        public bool Even_up { get; set; }
        private readonly float Max_evenup_angle = 30;
        private double movement_angle;
        private float[] angles = new float[] { 0, MathHelper.PiOver2, MathHelper.Pi, MathHelper.PiOver2 * 3 }; //new float[] { MathHelper.PiOver4, MathHelper.PiOver4 * 3, MathHelper.PiOver4 * 5, MathHelper.PiOver4 * 7 };
        private Vector2[] directs = new Vector2[] { new Vector2(1, -1), new Vector2(1, 1), new Vector2(-1, 1), new Vector2(-1, -1) };

        public double Movement_angle
        {
            get
            {
                return movement_angle;
            }
            set
            {
                if (value > MathHelper.TwoPi)
                   movement_angle = value - MathHelper.TwoPi;
                else if (value < 0)
                    movement_angle = value + MathHelper.TwoPi;
                else
                    movement_angle = value;

                Velocity_coefficient = new Vector2((float)Math.Sin(movement_angle), -(float)Math.Cos(movement_angle));
            }
        }
        public StoppingSprite(Texture2D texture, Vector2 position, Vector2 size, Color color, int writenumber): base(texture, position, size, color)        
        {
            Go_ahead = true;
        }

        public override void Move(GameTime gameTime) // Adjusted method - can move sprite backwards if ahead is false
        {
            if (Go_ahead)
                base.Move(gameTime); // Move sprite forward
            else
            {
                Position = new Vector2((float)(Position.X - Velocity.X * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio), // Move sprite backwards
               (float)(Position.Y - Velocity.Y * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio)); 
            }
        }

        public bool NextPositionIntersect(GameTime game_time, RandomLevel level, Color except)
        {
            Rectangle next_rectangle;
            if (Go_ahead)
            {
                next_rectangle = new Rectangle((int)((float)(Position.X + Velocity.X * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio)),
                    (int)((float)(Position.Y + Velocity.Y * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio)),
                    Rectangle.Width, Rectangle.Height);
            }
            else
            {
                next_rectangle = new Rectangle((int)(Position.X - Velocity.X * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                   (int)(Position.Y - Velocity.Y * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                   Rectangle.Width, Rectangle.Height);
            }

            List<Wall> Intersected_walls = new List<Wall>();
            foreach (Wall w in level.Walls)
            {
                if (w.Rectangle.Intersects(next_rectangle))
                    Intersected_walls.Add(w);
            }

            foreach (Wall w in Intersected_walls)
            {
                if (SC.CircleIntersectsRectangle(w.Rectangle, next_rectangle) && except != w.Color)
                {
                    if (Even_up)
                    {
                        if (Go_ahead)
                            EvenUpSprite(w, game_time, (float)base.Rotation, next_rectangle);
                        else
                            EvenUpSprite(w, game_time, (float)base.Rotation + MathHelper.Pi, next_rectangle);
                    }
                    return true;
                }
            }
            return false;
        }

        private void EvenUpSprite(Sprite wall, GameTime time, float rot, Rectangle next)
        {
            if (SC.keystate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) || SC.keystate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                return;

            float Rotation = SC.ClampAngle(rot);
            double AddRotation = 0;
            bool is_horizontal = wall.Size.X > wall.Size.Y;
            double move = Velocity_const.X * time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio;

            List<double> allowed_angles = new List<double>();

            for (int i = 0; i < angles.Length; i++)
			{
                Vector2 temp_pos = new Vector2(Position.X + ((float)(SC.GetVelCoef(angles[i]).X * move)), Position.Y + ((float)(SC.GetVelCoef(angles[i]).Y * move)));
                Rectangle temp_rec = new Rectangle((int)temp_pos.X, (int)temp_pos.Y, Rectangle.Width, Rectangle.Height);
                while (temp_rec == Rectangle)
                {
                    temp_pos += new Vector2(((float)(SC.GetVelCoef(angles[i]).X * move)), ((float)(SC.GetVelCoef(angles[i]).Y * move)));
                    temp_rec = new Rectangle((int)temp_pos.X, (int)temp_pos.Y, Rectangle.Width, Rectangle.Height);
                }
                
                if (!SC.CircleIntersectsRectangle(wall.Rectangle, temp_rec))
                    allowed_angles.Add(angles[i]);			 
			}

            if (allowed_angles.Count == 0)
                return;

            double the_best_angle_to_rotate = allowed_angles[0];
      
            foreach (double ang in allowed_angles)
            {
                if (DeltaAngle(the_best_angle_to_rotate, Rotation) > DeltaAngle(ang, Rotation))
                    the_best_angle_to_rotate = ang;
            }

            AddRotation = Rotation_velocity;
            double delta = DeltaAngle(the_best_angle_to_rotate, Rotation);
             if (SC.ClampAngle((float)(Rotation - delta)) == the_best_angle_to_rotate)
                AddRotation = -Rotation_velocity;

            base.Rotation += AddRotation * time.ElapsedGameTime.TotalMilliseconds;
            Movement_angle = base.Rotation;

        }

        private double DeltaAngle(double ang1, double ang2)
        {
            return Math.Min(Math.Abs(ang1 - ang2), Math.Abs(MathHelper.TwoPi - Math.Abs(ang1 - ang2)));
        }



    }
}
