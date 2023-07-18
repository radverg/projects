using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class WeaponItem : Sprite
    {
        public enum WeaponType { None, Eliminator, MultiShotCannon, Laser, LaserBouncing, Pulsar, FlatLaser, Mine, DoubleMine, RocketLauncher };
        public Weapon Weapon { get; set; }
        public WeaponType Weapon_type { get; set; }

        public static int Next_id = 20;

        public WeaponItem(WeaponType weapon_type, Weapon weapon, Texture2D texture, Vector2 position, Vector2 size)
            :base(texture, position, size)
        {
            Weapon_type = weapon_type;
            Weapon = weapon;
            Net_ID = Next_id;
            Next_id++;
        }

    }
}
