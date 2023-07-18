using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class Bouncer
    {
        private IntersectManager IM = new IntersectManager();
        private List<Sprite> walls;

        public Bouncer(List<Sprite> walls)
        {
            this.walls = walls;
        }

        public void SetIM(Rectangle current, Rectangle next)
        {
            IM.SetPreviousStatus();
            IM.SetAllI(false);
            Sprite[] walls = GetIntersectedWalls(next).ToArray();

            foreach (Sprite wall in walls)
            {
                if (wall.Size.X > wall.Size.Y) // Horizontal wall case
                {
                    IM.TopI = (current.Right > wall.Rectangle.X && current.X < wall.Rectangle.Right);

                    IM.RightI = (current.Right < wall.Rectangle.X || current.X > wall.Rectangle.Right);

                }
                else // Vertical wall case
                {
                    IM.RightI = (current.Bottom > wall.Rectangle.Y && current.Y < wall.Rectangle.Bottom);

                    IM.TopI = (current.Bottom < wall.Rectangle.Y || current.Y > wall.Rectangle.Bottom);
                }
            }
        }

        public bool TopTotalBounce()
        {
            return IM.TopI && !IM.TopPI;
        }

        public bool RightTotalBounce()
        {
            return IM.RightI && !IM.RightPI;
        }


        public IEnumerable<Sprite> GetIntersectedWalls(Rectangle next)
        {
            return from i in walls where i.Rectangle.Intersects(next) select i;
        }

    }
}
