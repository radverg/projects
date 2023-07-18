using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class Pulsar : Weapon
    {
        public static Texture2D Shot_texture { get; set; }
        private readonly Vector2 Shot_size = new Vector2(5, 25) * SC.resv_ratio;
        private readonly Vector2 shot_velocity = new Vector2(1.5f, 1.5f);

        public Pulsar()
            :base(20, WeaponItem.WeaponType.Pulsar)
        {
            Weapon_timer = new Timer(100);
        }


        public override IEnumerable<Shot> Shoot(RandomLevel level, TankPlayerSprite player)
        {
            if (SC.CheckKeyPressed((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false) && !Weapon_timer.Running)
                Weapon_timer.Start();

            if (SC.CheckKeyDropped((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false) && Weapon_timer.Running)
                Remove = true;

            if (Weapon_timer.IsTicked && Remaining_shots_count > 0)
            {
                Remaining_shots_count--;
                PulsarShot sh = new PulsarShot(player.Shot_position - (new Vector2(Shot_size.X, Shot_size.X) / 2), Shot_size, Color.Black, level, SC.GenerateAngle(MathHelper.ToDegrees((float)player.Rotation) - 8, MathHelper.ToDegrees((float)player.Rotation) + 8)
                    , shot_velocity, 3000, player);
                return new Shot[] { sh };
            }
            else if (Remaining_shots_count <= 0)
                Remove = true;

            return null;
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, RandomLevel level)
        {
            PulsarShot sh = new PulsarShot(startup_pos, Shot_size, Color.Black, level, angle, shot_velocity, 3000, owner);
            sh.Net_ID = shot_id;
            return new Shot[] { sh };


        }
    }
}
