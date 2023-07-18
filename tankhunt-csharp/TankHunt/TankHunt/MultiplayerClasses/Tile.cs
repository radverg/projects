using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class Tile
    {
        private List<Wall> Tile_walls = new  List<Wall>();
        public Vector2 Square_position { get; private set; }
        private int Square_size;

        public Rectangle Rectangle { get; private set; }

        public int Wave_number { get; set; }

        public Tile(Vector2 square_pos, int square_size)
        {
            Square_position = square_pos;
            Square_size = square_size;
            Wave_number = 0;
            Rectangle = new Rectangle((int)square_pos.X * square_size, (int)square_pos.Y * square_size, square_size, square_size);
        }

        public void AddWall(Wall wall)
        {
            Tile_walls.Add(wall);            
        }

        public IEnumerable<Wall> GetWalls()
        {
            return Tile_walls;
        }

        public Sides GetSides() // Bad code!!!!!!!
        {
            var current_sides = from tile in Tile_walls select tile.Wall_side;
            Sides sides = Sides.Top | Sides.Right | Sides.Down | Sides.Left;
            if (!current_sides.Contains(Wall.WallSide.Top))
                sides = sides ^ Sides.Top;
            if (!current_sides.Contains(Wall.WallSide.Right))
                sides = sides ^ Sides.Right;
            if (!current_sides.Contains(Wall.WallSide.Down))
                sides = sides ^ Sides.Down;
            if (!current_sides.Contains(Wall.WallSide.Left))
                sides = sides ^ Sides.Left;
            return sides;                     
        }

        public bool NextTileAccessible(Tile tile_for_check)
        {
            // Upper tile case
            if (Square_position.X == tile_for_check.Square_position.X && Square_position.Y > tile_for_check.Square_position.Y)
                return ((GetSides() & Sides.Top) != Sides.Top) && ((tile_for_check.GetSides() & Sides.Down) != Sides.Down);
            // Right tile case
            if (Square_position.X < tile_for_check.Square_position.X && Square_position.Y == tile_for_check.Square_position.Y)
                return ((GetSides() & Sides.Right) != Sides.Right) && ((tile_for_check.GetSides() & Sides.Left) != Sides.Left);
            // Lower tile case
            if (Square_position.X == tile_for_check.Square_position.X && Square_position.Y < tile_for_check.Square_position.Y)
                return ((GetSides() & Sides.Down) != Sides.Down) && ((tile_for_check.GetSides() & Sides.Top) != Sides.Top);
            // Left tile case
            if (Square_position.X > tile_for_check.Square_position.X && Square_position.Y == tile_for_check.Square_position.Y)
                return ((GetSides() & Sides.Left) != Sides.Left) && ((tile_for_check.GetSides() & Sides.Right) != Sides.Right);
            return false;
        }

        [Flags]
        public enum Sides
        {
            None = 0,
            Top = 1,
            Right = 2,
            Down = 4,
            Left = 8,
        }

    }
}
