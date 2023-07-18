using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class Sprite
    {
        private int net_ID;
        public int Net_ID { get { return net_ID; } set { net_ID = value; SetColor(); } }

      // Variables and properties -----------------------------------------------------------------------------------------------
        /// <summary>
        /// A texture that will be used for drawing to rectangle
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Rectangle that is used for drawing texture
        /// </summary>
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
                if (value > MathHelper.TwoPi)
                    rotation = value - MathHelper.TwoPi;
                else
                    rotation = value;
            }
        }

        private Vector2 origin;
        public Vector2 Origin
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
        }
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
        public Sprite(Texture2D texture, Vector2 position, Vector2 size, Color color)
        {
            this.Texture = texture;
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
            Color = Color.White;
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 size)
        {
            this.Texture = texture;
            this.Position = position;
            this.Size = size;
            Color = Color.White;
            Delete = false;
            Transparency = 1;
        } 
        #endregion

        public virtual void Move(GameTime gameTime)
        {
            Position = new Vector2((float)(Position.X + Velocity.X * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                (float)(Position.Y + Velocity.Y * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio)); 
        }

        public void Rotate(GameTime gameTime)
        {
            Rotation += Rotation_velocity * gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        // Various draw methods -----------------------------------------------------------------------------------------------------------------------------
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color * Transparency);
        }

        public void DrawRotated(SpriteBatch spritebatch)
        {
            Rectangle rrectangle = new Rectangle(Rectangle.X + (int)real_origin.X, Rectangle.Y + (int)real_origin.Y, Rectangle.Width, Rectangle.Height);
            spritebatch.Draw(Texture, rrectangle, null, Color * Transparency, (float)Rotation, Origin, SpriteEffects.None, 0);
        }
        public void DrawShadow(SpriteBatch spriteBatch, Color color, float transparency)
        {
            int shadow_move = (int)(8 * SC.res_ratio);
            spriteBatch.Draw(Texture, new Rectangle(Rectangle.X - shadow_move / 2, Rectangle.Y - shadow_move / 2, Rectangle.Width + shadow_move, Rectangle.Height + shadow_move), color * transparency);
        }

        public void DrawToCenter(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)(SC.screen_center.X - (size.X / 2)), (int)(SC.screen_center.Y - (size.Y / 2)), (int)size.X, (int)size.Y), Color * Transparency);

        }


        public void DrawToCenter(SpriteBatch spriteBatch, Vector2 camera_pos)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)(SC.screen_center.X - (size.X / 2.0f) + real_origin.X + camera_pos.X), (int)(SC.screen_center.Y - (size.Y / 2.0f) + real_origin.Y + camera_pos.Y), (int)size.X, (int)size.Y), null, Color * Transparency,
                (float)Rotation, Origin, SpriteEffects.None, 0);

        }
        // ---------------------------------------------------------------------------------------------------------------------------------------------------

        public override string ToString()
        {
            return string.Format("Rectangle: {0}", Rectangle.ToString());
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


        private void SetColor()
        {
            Color? color = GetIDColor(Net_ID);
            if (color != null)
                Color = (Color)color;
        }

        public static Color? GetIDColor(int id)
        {
            switch (id)
            {
                case 0: return Color.Red;
                case 1: return new Color(94, 85, 255); // blue
                case 2: return Color.Green;
                case 3: return new Color(225, 231, 18); // yellow
                case 4: return Color.Violet;
                case 5: return Color.Khaki;
                default: return null;
            }
        }

        public void Interpolate(GameTime game_time, float interpolation_const)
        {
            Vector2 difference = Remote_position - Position;
            if (difference.X < 2 || difference.Y < 2)
                Position = new Vector2(Remote_position.X, Remote_position.Y); // Jump to remote position immediately
            else
                Position += difference * interpolation_const; // Move sprite slowly towards remote position
        }
    }
}
