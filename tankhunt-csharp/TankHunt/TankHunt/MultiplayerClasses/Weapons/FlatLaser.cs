using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
   
    public class FlatLaser : Weapon
    {
        public static Texture2D shot_texture;
        private FlatLaserShot laser_shot = null;
        private Vector2 Shot_size;
        private readonly Vector2 shot_velocity = new Vector2(0.66f, 0.66f);

        public FlatLaser()
            :base(1, WeaponItem.WeaponType.Laser)
        {
            Shot_size = SC.resv_ratio * new Vector2(7, 7);
        }

        public override IEnumerable<Shot> Shoot(RandomLevel level, TankPlayerSprite player)
        {
            if (laser_shot == null && SC.CheckKeyPressed((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false))
            {
                Remaining_shots_count--;
                Remove = (Remaining_shots_count <= 0);
                laser_shot = new FlatLaserShot(player.Rotation, player.Shot_position - (Shot_size / 2), Shot_size, player.Color, shot_velocity, player);
                return new FlatLaserShot[] { laser_shot };
            }
            return null;
            
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, RandomLevel level)
        {
            FlatLaserShot ls = new FlatLaserShot(angle, startup_pos, Shot_size, owner.Color, shot_velocity, owner);
            ls.Net_ID = shot_id;
            return new Shot[] { ls };
        }

    }
}
