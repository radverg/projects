using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class Sprite
    {
        private int net_ID;
        public int Net_ID { get { return net_ID; } set { net_ID = value; SetColor(); } }

      // Variables and properties -----------------------------------------------------------------------------------------------
        public Vector2 real_origin;
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
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public float Transparency { get; set; }
        public int Writenumber { get; set; }

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
        public Sprite(Texture2D texture, Vector2 position, Vector2 size, Color color, int writenumber)
        {
            this.Texture = texture;
            this.Color = color;
            this.Position = position;
            this.Size = size;
            this.Writenumber = writenumber;
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
            spriteBatch.Draw(Texture, Rectangle, Color);
        }

        public void DrawRotated(SpriteBatch spritebatch)
        {
            real_origin = Origin * new Vector2(Rectangle.Width / (float)Texture.Width, Rectangle.Height / (float)Texture.Height);
            Rectangle rrectangle = new Rectangle(Rectangle.X + (int)real_origin.X, Rectangle.Y + (int)real_origin.Y, Rectangle.Width, Rectangle.Height);
            spritebatch.Draw(Texture, rrectangle, null, Color * Transparency, (float)Rotation, Origin, SpriteEffects.None, 0);

        }
        public void DrawWithShadow(SpriteBatch spriteBatch, Color color, float transparency)
        {
            int shadow_move = (int)SC.ChangeX(8);
            spriteBatch.Draw(Texture, new Rectangle(Rectangle.X - shadow_move / 2, Rectangle.Y - shadow_move / 2, Rectangle.Width + shadow_move, Rectangle.Height + shadow_move), color * Transparency);
            spriteBatch.Draw(Texture, Rectangle, Color * Transparency);
        }

        public void DrawToCenter(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)(SC.screen_center.X - (size.X / 2) + Origin.X), (int)(SC.screen_center.Y - (size.Y / 2) + Origin.Y), (int)size.X, (int)size.Y),null, Color * Transparency,
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
            switch (Net_ID)
            {
                case 0: Color = Color.Red; break;
                case 1: Color = new Color(94, 85, 255); break; // blue
                case 2: Color = Color.Green; break;
                case 3: Color = new Color(225, 231, 18); break; // yellow
                case 4: Color = Color.Violet; break;
                case 5: Color = Color.Khaki; break;
            }
        }
    }
}
