using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TankHunt
{
    public class LaserBouncingShot : Shot
    {
        private List<Sprite> Elementar_laser_shots = new List<Sprite>();
        private readonly int Max_bounces = 8;
        private int Bounces_count = 0;
        public static SoundEffect Shot_sound { private get; set; }
        private double iterations = 0;

        public LaserBouncingShot(Vector2 position, Vector2 size, Color color, RandomLevel level, double player_rotation, Vector2 Shot_velocity, int time_to_remove, TankPlayerSprite owner)
            :base(Laser.shot_texture, position, size, color, level, player_rotation, Shot_velocity, time_to_remove, owner)
        {
            Shot_sound.Play();
            Color = color;
            Kills_owner = false;
        }

        public override void Update(GameTime game_time)
        {
            iterations += game_time.ElapsedGameTime.TotalMilliseconds * Velocity_const.X * SC.res_ratio;
            int real_iterations = (int)Math.Floor(iterations);
            for (int i = 0; i < real_iterations; i++)
            {
                iterations--;
                Next_position = Position + Velocity_coefficient;
                if (Next_position != Position)
                {
                    Sprite elementar_shot = new Sprite(Laser.shot_texture, Next_position, Size);
                    Rectangle = elementar_shot.Rectangle;
                    elementar_shot.Color = Color;
                    Elementar_laser_shots.Add(elementar_shot);

                    bouncer.SetIM(Rectangle, new Rectangle((int)(Next_position.X + Velocity_coefficient.X), (int)(Next_position.Y + Velocity_coefficient.Y), Rectangle.Width, Rectangle.Height));
                    if (bouncer.RightTotalBounce()) // if horizontal bounce is true
                    {
                        Velocity_coefficient *= new Vector2(-1, 1);
                        Bounces_count++;
                    }

                    if (bouncer.TopTotalBounce()) // if vertical bounce is true
                    {
                        Velocity_coefficient *= new Vector2(1, -1);
                        Bounces_count++;
                    }
                }
                Position = Next_position;
                if (Bounces_count == Max_bounces || Elementar_laser_shots.Count > 5000)
                    Delete = true;
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
            if (Elementar_laser_shots.Count > 0)
                if (player == owner && !Elementar_laser_shots.Last().Rectangle.Intersects(player.Rectangle))
                    return false;

            foreach (Sprite s in Elementar_laser_shots)
            {
                if (s.Rectangle.Intersects(player.Rectangle))
                {
                    Delete = true;
                    return true;
                }
            }
            return false;
        }


    }
}
