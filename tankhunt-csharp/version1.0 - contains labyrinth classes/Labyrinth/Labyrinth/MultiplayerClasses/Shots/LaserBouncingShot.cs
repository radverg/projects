using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Labyrinth
{
    public class LaserBouncingShot : Shot
    {
        private List<Sprite> Elementar_laser_shots = new List<Sprite>();
        private readonly int Max_bounces = 8;
        private int Bounces_count = 0;
        public static SoundEffect Shot_sound { get; set; }

        public LaserBouncingShot(Vector2 position, Vector2 size, Color color, List<Sprite> walls, double player_rotation, Vector2 Shot_velocity, int time_to_remove, TankPlayerSprite owner)
            :base(Laser.shot_texture, position, size, color, walls, player_rotation, Shot_velocity, time_to_remove, owner)
        {
            Shot_sound.Play();
            Color = color;
        }

        public override void Update(GameTime game_time)
        {
            int iterations = (int)(game_time.ElapsedGameTime.TotalMilliseconds * Velocity_const.X);
            for (int i = 0; i < iterations; i++)
            {
                Next_position = Position + Velocity_coefficient;
                if (Next_position != Position)
                {
                    Sprite elementar_shot = new Sprite(Laser.shot_texture, Next_position, Size);
                    Rectangle = elementar_shot.Rectangle;
                    elementar_shot.Color = Color;
                    Elementar_laser_shots.Add(elementar_shot);

                    SetIM();
                    if (IM.RightI && !IM.RightPI) // if horizontal bounce is true
                    {
                        Velocity_coefficient *= new Vector2(-1, 1);
                        Bounces_count++;
                    }

                    if (IM.TopI && !IM.TopPI) // if vertical bounce is true
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

        public override bool IsKillingRectangle(Rectangle rect)
        {
            foreach (Sprite s in Elementar_laser_shots)
            {
                if (s.Rectangle.Intersects(rect))
                {
                    Delete = true;
                    return true;
                }
            }
            return false;
        }


    }
}
