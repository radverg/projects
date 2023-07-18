using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
   
    public class Laser : Weapon
    {
        public static Texture2D shot_texture;
        private LaserShot laser_shot = null;
        private Vector2 Shot_size;
        private readonly Vector2 shot_velocity = new Vector2(3, 3);

        public Laser()
            :base(1, WeaponItem.WeaponType.Laser)
        {
            Shot_size = SC.resv_ratio * new Vector2(7, 7);
        }

        public override IEnumerable<Shot> Shoot(List<Sprite> walls, TankPlayerSprite player)
        {
            if (laser_shot == null)
            {
                laser_shot = new LaserShot(player.Rotation, player.Shot_position - (Shot_size / 2), Shot_size, player.Color, shot_velocity, player);
                return new LaserShot[] { laser_shot };
            }
            return null;
            
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, List<Sprite> walls)
        {
            LaserShot ls = new LaserShot(angle, startup_pos, Shot_size, owner.Color, shot_velocity, owner);
            ls.Net_ID = shot_id;
            return new Shot[] { ls };
        }

    }
}
