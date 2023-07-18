using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using TankHuntServer.WebSocketTankHuntServer;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public class Sprite
    {
        private int net_ID;
        public int Net_ID { get { return net_ID; } set { net_ID = value; } }

        // Variables and properties -----------------------------------------------------------------------------------------------
        
        public Rectangle Rectangle { get; set; }

        // Positions---------------------------------------
        public Vector2 Previous_position { get; set; }
        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; UpdateRectangle(value, size); } }
        public Vector2 Next_position { get; set; }

        public Vector2 Remote_position { get; set; }
        //-------------------------------------------------
        private Vector2 size;
        public Vector2 Size { get { return size; } set { size = value; UpdateRectangle(position, value); } }

        public double Previous_rotation { get; set; }
        private double rotation;
        public double Rotation
        {
            get { return rotation; }
            set
            {
                if (value >  Math.PI * 2)
                    rotation = value - Math.PI * 2;
                else
                    rotation = value;
            }
        }

        private Vector2 origin;
      /*  public Vector2 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
                if (Texture != null)
                    real_origin = value * new Vector2(Rectangle.Width / (float)Texture.Width, Rectangle.Height / (float)Texture.Height);
            }
        }*/
        private Vector2 real_origin;
        public Color Color { get; set; }

        public float Transparency { get; set; }

        // Velocities: ---------------------------------------------------------------------------------------------------------------
        private Vector2 velocity_const;
        public Vector2 Velocity_const { get { return velocity_const; } set { velocity_const = value; Velocity = velocity_const * velocity_coefficient; } }
        private Vector2 velocity_coefficient;
        public Vector2 Velocity_coefficient { get { return velocity_coefficient; } set { velocity_coefficient = value; Velocity = velocity_coefficient * velocity_const; } }
        public Vector2 Velocity { get; private set; }
        public float Rotation_velocity { get; set; }
        //  -------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// It can be used for removing sprite from collections
        /// </summary>
        public bool Delete { get; set; }

        #region Constructors
        public Sprite(Vector2 position, Vector2 size, Color color)
        {
            this.Color = color;
            this.Position = position;
            this.Size = size;
            Delete = false;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, (int)(size.X), (int)(size.Y));
            Transparency = 1;
        }

        public Sprite()
        {
            Delete = false;
            Transparency = 1;
            Color = Colors.White;
        }

        public Sprite(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;
            Color = Colors.White;
            Delete = false;
            Transparency = 1;
        }
        #endregion

        public virtual void Move(TimeSpan deltaTime)
        {
            Position = new Vector2((float)(Position.X + Velocity.X * deltaTime.TotalMilliseconds),
                (float)(Position.Y + Velocity.Y * deltaTime.TotalMilliseconds));
        }

        public void Rotate(TimeSpan deltaTime)
        {
            Rotation += Rotation_velocity * deltaTime.TotalMilliseconds;
        }

      
        protected void UpdateRectangle(Vector2 position, Vector2 size)
        {
            Rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public void BuildRectangle(float x, float y, float sizeX, float sizeY)
        {
            Rectangle = new Rectangle((int)x, (int)y, (int)sizeX, (int)sizeY);
        }

        /// <summary>
        /// Determines if position has equal value to previous position, these properties have to be set manually in game loop
        /// </summary>
        /// <returns>True - position was changed, False - position wasn't changed</returns>
        public bool PositionChanged()
        {
            if (Position.ToString() != Previous_position.ToString())
                return true;
            return false;
        }

        /// <summary>
        /// Determines if rotation has equal value to previous rotation, these properties have to be set manually in game loop
        /// </summary>
        /// <returns>True - rotation was changed, False - rotation wasn't changed</returns>
        public bool RotationChanged()
        {
            if (rotation != Previous_rotation)
                return true;
            return false;
        }




        public void Interpolate(TimeSpan deltaTime, float interpolation_const)
        {
            Vector2 difference = Remote_position - Position;
            if (difference.X < 2 || difference.Y < 2)
                Position = new Vector2(Remote_position.X, Remote_position.Y); // Jump to remote position immediately
            else
                Position += difference * interpolation_const; // Move sprite slowly towards remote position
        }
    }
}
