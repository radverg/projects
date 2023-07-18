using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TankHunt
{
    public class FlatLaserShot : Shot
    {
        private List<Sprite> Elementar_laser_shots = new List<Sprite>();
        private Vector2 Shot_size { get; set; }
        public static SoundEffect Shot_sound { private get; set; }

        public FlatLaserShot(double angle, Vector2 startup_position, Vector2 shot_size, Color player_color, Vector2 shot_velocity, TankPlayerSprite ownerr)
            :base()
        {
            Velocity_coefficient = new Vector2((float)Math.Sin(angle), (float)-Math.Cos(angle));
            Play_shot_bounce_sound = false;
            Movement_angle = angle;
            Startup_position = startup_position;
            Position = startup_position;
            Shot_size = shot_size;
            Velocity_const = shot_velocity;
            Color = player_color;
            LaserShot.Shot_sound.Play();
            owner = ownerr;
            Remove_after_kill = false;

            Vector2 first_direction = new Vector2((float)Math.Sin(angle + MathHelper.PiOver2), (float)-Math.Cos(angle + MathHelper.PiOver2));
            Vector2 second_direction = new Vector2((float)Math.Sin(angle - MathHelper.PiOver2), (float)-Math.Cos(angle - MathHelper.PiOver2));

            for (int i = 0; i < 70 * SC.res_ratio; i++)
            {
                Sprite first = new Sprite(Laser.shot_texture, Startup_position + i * first_direction, Shot_size, player_color);
                first.Velocity_coefficient = Velocity_coefficient;
                first.Velocity_const = Velocity_const;
                Elementar_laser_shots.Add(first);


                Sprite second = new Sprite(Laser.shot_texture, Startup_position + i * second_direction, Shot_size, player_color);
                second.Velocity_coefficient = Velocity_coefficient;
                second.Velocity_const = Velocity_const;
                Elementar_laser_shots.Add(second);
            }


        }

        public override void Update(GameTime game_time)
        {
            foreach (Sprite s in Elementar_laser_shots)
            {
                s.Move(game_time);
                if (!s.Rectangle.Intersects(SC.Level_rectangle))
                    s.Delete = true;
            }
            Elementar_laser_shots.RemoveAll((s) => s.Delete);
            Delete = (Elementar_laser_shots.Count == 0); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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
            foreach (Sprite s in Elementar_laser_shots)
            {
                if (s.Rectangle.Intersects(player.Rectangle))
                    return true;
            }
            return false;
        }


    }
}
