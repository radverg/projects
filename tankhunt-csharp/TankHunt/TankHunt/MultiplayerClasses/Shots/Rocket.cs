using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunt
{
    class Rocket : Shot
    {
        public static SoundEffect Shot_sound { private get; set; }
        public static Texture2D ShotTexture { private get; set; }

        private Timer startSeekingTimer;
        public bool seeking { get; private set; }

        public Vector2 killingPoint { get; set; }
        public Vector2 centerPoint { get; set; }

        private float radius;

        public Rocket(Vector2 position, Vector2 size, Color color, RandomLevel level, double player_rotation, Vector2 Shot_velocity, int time_to_remove, TankPlayerSprite owner)
            :base(ShotTexture, position, size, color, level, player_rotation, Shot_velocity, time_to_remove, owner)
        {
            Color = color;
            Rotation_velocity = 0.007f;
            centerPoint = Position;
            radius = size.X / 2;
            Movement_angle = player_rotation;
            startSeekingTimer = new Timer(3200);
            startSeekingTimer.Start();
            seeking = false;
            Origin = Size / 2;
        }

        public override void Update(GameTime game_time)
        {
            if (startSeekingTimer.IsTicked)
            {
                seeking = true;
                startSeekingTimer.Stop();
            }

            if (!seeking)
            {
                startSeekingTimer.Update(game_time);
                Move(game_time);
                return;
            }

            Tile target = null;
           
            // Find path and target --------------------------------------------------------------------------------------------------------------
            List<Tile> useable_tiles = new List<Tile>();
            List<Tile> queue = new List<Tile>();
            Vector2[] directions = new Vector2[] { new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0) };

            int square_pos_x = (int)Math.Floor(Position.X / Level.Square_size);
            int square_pos_y = (int)Math.Floor(Position.Y / Level.Square_size);

            if (square_pos_x > Level.Size.X - 1 || square_pos_y > Level.Size.Y - 1 || square_pos_x < 0 || square_pos_y < 0) // Check if shot is in level
            {
                Move(game_time);
                return;
            }

            useable_tiles.Add(Level.Tiles[square_pos_x, square_pos_y]); // Add tile where object is - default tile 
            queue.Add(Level.Tiles[square_pos_x, square_pos_y]); // Start up queue by default tile

            while (queue.Count > 0)
            {
                Tile tile = queue.First(); // Save first tile in queue
                queue.RemoveAt(0); // Then remove it from queue
                Tile next_tile = null;

                // Check if there is a player in this tile, if yes, then end loop and set target tile
                foreach (TankPlayerSprite p in Level.Players)
                {
                    if (p.Rectangle.Intersects(tile.Rectangle) && p.IsAlive)
                    {
                        Color = p.Color; // Set rocket's color acccording to player beeing seeked
                        target = tile;
                        useable_tiles.Add(tile); // Add tile where object is - default tile
                    }
                }
                if (target != null)
                    break;
                // --------------------------------------------------------------------------

                for (int i = 0; i < directions.Length; i++)
                {
                    Vector2 next_tile_pos = tile.Square_position + directions[i];
                    if (!(next_tile_pos.X < 0 || next_tile_pos.X >= Level.Size.X || next_tile_pos.Y < 0 || next_tile_pos.Y >= Level.Size.Y)) // Check if tile is in range
                    {
                        next_tile = Level.Tiles[(int)next_tile_pos.X, (int)next_tile_pos.Y];
                        if (!useable_tiles.Contains(next_tile) && tile.NextTileAccessible(next_tile))
                        {
                            useable_tiles.Add(next_tile);
                            queue.Add(next_tile);
                            next_tile.Wave_number = tile.Wave_number + 1;
                        }
                    }
                }
            }

            if (target == null) // No reachable player -> no rotation
            {
                Move(game_time);
                return;
            }

            // Count path and save it to collection
            List<Tile> path = new List<Tile>();
            path.Add(useable_tiles.Last());
            int current_num = path[0].Wave_number; // !!!!!!!!!!!! its possible to save only first two tiles into path
            for (int i = useable_tiles.Count - 1; i >= 0; i--)
            {
                byte xTempDistance = (byte)MathHelper.Distance(path.Last().Square_position.X, useable_tiles[i].Square_position.X);
                byte yTempDistance = (byte)MathHelper.Distance(path.Last().Square_position.Y, useable_tiles[i].Square_position.Y);
                if (useable_tiles[i].Wave_number == current_num - 1 && xTempDistance < 2 && yTempDistance < 2 && (xTempDistance != yTempDistance) && path.Last().NextTileAccessible(useable_tiles[i]))
                {
                    path.Add(useable_tiles[i]);
                    current_num--;
                }
            }

            path.Reverse();

            // Reset wave numbers for next use
            foreach (Tile t in useable_tiles)
            {
                t.Wave_number = 0;
            }

            // -------------------------------------------------------------------------------------------------------------------------------------

            if (path.Count == 1) // Rocket is in tile where player/s are, determine the nearest player
            {
                var availablePlayersInTile = from p in Level.Players where p.Rectangle.Intersects(target.Rectangle) select p;
                availablePlayersInTile.OrderByDescending(d => SC.GetDistance(d.Position, Position));
                TankPlayerSprite targetPlayer = availablePlayersInTile.First();
                Color = targetPlayer.Color; // Set rocket's color acccording to player beeing seeked
                // Now the nearest alive player is determined, now determine to which direction should rocket rotate
                
                // Count angle and rotate
                double angleToPlayer = SC.GetAngle(Position, targetPlayer.CeneterPosition);
                if (SC.GetAngleDifference(angleToPlayer, Movement_angle) > SC.GetAngleDifference(angleToPlayer, Movement_angle + (game_time.ElapsedGameTime.TotalMilliseconds * Rotation_velocity)))
                    Movement_angle = Movement_angle + game_time.ElapsedGameTime.TotalMilliseconds * Rotation_velocity;
                else
                    Movement_angle = Movement_angle + (game_time.ElapsedGameTime.TotalMilliseconds * Rotation_velocity * (-1));
                ChangeVelocityDirection(Movement_angle);        
            }
            else // Rocket isn't in the tile containing player yet (path.count is grater than 1) !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! i have method for this
            {
                double targetAngle = 0; // 
                
                if (path[0].Rectangle.Y > path[1].Rectangle.Y) // target tile is up
                    targetAngle = 0;
                else if (path[0].Rectangle.Y < path[1].Rectangle.Y) // target tile is down
                    targetAngle = MathHelper.Pi;
                else if (path[0].Rectangle.X < path[1].Rectangle.X) // target tile is on the right
                    targetAngle = MathHelper.PiOver2;
                else if (path[0].Rectangle.X > path[1].Rectangle.X) // target tile is on the left
                    targetAngle = MathHelper.PiOver2 * 3; 
              
                if (SC.GetAngleDifference(targetAngle, Movement_angle) > SC.GetAngleDifference(targetAngle, Movement_angle + (game_time.ElapsedGameTime.TotalMilliseconds * Rotation_velocity)))
                    Movement_angle = Movement_angle + game_time.ElapsedGameTime.TotalMilliseconds * Rotation_velocity;
                else
                    Movement_angle = Movement_angle + (game_time.ElapsedGameTime.TotalMilliseconds * Rotation_velocity * (-1));
                ChangeVelocityDirection(Movement_angle);
            }

            // 
            Move(game_time);
        
        }

        public override void Move(GameTime gameTime)
        {
            Next_position = Position + new Vector2((float)(Velocity.X * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                   (float)(Velocity.Y * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio));

            bouncer.SetIM(Rectangle, new Rectangle((int)Next_position.X, (int)Next_position.Y, Rectangle.Width, Rectangle.Height));
            if (bouncer.RightTotalBounce()) // if horizontal bounce is true
            {
                Velocity_coefficient *= new Vector2(-1, 1);
                if (Universal_shot_bounce != null)
                    Universal_shot_bounce.Play();
            }

            if (bouncer.TopTotalBounce()) // if vertical bounce is true
            {
                Velocity_coefficient *= new Vector2(1, -1);
                if (Universal_shot_bounce != null)
                    Universal_shot_bounce.Play();
            }
            Movement_angle = SC.ArcusSinusCosinus(Velocity_coefficient * new Vector2(1, -1));
            centerPoint = new Vector2((float)(centerPoint.X + Velocity.X * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                (float)(centerPoint.Y + Velocity.Y * gameTime.ElapsedGameTime.TotalMilliseconds * SC.res_ratio));
            killingPoint = centerPoint + Velocity_coefficient * (Size / 2);

            // Recount position of the sprite
            Position = centerPoint - (Size / 2);
            Rotation = Movement_angle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.DrawRotated(spriteBatch);
        }

        public override bool IsKillingPlayer(TankPlayerSprite tank)
        {
            return tank.Rectangle.Intersects(new Rectangle((int)killingPoint.X, (int)killingPoint.Y, 1, 1));
        }


        

    }
}
