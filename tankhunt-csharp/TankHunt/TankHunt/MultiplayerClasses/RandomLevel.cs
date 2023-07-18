using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunt
{
    public class RandomLevel
    {
        public List<Wall> Walls { get; set; }
        public Tile[,] Tiles { get; set; }
        public List<Tile> Useable_tiles { get; set; }
        public bool Darkness { get; set; }

        private int square_size;
        public int Square_size
        {
            get
            {
                return square_size;
            }
            set
            {
                square_size = value;
                Absolute_size = new Vector2(square_size * Size.X + Wall.Horizontal_wall.Height * SC.res_ratio, square_size * size.Y
                    + Wall.Horizontal_wall.Height * SC.res_ratio);
            }
        }

        private Vector2 size;
        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                Absolute_size = new Vector2(square_size * size.X + Wall.Horizontal_wall.Height * SC.res_ratio, square_size * size.Y
                    + Wall.Horizontal_wall.Height * SC.res_ratio);
            }
        }

        private Vector2 absolute_size;
        public Vector2 Absolute_size { get { return absolute_size; } private set { absolute_size = value; DataTranslator.Level_absolute_size = absolute_size; } }

        private Vector2 default_positioin;
        public Vector2 Default_position
        {
            get { return default_positioin; }
            set
            {
                default_positioin = value;
                Useable_tiles = GetUseableTiles(value);
            }
        }

        public event EventHandler Level_Changed;

        public List<TankPlayerSprite> Players { get; set; }

        public RandomLevel()
        {
            Walls = new List<Wall>();
        }


        public void CreateRandomLevel(IEnumerable<Color> players_colors, Vector2 max_min_square_size, Vector2 max_min_size)
        {
            Walls.Clear();
            Square_size = SC.rnd.Next((int)max_min_square_size.Y, (int)max_min_square_size.X);
            Size = new Vector2(SC.rnd.Next((int)max_min_size.Y, (int)max_min_size.X), (SC.rnd.Next((int)max_min_size.Y, (int)max_min_size.X)));
            Tiles = new Tile[(int)Size.X, (int)Size.Y];


            for (int x = 0; x < Size.X; x++)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    Tiles[x, y] = new Tile(new Vector2(x, y), Square_size);
                    int rnd = SC.rnd.Next(0, 4);

                    Color color;
                    if (SC.rnd.Next(0, 100) < 7)
                        color = players_colors.ToArray()[SC.rnd.Next(0, players_colors.ToArray().Length)];
                    else
                        color = Color.Black;

                    if (rnd < 4)
                    {
                        Wall new_wall = new Wall((Wall.WallSide)rnd, new Vector2(x, y), Square_size, color);
                        Walls.Add(new_wall);
                        Tiles[x, y].AddWall(new_wall);
                    }
                }
            }

            for (int x = 0; x < Size.X; x++) // Set upper and lower edges
            {
                Wall wall1 = new Wall(Wall.WallSide.Top, new Vector2(x, 0), Square_size, Color.Black);
                Wall wall2 = new Wall(Wall.WallSide.Down, new Vector2(x, Size.Y - 1), Square_size, Color.Black);
                Walls.Add(wall1); Walls.Add(wall2);
                Tiles[x, 0].AddWall(wall1); Tiles[x, (int)(Size.Y) - 1].AddWall(wall2);

            }
            for (int y = 0; y < Size.Y; y++) // Set left and right edges
            {
                Wall wall1 = new Wall(Wall.WallSide.Right, new Vector2(Size.X - 1, y), Square_size, Color.Black);
                Wall wall2 = new Wall(Wall.WallSide.Left, new Vector2(0, y), Square_size, Color.Black);
                Walls.Add(wall1); Walls.Add(wall2);
                Tiles[0, y].AddWall(wall2); Tiles[(int)(Size.X) - 1, y].AddWall(wall1);
            }

            foreach (Wall w in Walls) // Fix wall's colors
            {
                var ws = from wa in Walls where (w.Rectangle == wa.Rectangle) && (w != wa) select wa;
                foreach (var wal in ws)
                {
                    wal.Color = Color.Black;
                }
            }

            if (Level_Changed != null)
                Level_Changed(this, EventArgs.Empty);
        }


        public void SetExistingLevel(int square_size, Vector2 size, List<Wall> walls)
        {
            Size = size;
            Square_size = square_size;
            Walls = walls;

            if (Level_Changed != null)
                Level_Changed(this, EventArgs.Empty);
        }

        public List<Tile> GetUseableTiles(Vector2 pos)
        {
            List<Tile> useable_tiles = new List<Tile>();
            List<Tile> queue = new List<Tile>();
            Vector2[] directions = new Vector2[] { new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0) };

            int square_pos_x = (int)Math.Floor(pos.X / Square_size);
            int square_pos_y = (int)Math.Floor(pos.Y / Square_size);

            useable_tiles.Add(Tiles[square_pos_x, square_pos_y]); // Add tile where object is - default tile
            queue.Add(Tiles[square_pos_x, square_pos_y]); // Start up queue by default tile

            while (queue.Count > 0)
            {
                Tile tile = queue.First(); // Save first tile in queue
                queue.RemoveAt(0); // Then remove it from queue
                Tile next_tile = null;

                for (int i = 0; i < directions.Length; i++)
                {
                    Vector2 next_tile_pos = tile.Square_position + directions[i];
                    if (!(next_tile_pos.X < 0 || next_tile_pos.X >= Size.X || next_tile_pos.Y < 0 || next_tile_pos.Y >= Size.Y)) // Check if tile is in range
                    {
                        next_tile = Tiles[(int)next_tile_pos.X, (int)next_tile_pos.Y];
                        if (!useable_tiles.Contains(next_tile) && tile.NextTileAccessible(next_tile))
                        {
                            useable_tiles.Add(next_tile);
                            queue.Add(next_tile);
                        }
                    }
                }
            }
            return useable_tiles;
           
        }

        public Vector2 GetUsableTiledRndPosition(Vector2 size_of_sprite)
        {
            Tile random_tile = Useable_tiles[SC.rnd.Next(0, Useable_tiles.Count)];
            return new Vector2(SC.rnd.Next(((int)random_tile.Square_position.X * Square_size) + 5, ((int)random_tile.Square_position.X * Square_size) + Square_size - (int)size_of_sprite.X - 5),
                SC.rnd.Next(((int)random_tile.Square_position.Y * Square_size) + 5, ((int)random_tile.Square_position.Y * Square_size) + Square_size - (int)size_of_sprite.Y - 5));
        }

        public void RandomizeDefaultPosition(int min_useable_tiles, Vector2 size_of_sprite)
        {    
            Useable_tiles = new List<Tile>();
            
            int attempts = 0;
            do
            {
                Useable_tiles.Clear();
                foreach (Tile t in Tiles)
                {
                    Useable_tiles.Add(t);
                }
                attempts++;
                Default_position = GetUsableTiledRndPosition(size_of_sprite);
            }
            while (GetUseableTiles(default_positioin).Count < min_useable_tiles && attempts < 1000);
        }

        public List<Tile> GetIntersectedTiles(Rectangle rect)
        {
            List<Tile> tiles_return = new List<Tile>();
            Vector2[] corners = new Vector2[] { new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width - 1, rect.Y), new Vector2(rect.X + rect.Width - 1, rect.Y + rect.Height - 1), new Vector2(rect.X, rect.Y + rect.Height - 1) };

            for (int i = 0; i < corners.Length; i++)
            {
                Vector2 square_pos = new Vector2((float)Math.Floor(corners[i].X / (double)Square_size), (float)Math.Floor(corners[i].Y / (double)Square_size));
                Tile current = Tiles[(int)square_pos.X, (int)square_pos.Y];
                if (!tiles_return.Contains(current))
                    tiles_return.Add(current);
            }
            return tiles_return;
        }
    }
    
}
