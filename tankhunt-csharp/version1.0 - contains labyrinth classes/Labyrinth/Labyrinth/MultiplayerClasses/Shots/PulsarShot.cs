using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Labyrinth
{
    public class PulsarShot : Shot
    {
        public Vector2 Shot_size { get; set; }
        public static SoundEffect Shot_sound { get; set; }
        private Rectangle shot_head;

       public PulsarShot(Vector2 position, Vector2 size, Color color, List<Sprite> walls, double player_rotation, Vector2 Shot_velocity, int time_to_remove, TankPlayerSprite owner)
            :base(Laser.shot_texture, position, size, color, walls, player_rotation, Shot_velocity, time_to_remove, owner)
        {
            Shot_sound.Play();
            Color = color;
            Bouncing = false;
            Origin = new Vector2(size.X / 2, size.Y * 0.2f);
            Rotation = player_rotation; 
        }

        public override void Update(GameTime game_time)
        {
            shot_head = new Rectangle(Rectangle.X, Rectangle.Y, 3, 3);

            foreach (Sprite w in walls)
            {
                if (shot_head.Intersects(w.Rectangle))
                    Delete = true;
            }

            base.Update(game_time);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.DrawRotated(spriteBatch);
        }
       
        public override bool IsKillingRectangle(Rectangle rect)
        {
            return rect.Intersects(shot_head);
          
        }


    }
}
