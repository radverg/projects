using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public abstract class Weapon
    {
        public int Remaining_shots_count { get; protected set; }
        public int Maximum_shots_count { get; set; }
        public Timer Weapon_timer { get; protected set; }
        public bool Remove { get; set; }
        public WeaponItem.WeaponType Weapon_type { get; set; }
        public TankPlayerSprite Weapon_owner { get; set; }

        protected List<Sprite> Walls { get; private set; }

        public Weapon(int Maximum_shots, WeaponItem.WeaponType weapon_type)
        {
            Maximum_shots_count = Maximum_shots;
            Remaining_shots_count = Maximum_shots;
            Remove = false;
            Weapon_type = weapon_type;
        }

        public abstract IEnumerable<Shot> Shoot(List<Sprite> walls, TankPlayerSprite player);
        public abstract IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, List<Sprite> walls);
    }
}
