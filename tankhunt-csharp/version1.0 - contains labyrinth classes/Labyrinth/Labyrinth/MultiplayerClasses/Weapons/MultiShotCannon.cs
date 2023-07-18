using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class MultiShotCannon : Weapon
    {
        private readonly Vector2 Shot_size = new Vector2(6, 6) * SC.resv_ratio;
        private readonly Vector2 Shot_velocity = new Vector2(0.5f, 0.5f);

        public MultiShotCannon()
            :base(10, WeaponItem.WeaponType.MultiShotCannon)
        {
            Weapon_timer = new Timer(200);
            Weapon_timer.Start();
        }
        public override IEnumerable<Shot> Shoot(List<Sprite> walls, TankPlayerSprite player)
        {
            if (Remaining_shots_count != 0 && Weapon_timer.IsTicked)
            {
                Remaining_shots_count--;
                Shot shot = new Shot(OneShotCannon.Shot_texture, player.Shot_position,
                    Shot_size, Color.White, walls, SC.GenerateAngle(MathHelper.ToDegrees((float)player.Rotation) - 5, MathHelper.ToDegrees((float)player.Rotation) + 5), Shot_velocity, 8000, player);
                return new Shot[] { shot };
            }
            else if (Remaining_shots_count == 0)
                Remove = true;

            return null;
            
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, List<Sprite> walls)
        {
            Shot shot = new Shot(OneShotCannon.Shot_texture, startup_pos, Shot_size, Color.White, walls, angle, Shot_velocity, 8000, owner);
            shot.Net_ID = shot_id;
            return new Shot[] { shot };

        }

    }
}
