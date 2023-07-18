using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class StoppingSprite : Sprite
    {
        public bool Go_ahead { get; set; }
        private double movement_angle;
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
        public StoppingSprite(Texture2D texture, Vector2 position, Vector2 size, Color color, int writenumber): base(texture, position, size, color, writenumber)        
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

        public bool NextPositionIntersect(GameTime game_time, List<Sprite> walls)
        {
            Rectangle next_rectangle;
            if (Go_ahead)
            {
                next_rectangle = new Rectangle((int)(Position.X + Velocity.X * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                    (int)(Position.Y + Velocity.Y * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                    Rectangle.Width, Rectangle.Height);
            }
            else
            {
                next_rectangle = new Rectangle((int)(Position.X - Velocity.X * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                   (int)(Position.Y - Velocity.Y * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                   Rectangle.Width, Rectangle.Height);
            }
            
            foreach (Sprite s in walls)
            {
                if (next_rectangle.Intersects(s.Rectangle))
                    return true;
            }
            return false;
        }



    }
}
