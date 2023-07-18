using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
   
    public class LaserBouncing : Weapon
    {
        public static Texture2D shot_texture;
        private LaserBouncingShot laser_bouncing_shot = null;
        private Vector2 Shot_size;
        private List<Vector2> Prediction_positions = new List<Vector2>();
        private readonly int Max_prediction_bounces = 4;
        private readonly Vector2 prediction_shot_size = new Vector2(6, 6) * SC.resv_ratio;
        private Bouncer bouncer;

        public LaserBouncing()
            :base(1, WeaponItem.WeaponType.Laser)
        {
            Shot_size = SC.resv_ratio * new Vector2(7, 7);
        }

        public override IEnumerable<Shot> Shoot(List<Sprite> walls, TankPlayerSprite player)
        {
            if (laser_bouncing_shot == null)
            {
                laser_bouncing_shot = new LaserBouncingShot(player.Shot_position - (Shot_size / 2), Shot_size, player.Color, walls, player.Rotation, new Vector2(4, 4), 8000, player);
                return new LaserBouncingShot[] { laser_bouncing_shot };
            }
            return null;
            
        }

        public override IEnumerable<Shot> NetShoot(Vector2 startup_pos, double angle, TankPlayerSprite owner, int shot_id, List<Sprite> walls)
        {
            laser_bouncing_shot = new LaserBouncingShot(startup_pos, Shot_size, owner.Color, walls, angle, new Vector2(4, 4), 8000, owner);
            laser_bouncing_shot.Net_ID = shot_id;
            return new LaserBouncingShot[] { laser_bouncing_shot };
        }

        public void Update(List<Sprite> walls, TankPlayerSprite player)
        {
            Prediction_positions.Clear();
            int prediction_bounces = 0;
            if (bouncer == null)
                bouncer = new Bouncer(walls);
            Vector2 Vel_coeff = player.Velocity_coefficient;
            Vector2 current = player.Shot_position - (prediction_shot_size / 2);
            Vector2 next;
            while ((prediction_bounces != Max_prediction_bounces) && Prediction_positions.Count < 600 * SC.res_ratio)
            {
                next = current + Vel_coeff;

                bouncer.SetIM(new Rectangle((int)current.X, (int)current.Y, (int)prediction_shot_size.X, (int)prediction_shot_size.Y), 
                    new Rectangle((int)next.X, (int)next.Y, (int)prediction_shot_size.X, (int)prediction_shot_size.Y));

                if (bouncer.RightTotalBounce()) // if horizontal bounce is true
                {
                    Vel_coeff *= new Vector2(-1, 1);
                    prediction_bounces++;
                }

                if (bouncer.TopTotalBounce()) // if vertical bounce is true
                {
                    Vel_coeff *= new Vector2(1, -1);
                    prediction_bounces++;
                }

                current = next;
                Prediction_positions.Add(next);
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            foreach (Vector2 v in Prediction_positions)
            {
                sprite_batch.Draw(Laser.shot_texture, new Rectangle((int)v.X, (int)v.Y, (int)prediction_shot_size.X, (int)prediction_shot_size.Y), Color.Black * 0.05f);
            }
	
        }

            

            


    

    }
}
