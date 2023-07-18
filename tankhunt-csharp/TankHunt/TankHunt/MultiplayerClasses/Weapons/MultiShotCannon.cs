using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class MultiShotCannon : Weapon
    {
        private readonly Vector2 Shot_size = new Vector2(6, 6) * SC.resv_ratio;
        private readonly Vector2 Shot_velocity = new Vector2(0.33f, 0.33f);

        public MultiShotCannon()
            :base(10, WeaponItem.WeaponType.MultiShotCannon)
        {
            Weapon_timer = new Timer(200);
        }
        public override IEnumerable<Shot> Shoot(RandomLevel level, TankPlayerSprite player)
        {
            if (SC.CheckKeyPressed((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false) && !Weapon_timer.Running) // Run weapon after pressing d key
                Weapon_timer.Start();

            if (SC.CheckKeyDropped((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false) && Weapon_timer.Running) // Stop weapon after dropping d key
                Remove = true;

            if (Remaining_shots_count != 0 && Weapon_timer.IsTicked)
            {
                Remaining_shots_count--;
                Shot shot = new Shot(OneShotCannon.Shot_texture, player.GetShotPosition(Shot_size, 17f * SC.res_ratio),
                    Shot_size, Color.White, level, SC.GenerateAngle(MathHelper.ToDegrees((float)player.Rotation) - 5, MathHelper.ToDegrees((float)player.Rotation) + 5), Shot_velocity, 8000, player);
                return new Shot[] { shot };
            }
            else if (Remaining_shots_count == 0)
                Remove = true;

            return null;           
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, RandomLevel level)
        {
            Shot shot = new Shot(OneShotCannon.Shot_texture, startup_pos, Shot_size, Color.White, level, angle, Shot_velocity, 8000, owner);
            shot.Net_ID = shot_id;
            return new Shot[] { shot };

        }

    }
}
