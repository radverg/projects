using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class BouncingMSprite : Sprite
    {
        protected IntersectManager IM = new IntersectManager();
        protected List<Sprite> walls;
        private Rectangle next_pos_rec;
        public BouncingMSprite(Texture2D texture, Vector2 position, Vector2 size, Color color, List<Sprite> walls)
            :base(texture, position, size, color, -1)
        {
            this.walls = walls;
        }

        public BouncingMSprite()
            : base()
        {
            
        }

        public BouncingMSprite(List<Sprite> walls)
        {
            this.walls = walls;
        }
    

        public void SetIM()
        {
            IM.SetPreviousStatus();
            IM.SetAllI(false);
            Sprite[] walls = GetIntersectedWalls().ToArray();

            foreach (Sprite wall in walls)
            {
                if (wall.Size.X > wall.Size.Y) // Horizontal wall case
                {
                    IM.TopI = (Rectangle.Right > wall.Rectangle.X && Rectangle.X < wall.Rectangle.Right);

                    IM.RightI = (Rectangle.Right < wall.Rectangle.X || Rectangle.X > wall.Rectangle.Right);   
                    
                }
                else // Vertical wall case
                {
                    IM.RightI = (Rectangle.Bottom > wall.Rectangle.Y && Rectangle.Y < wall.Rectangle.Bottom);

                    IM.TopI = (Rectangle.Bottom < wall.Rectangle.Y || Rectangle.Y > wall.Rectangle.Bottom);
                }
            }
        }

        public bool[] GetResult()
        {
            return new bool[] { IM.RightI, IM.TopI } ;
        }

        public IEnumerable<Sprite> GetIntersectedWalls()
        {
            next_pos_rec = new Rectangle((int)Next_position.X, (int)Next_position.Y, (int)Size.X, (int)Size.Y);
            return from i in walls where i.Rectangle.Intersects(next_pos_rec) select i;
        }
    }
}
