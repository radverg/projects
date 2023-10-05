using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace balonky
{
    public class balonek
    {
        public Texture2D ballon;
        public int color;
        public int speed;
        public int X;
        public int Y;
        public int score_value;
        public bool aktivni = true;

        public balonek(Texture2D loadedsprite, int colorr, int speedd)
        {
            ballon = loadedsprite;
            speed = speedd;
            color = colorr;
            score_value = color * 5;
            generateposition();
        }

        public void generateposition()
        {
            X = Game1.rand.Next(Game1.sirkaOkna);
            Y = Game1.vyskaOkna + Game1.rand.Next(300);
        }

        public void flyup()
        {
            Y -= speed;
            if (Y < -183) generateballon();
        }

        public void generateballon()
        {
            aktivni = true;
            int colorr = Game1.rand.Next(5);
            ballon = Game1.spriteballons[colorr];
            color = colorr;
            speed = 1 + Game1.rand.Next(6);
            switch (color)
            {
                case 0:
                    score_value = -20 * speed;
                    break;
                case 1:
                    score_value = -10 * speed;
                    break;
                case 2:
                    score_value = 1 * speed;
                    break;
                case 3:
                    score_value = 2 * speed;
                    break;
                case 4:
                    score_value = 3 * speed;
                    break;
            }
            generateposition();
        }
    };
}

