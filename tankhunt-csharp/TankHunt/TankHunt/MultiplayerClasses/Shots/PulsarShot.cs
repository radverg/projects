using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TankHunt
{
    public class PulsarShot : Shot
    {
        public static SoundEffect Shot_sound { private get; set; }
        private Rectangle shot_head;

        public PulsarShot(Vector2 position, Vector2 size, Color color, RandomLevel level, double player_rotation, Vector2 Shot_velocity, int time_to_remove, TankPlayerSprite owner)
            :base(Laser.shot_texture, position, size, color, level, player_rotation, Shot_velocity, time_to_remove, owner)
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

            foreach (Sprite w in Level.Walls)
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
       
        public override bool IsKillingPlayer(TankPlayerSprite player)
        {
            return player.Rectangle.Intersects(shot_head);
          
        }


    }
}
