using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TankHunt
{
    public class LaserShot : Shot
    {
        private List<Sprite> Elementar_laser_shots = new List<Sprite>();
        public Vector2 Shot_size { get; set; }
        public static SoundEffect Shot_sound { get; set; }

        private double iterations = 0;

        public LaserShot(double angle, Vector2 startup_position, Vector2 shot_size, Color player_color, Vector2 shot_velocity, TankPlayerSprite ownerr)
            :base()
        {
            Velocity_coefficient = new Vector2((float)Math.Sin(angle), (float)-Math.Cos(angle));
            Movement_angle = angle;
            Startup_position = startup_position;
            Position = startup_position;
            Shot_size = shot_size;
            Velocity_const = shot_velocity * SC.resv_ratio;
            Color = player_color;
            Shot_sound.Play();
            owner = ownerr;
            Remove_after_kill = false;
            Kills_owner = false;

        }

        public override void Update(GameTime game_time)
        {
            iterations += game_time.ElapsedGameTime.TotalMilliseconds * Velocity_const.X * SC.res_ratio;
            int real_iterations = (int)Math.Floor(iterations); 
            for (int i = 0; i < iterations; i++)
            {
                iterations--;
                Vector2 next_position = Position + Velocity_coefficient;
                if (next_position != Position)
                {
                    
                    Sprite elementar_shot = new Sprite(Laser.shot_texture, next_position, Shot_size);
                    elementar_shot.Color = Color;
                    Delete = !elementar_shot.Rectangle.Intersects(SC.Level_rectangle);                   
                    Elementar_laser_shots.Add(elementar_shot);
                   
                }
                Position = next_position;
                
            }


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite s in Elementar_laser_shots)
            {
                s.Draw(spriteBatch);
            }
            
        }

        public override bool IsKillingPlayer(TankPlayerSprite player)
        {
            if (player == owner && !Kills_owner)
                return false;

            foreach (Sprite s in Elementar_laser_shots)
            {
                if (s.Rectangle.Intersects(player.Rectangle))
                    return true;
            }
            return false;
        }


    }
}
