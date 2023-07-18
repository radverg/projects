using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace TankHunt
{
    public class Mine : Shot
    {
        public static Texture2D Shot_texture { get; set; }
        public static SoundEffect Burrow_sound { get; set; }

        private bool Activated = false;

        public Mine(Vector2 startup_pos, Vector2 shot_size, TankPlayerSprite owner)
            :base()
        {
            Position = startup_pos;
            Size = shot_size;
            Transparency = 1;
            base.owner = owner;
            Texture = Shot_texture;
            Remove_after_kill = true;
            Startup_position = startup_pos;
            Burrow_sound.Play();
        }

        public override void Update(GameTime game_time)
        {
            if (Transparency >= 0 && !Activated)
                Transparency -= (float)(0.001f * game_time.ElapsedGameTime.TotalMilliseconds);

            if (Transparency < 0)
            {
                Transparency = 0;
                Activated = true;
            }
        }

        public override bool IsKillingPlayer(TankPlayerSprite player)
        {
            if (player.Rectangle.Intersects(Rectangle) && Transparency == 0)
                return true;
            return false;
        }


    }
}
