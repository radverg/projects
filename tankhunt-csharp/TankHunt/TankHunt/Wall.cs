using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunt
{
    public class Wall : Sprite
    {
        public WallSide Wall_side { get; private set; }
        public WallType Wall_type { get; private set; }

        public static Texture2D Horizontal_wall {  get; set; }
        public static Texture2D Vertical_wall { private get; set; }

        public Vector2 Square_position { get; private set; }

        public Wall(WallSide side, Vector2 square_pos, int square_size, Color color)
            :base()
        {
            Color = color;
            Wall_side = side;

            float smaller_size = Horizontal_wall.Height * SC.res_ratio;

            switch (side)
            {
                case WallSide.Top: 
                    {
                        Texture = Horizontal_wall;
                        Wall_type = WallType.HorizontalWall;
                        Position = square_pos * new Vector2(square_size);
                        Size = new Vector2(square_size + smaller_size, smaller_size);
                        break;
                    }
                case WallSide.Right:
                    {
                        Texture = Vertical_wall;
                        Wall_type = WallType.VerticalWall;
                        Position = new Vector2((square_pos.X + 1) * square_size, square_pos.Y * square_size);
                        Size = new Vector2(smaller_size, square_size + smaller_size); 
                        break;
                    }
                case WallSide.Down:
                    {
                        Texture = Horizontal_wall;
                        Wall_type = WallType.HorizontalWall;
                        Position = new Vector2(square_pos.X * square_size, (square_pos.Y + 1) * square_size);
                        Size = new Vector2(square_size + smaller_size, smaller_size);
                        break;
                    }
                case WallSide.Left:
                    {
                        Texture = Vertical_wall;
                        Wall_type = WallType.VerticalWall;
                        Position = square_pos * new Vector2(square_size);
                        Size = new Vector2(smaller_size, square_size + smaller_size); 
                        break;
                    }
            }
            Square_position = square_pos;
        }


        public enum WallSide
        {
            Top = 0,
            Right = 1,
            Down = 2,
            Left = 3,
        }

        public enum WallType
        {
            HorizontalWall,
            VerticalWall
        }

    }
}
