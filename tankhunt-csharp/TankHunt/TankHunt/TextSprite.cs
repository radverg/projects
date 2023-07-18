using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class TextSprite
    {
        private string text;
        public string Text { get { return text; } set { text = value; WrapText(); } }
        private SpriteFont font;
        public Color Color { get; set; }
        public Vector2 Position { get; set; }
        public int Max_line_width { get; set; }

        public TextSprite(string text, SpriteFont font, Color color, Vector2 position, int max_width)
        {
            this.font = font;
            Max_line_width = max_width;
            Text = text;
            Color = color;
            
            Position = position;
        }

        public void WrapText()
        {
            string result = "";
            string[] words = Text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (font.MeasureString(result + " " + words[i]).X > Max_line_width)
                    result = result + "\n" + words[i];
                else if (i == 0)
                    result = words[i];
                else
                    result = result + " " + words[i];
            }
            text = result;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, Text, Position, Color, 0, Vector2.Zero, SC.res_ratio, SpriteEffects.None, 0);
        }

    }
}
