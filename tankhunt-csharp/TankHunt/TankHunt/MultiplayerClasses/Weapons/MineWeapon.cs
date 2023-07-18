using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class MineWeapon : Weapon
    {
        private readonly Vector2 Shot_size = new Vector2(45, 45) * SC.resv_ratio;

        public MineWeapon(int count)
            : base(count, WeaponItem.WeaponType.Mine)
        {

        }

        public override IEnumerable<Shot> Shoot(RandomLevel level, TankPlayerSprite player)
        {
            if (Remaining_shots_count <= 0)
            {
                Remove = true;
                return null;
            }

            if (SC.CheckKeyPressed((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false))
            {
                Mine shot = new Mine(player.GetShotPosition(Shot_size, 40f * SC.res_ratio), Shot_size, player);
                Remaining_shots_count--;
                return new Shot[] { shot };
            }
            return null;
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, RandomLevel level)
        {
            Mine shot = new Mine(startup_pos, Shot_size, owner);
            shot.Net_ID = shot_id;
            return new Shot[] { shot };
        }


    }
}
