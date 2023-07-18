using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth
{
    public class RandomLevel
    {
        public static Texture2D Horizontal_wall { get; set; }
        public static Texture2D Vertical_wall { get; set; }
        public List<Sprite> Walls { get; set; }
        public int Square_size { get; set; }
        public Vector2 Size { get; set; }

        public event EventHandler Level_Changed;

        public RandomLevel(bool create_new)
        {
            Walls = new List<Sprite>();
            if (create_new)
                CreateRandomLevel();
        }
               

        public void CreateRandomLevel()
        {
            Walls.Clear();
            Square_size = SC.rnd.Next(70, 100);
            Size = new Vector2(SC.rnd.Next(4, SC.screen_rectangle.Height / Square_size), (SC.rnd.Next(4, SC.screen_rectangle.Height / Square_size)));
            for (int x = 0; x < Size.X; x++)
            {
                Walls.Add(new Sprite(Horizontal_wall, new Vector2(Square_size * x, 0), new Vector2(Square_size + Horizontal_wall.Height, Horizontal_wall.Height), Color.White, -1));
                Walls.Add(new Sprite(Horizontal_wall, new Vector2(Square_size * x, (Size.Y) * Square_size), new Vector2(Square_size + Horizontal_wall.Height, Horizontal_wall.Height), Color.White, -1));
            }
            for (int y = 0; y < Size.Y; y++)
            {
                Walls.Add(new Sprite(Vertical_wall, new Vector2(0, Square_size * y), new Vector2(Vertical_wall.Width, Square_size + Vertical_wall.Width), Color.White, -1));
                Walls.Add(new Sprite(Vertical_wall, new Vector2((Size.X) * Square_size, y * Square_size), new Vector2(Vertical_wall.Width, Square_size + Vertical_wall.Width), Color.White, -1));
            }

            for (int x = 0; x < Size.X; x++)
            {

                for (int y = 0; y < Size.Y; y++)
                {
                    int rnd = SC.rnd.Next(0, 5);
                    switch (rnd)
                    {
                        case 0:
                            {
                                break;
                            }
                        case 1:
                            {
                                Sprite new_sprite = new Sprite(Horizontal_wall, new Vector2(Square_size * x, Square_size * y), new Vector2(Square_size + Horizontal_wall.Height, Horizontal_wall.Height), Color.White, -1);
                                Walls.Add(new_sprite);
                                break;
                            }
                        case 2:
                            {
                                Sprite new_sprite = new Sprite(Vertical_wall, new Vector2(Square_size * (x + 1), y * Square_size), new Vector2(Vertical_wall.Width, Square_size + Vertical_wall.Width), Color.White, -1);
                                Walls.Add(new_sprite);
                                break;
                            }
                        case 3:
                            {
                                Sprite new_sprite = new Sprite(Horizontal_wall, new Vector2(Square_size * x, (y + 1) * Square_size), new Vector2(Square_size + Horizontal_wall.Height, Horizontal_wall.Height), Color.White, -1);
                                Walls.Add(new_sprite);
                                break;
                            }
                        case 4:
                            {
                                Sprite new_sprite = new Sprite(Vertical_wall, new Vector2(Square_size * x, y * Square_size), new Vector2(Vertical_wall.Width, Square_size + Vertical_wall.Width), Color.White, -1);
                                Walls.Add(new_sprite);
                                break;
                            }
                    }
                }


            }
            if (Level_Changed != null)
                Level_Changed(this, EventArgs.Empty);

        }

        public void SetExistingLevel(int square_size, Vector2 size, List<Sprite> walls)
        {
            Size = size;
            Square_size = square_size;
            Walls = walls;

            if (Level_Changed != null)
                Level_Changed(this, EventArgs.Empty);
        }
    }
}
