using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TankHunt
{
    public class SimpleTextBox
    {
        public TextSprite Text { get; set; }
        public TextSprite Name { get; set; }

        private SpriteFont font;

        public string Label { get; set; }

        public bool Activated { get; set; }

        private Vector2 Position { get; set; }



        private Keys[] letters = new Keys[] { Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Z, Keys.U, Keys.I, Keys.O, Keys.P, Keys.A, Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, Keys.Y,
            Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M };

        public SimpleTextBox(Vector2 pos, SpriteFont font, string label, int max_width)
        {
            Name = new TextSprite(label, font, Color.White, pos, 200);
            Text = new TextSprite("", font, Color.White, Name.Position + new Vector2(font.MeasureString(Name.Text).X, 0), max_width ); 
            this.font = font;
            Activated = false;
        }

        public void Update()
        {
            for (int i = 0; i < letters.Length; i++)
            {
                if (SC.CheckKeyPressed(letters[i], true))
                    Text.Text += letters[i].ToString().ToLower();
            }

            if (SC.CheckKeyPressed(Keys.Space, true))
                Text.Text += " ";

            if (SC.CheckKeyPressed(Keys.Back, true) && Text.Text.Length > 0)
            {
                Text.Text = Text.Text.Substring(0, Text.Text.Length - 1);
            }

            if (SC.CheckKeyPressed(Keys.OemMinus, true))
                Text.Text += "-";

            if (SC.CheckKeyPressed(Keys.NumPad0, true) || SC.CheckKeyPressed(Keys.D0, true))
                Text.Text += "0";
            if (SC.CheckKeyPressed(Keys.NumPad1, true) || SC.CheckKeyPressed(Keys.D1, true))
                Text.Text += "1";
            if (SC.CheckKeyPressed(Keys.NumPad2, true) || SC.CheckKeyPressed(Keys.D2, true))
                Text.Text += "2";
            if (SC.CheckKeyPressed(Keys.NumPad3, true) || SC.CheckKeyPressed(Keys.D3, true))
                Text.Text += "3";
            if (SC.CheckKeyPressed(Keys.NumPad4, true) || SC.CheckKeyPressed(Keys.D4, true))
                Text.Text += "4";
            if (SC.CheckKeyPressed(Keys.NumPad5, true) || SC.CheckKeyPressed(Keys.D5, true))
                Text.Text += "5";
            if (SC.CheckKeyPressed(Keys.NumPad6, true) || SC.CheckKeyPressed(Keys.D6, true))
                Text.Text += "6";
            if (SC.CheckKeyPressed(Keys.NumPad7, true) || SC.CheckKeyPressed(Keys.D7, true))
                Text.Text += "7";
            if (SC.CheckKeyPressed(Keys.NumPad8, true) || SC.CheckKeyPressed(Keys.D8, true))
                Text.Text += "8";
            if (SC.CheckKeyPressed(Keys.NumPad9, true) || SC.CheckKeyPressed(Keys.D9, true))
                Text.Text += "9";

        }

        public void Draw(SpriteBatch sb)
        {
            Name.Draw(sb);
            Text.Draw(sb);
          
        }
    }
}
