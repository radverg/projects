using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class BouncingSprite : Sprite
    {
        public IntersectManager IM { get; set; }
        private Level Level_component;
        public string Name { get; set; }
        public Vector2 TargetPoint { get; set; }
        public Vector2?[] Block_positions { get; set; }
        private Vector2 Primary_block_position { get { return SC.GetSquarePosition(SC.GetCenter(Rectangle)); } }
        public enum Bounces { Top, Left, Right, Down };
        public bool Alive { get; set; }
    
        public BouncingSprite(Texture2D texture, Vector2 position, Vector2 size, Color color, int writenumber, Vector2 velocity, Level level): base(texture, position, size, color, writenumber)        
        {
            IM = new IntersectManager(); // Create intersect manager instance
            Velocity_const = velocity; // Set velocity
            Block_positions = new Vector2?[4]; // 
            this.Level_component = level;
            Velocity_coefficient = new Vector2(1, 0);
            Alive = true;
        }

        private bool CheckBlockBounces(Rectangle rect, Bounces bounce)
        {
            Vector2 position = new Vector2(SC.GetSquarePosition(rect.X), SC.GetSquarePosition(rect.Y)); // Set block position of determined rectangle
            if (bounce == Bounces.Left || bounce == Bounces.Right) // Check left or right block bounce
            {
                for (int i = 0; i < Block_positions.Length; i++)
                {
                    if (Block_positions[i] != null)
                        if (Block_positions[i].Value.X == position.X)
                            return false;
                }
                return true;
            }

            if (bounce == Bounces.Top || bounce == Bounces.Down) // Check top or down block bounce
            {
                for (int i = 0; i < Block_positions.Length; i++)
                {
                    if (Block_positions[i] != null)
                        if (Block_positions[i].Value.Y == position.Y)
                            return false;
                }
                return true;
            }
            return true;

        }

        public int GetBlockPositionsCount()
        {
            int count = 0;
            for (int i = 0; i < Block_positions.Length; i++)
                if (Block_positions[i] != null)
                    count++;
            return count;
        }


        public void SetBounces()
        {
            IM.SetPreviousStatus(); // Set previous status
            IM.SetAllI(false); // Set current (new) status to false

            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                {
                    Sprite tile = Level_component.Level_set.Selected_level.Field[(int)Primary_block_position.X + x, (int)Primary_block_position.Y + y];
                    if (Rectangle.Intersects(tile.Rectangle) &&
                        tile.Writenumber != 0 && tile.Writenumber != 1)
                    {
                        if (CheckBlockBounces(tile.Rectangle, Bounces.Left))
                            IM.LeftI = true;

                        if (CheckBlockBounces(tile.Rectangle, Bounces.Right))
                            IM.RightI = true;

                        if (CheckBlockBounces(tile.Rectangle, Bounces.Down))
                            IM.DownI = true;

                        if (CheckBlockBounces(tile.Rectangle, Bounces.Top))
                            IM.TopI = true;
                    }
                }     
  
            SetBlockPositions();
        }

        /// <summary>
        /// Changes horizontal and vertical speed according to what bounce is true 
        /// </summary>
        public void CheckBounces()
        {
            if ((IM.LeftI && !IM.LeftPI) || (IM.RightI && !IM.RightPI)) // Check right and left side bounces and set velocity
                Velocity_coefficient = new Vector2(-Velocity_coefficient.X, Velocity_coefficient.Y);

            if ((IM.TopI && !IM.TopPI) || (IM.DownI && !IM.DownPI)) // Check top and down bounces and set velocity
                Velocity_coefficient = new Vector2(Velocity_coefficient.X, -Velocity_coefficient.Y);
        }

        /// <summary>
        /// Sets all block positions which player intersects
        /// </summary>
        private void SetBlockPositions()
        {
            int index = 0;
            Vector2 Primary_block_position = this.Primary_block_position;
            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                {
                    if (Rectangle.Intersects(Level_component.Level_set.Selected_level.Field[(int)Primary_block_position.X + x, (int)Primary_block_position.Y + y].Rectangle) && index < 4)
                    {
                        Block_positions[index] = new Vector2(Primary_block_position.X + x, Primary_block_position.Y + y);
                        index++;
                    }
                }     

            for (int i = index; i < 4; i++)
                Block_positions[i] = null;
        }

        public void ChangeDirection()
        {
            TargetPoint = new Vector2(SC.mousestate.X + Level_component.Camera.absoulute_pos.X, SC.mousestate.Y + Level_component.Camera.absoulute_pos.Y);
            Vector2 player_center_position = SC.GetCenter(Rectangle);
            float x_distance = TargetPoint.X - player_center_position.X;
            float y_distance = TargetPoint.Y - player_center_position.Y;
            float total_distance = SC.GetDistance(TargetPoint, player_center_position);
           
            Velocity_coefficient = new Vector2(x_distance / total_distance, y_distance / total_distance);
        }
    }
}
