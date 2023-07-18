using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public struct Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public int intX { get { return (int)X; } }
        public int intY { get { return (int)Y; } }

        public float floatX { get { return (float)X; } }
        public float floatY { get { return (float)Y; } }


        public Vector2(double x, double y) : this()
        {
            X = x;
            Y = y;
        }

        public Vector2(Vector2 vector2) : this()
        {
            X = vector2.X;
            Y = vector2.Y;
        }


        public override string ToString()
        {
            return string.Format("[ X: {0} Y: {1} ]", X, Y);
        }

        public static Vector2 operator +(Vector2 first, Vector2 second)
        {
            return new Vector2(first.X + second.X, first.Y + second.Y);
        }

        public static Vector2 operator -(Vector2 first, Vector2 second)
        {
            return new Vector2(first.X - second.X, first.Y - second.Y);
        }

        public static Vector2 operator *(Vector2 first, Vector2 second)
        {
            return new Vector2(first.X * second.X, first.Y * second.Y);
        }

        public static Vector2 operator /(Vector2 first, Vector2 second)
        {
            return new Vector2(first.X / second.X, first.Y / second.Y);
        }

        public static Vector2 operator *(Vector2 first, double second)
        {
            return new Vector2(first.X * second, first.Y * second);
        }
        
    }
}
