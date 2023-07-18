using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class RocketLauncher : Weapon
    {
        public static Texture2D Shot_texture { get; set; }
        private readonly Vector2 Shot_size = new Vector2(36, 36) * SC.resv_ratio;
        private readonly Vector2 shot_velocity = new Vector2(0.29f, 0.29f);

        public RocketLauncher()
            : base(1, WeaponItem.WeaponType.RocketLauncher)
        {

        }


        public override IEnumerable<Shot> Shoot(RandomLevel level, TankPlayerSprite player)
        {
            if (SC.CheckKeyPressed((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false) && Remaining_shots_count > 0)
            {
                Remaining_shots_count--;
                Rocket sh = new Rocket(player.GetShotPosition(Vector2.Zero, 50f * SC.res_ratio), Shot_size, Color.Black, level, SC.GenerateAngle(MathHelper.ToDegrees((float)player.Rotation) - 8, MathHelper.ToDegrees((float)player.Rotation) + 8)
                    , shot_velocity, 15000, player);
                return new Shot[] { sh };
            }
            else if (Remaining_shots_count <= 0)
                Remove = true;

            return null;
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, RandomLevel level)
        {
            Rocket sh = new Rocket(startup_pos, Shot_size, Color.Black, level, angle, shot_velocity, 15000, owner);
            sh.Net_ID = shot_id;
            return new Shot[] { sh };


        }
    }
}
