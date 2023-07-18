using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunt
{
    public class Eliminator : Weapon
    {
        public static Texture2D Elementar_shot_texture { private get; set; }
        public Shot Parent_shot { get; private set; }
        public bool Can_splash { get; set; }
        private readonly Vector2 Shot_size = new Vector2(10, 10) * SC.resv_ratio;
        private readonly Vector2 Parent_shot_size = new Vector2(14, 14) * SC.resv_ratio;

        public Eliminator()
            :base(45, WeaponItem.WeaponType.Eliminator)
        {
            Weapon_timer = new Timer(8000);
            Can_splash = false;
        }

        public override IEnumerable<Shot> Shoot(RandomLevel level, TankPlayerSprite player)
        {
            if (!Weapon_timer.Running && SC.CheckKeyPressed((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false))
            {
                Weapon_timer.Start();
                Parent_shot = new Shot(OneShotCannon.Shot_texture, player.GetShotPosition(Shot_size, 22f * SC.res_ratio),
                    Parent_shot_size, Color.White, level, player.Rotation, new Vector2(0.33f, 0.33f), 8000, player);
                return new Shot[] { Parent_shot };
            }
            else if (Weapon_timer.IsTicked || (SC.CheckKeyDropped((Microsoft.Xna.Framework.Input.Keys)SC.KeysAssociation["ashot"], false) && Weapon_timer.Running))
            {
                Parent_shot.Delete = true;
                Remove = true;
                return GetShots(Parent_shot.Position, Remaining_shots_count, level, player);
            }
            return null;
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, RandomLevel level)
        {
            if (!Can_splash)
            {
                Can_splash = true;
                Parent_shot = new Shot(OneShotCannon.Shot_texture, startup_pos,
                    Parent_shot_size, Color.White, level, angle, new Vector2(0.33f, 0.33f), 8000, owner);
                Parent_shot.Net_ID = shot_id;
                return new Shot[] { Parent_shot };
            }
            else
            {
                Parent_shot.Delete = true;
                return GetShots(startup_pos, Remaining_shots_count, level, owner);
            }
        }

        private IEnumerable<Shot> GetShots(Vector2 startup_pos, int count, RandomLevel level, TankPlayerSprite owner)
        {
            Shot[] shots = new Shot[Remaining_shots_count];
            int coeff = (int)(MathHelper.Distance((float)((double)startup_pos.X / (double)DataTranslator.Level_absolute_size.X), (float)((double)startup_pos.Y / (double)DataTranslator.Level_absolute_size.Y)) * 1000);


            for (int i = 1; i < shots.Length + 1; i++)
            {
                double anglee = Math.Sin(MathHelper.ToRadians(coeff * (float)Math.Cos(i))) * 10;
                float shot_velocity = 6 + 5 * (float)Math.Cos(MathHelper.ToRadians(coeff * (i + (i / (i + 1)))));

                shots[i - 1] = new Shot(Elementar_shot_texture, startup_pos,
                    Shot_size, Color.White, level, anglee, new Vector2(shot_velocity) * 0.1f, 8000, owner);
                shots[i - 1].Bouncing = false;
                shots[i - 1].Net_ID = Parent_shot.Net_ID;
            }
            return shots;


        }

    }

}
