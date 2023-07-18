using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth_editor
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle Rectangle { get; set; }
        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; UpdateRectangle(); } }
        private Vector2 size;
        public Vector2 Size { get { return size; } set { size = value; UpdateRectangle(); } }
        float Rotation { get; set; }
        public Color Color { get; set; }
        public int Writenumber { get; set; }

        public Sprite(Texture2D texture, Vector2 position, Vector2 size, Color color, int writenumber)
        {
            this.Texture = texture;
            this.Color = color;
            this.Position = position;
            this.Writenumber = writenumber;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, (int)(size.X), (int)(size.Y));

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color);
        }

        public void DrawWithShadow(SpriteBatch spriteBatch, Color color, float transparency)
        {
            int shadow_move = (int)SC.ChangeX(8);
            spriteBatch.Draw(Texture, new Rectangle(Rectangle.X - shadow_move / 2, Rectangle.Y - shadow_move / 2, Rectangle.Width + shadow_move, Rectangle.Height + shadow_move), color * transparency);
            spriteBatch.Draw(Texture, Rectangle, Color);

 
        }

        public override string ToString()
        {
            return string.Format("Rectangle: {0}", Rectangle.ToString());
        }

        private void UpdateRectangle()
        {
            Rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }


    }
}
