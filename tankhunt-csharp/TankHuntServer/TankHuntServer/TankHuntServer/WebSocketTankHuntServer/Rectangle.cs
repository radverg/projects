using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public class Rectangle
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int Top { get { return Y; } }
        public int Bottom { get { return Y + Height;  } }
        public int Left { get { return X; } }
        public int Right { get { return X + Height; } }


        public Rectangle()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }


        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }


        public bool Intersects(Rectangle rect)
        {
            return !(Top > rect.Bottom ||
                Bottom < rect.Top ||
                Left > rect.Right ||
                Right < rect.Left);
        }


    }
}
