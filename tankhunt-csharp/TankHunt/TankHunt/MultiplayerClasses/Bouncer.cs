using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class Bouncer
    {
        private IntersectManager IM = new IntersectManager();
        private List<Wall> walls;

        public Bouncer(List<Wall> walls)
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
                    if (!IM.TopI)
                        IM.TopI = (current.Right > wall.Rectangle.X && current.X < wall.Rectangle.Right);

                    if (!IM.RightI)
                        IM.RightI = ((current.Right <= wall.Rectangle.X) || (current.X >= wall.Rectangle.Right)) && ((current.Top <= wall.Rectangle.Bottom) && (current.Bottom >= wall.Rectangle.Top)) && walls.Length == 1;

                }
                else // Vertical wall case
                {
                    if (!IM.RightI)
                        IM.RightI = (current.Bottom > wall.Rectangle.Y && current.Y < wall.Rectangle.Bottom);

                    if (!IM.TopI)
                        IM.TopI = (current.Bottom <= wall.Rectangle.Y || current.Y >= wall.Rectangle.Bottom) && ((current.Right >= wall.Rectangle.Left) && (current.Left <= wall.Rectangle.Right)) && walls.Length == 1;
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
            IEnumerable<Wall> All_intersected = from i in walls where i.Rectangle.Intersects(next) select i;
            List<Wall> Return = new List<Wall>();
            foreach (Wall w in All_intersected)
            {
                bool add = true;
                foreach (Wall w_ret in Return)
                {
                    if ((add = (w.Rectangle != w_ret.Rectangle)) == false)
                        break;
                }

                if (add)
                    Return.Add(w);
            }
            return Return;           
        }
    }
}
