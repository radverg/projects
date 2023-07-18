using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class OneShotCannon : Weapon
    {
        public static Texture2D Shot_texture;
        private Shot shot;

        public OneShotCannon()
            : base(1, WeaponItem.WeaponType.None)
        {
        }
    
        public override IEnumerable<Shot> Shoot(List<Sprite> walls, TankPlayerSprite player)
        {
            if (Remaining_shots_count != 0)
            {
                Remaining_shots_count--;
                Vector2 shot_size = new Vector2(Shot_texture.Width, Shot_texture.Height) * SC.resv_ratio;
                shot = new Shot(Shot_texture, player.Shot_position - (shot_size / 2), shot_size, Color.White, walls, player.Rotation, new Vector2(0.5f, 0.5f), 8000, player);
                shot.Shot_timer.Tick += new EventHandler(Shot_timer_Tick);
                return new Shot[] { shot };
            }
            else
            {
                return null;
            }
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, List<Sprite> walls)
        {
            shot = new Shot(Shot_texture, startup_pos, new Vector2(Shot_texture.Width, Shot_texture.Height) * SC.resv_ratio, Color.White, walls, angle, new Vector2(0.5f, 0.5f), 8000, owner);
            shot.Net_ID = shot_id;
            return new Shot[] { shot };
        }
         
        public void Update()
        {
            if (shot == null)
                return;

            if (shot.Delete)
            {
                shot = null;
                Remaining_shots_count += 1;
            }
        }

    

        void Shot_timer_Tick(object sender, EventArgs e)
        {
            shot.Delete = true;
            Remaining_shots_count += 1;           
        }

    }
}
