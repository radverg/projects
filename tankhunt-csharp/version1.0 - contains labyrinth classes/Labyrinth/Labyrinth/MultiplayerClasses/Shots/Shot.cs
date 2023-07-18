using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Labyrinth
{
    public class Shot : BouncingMSprite
    {
        public static int NextID { get; set; }
        public static SoundEffect Universal_shot_bounce { get; set; }
        public Timer Shot_timer { get; set; }
        public bool Bouncing { get; set; }
        public double Movement_angle { get; set; }
        public TankPlayerSprite owner { get; set; }
        public Vector2 Startup_position { get; set; }
        public bool Remove_after_kill { get; set; }

        public Shot(Texture2D texture, Vector2 position, Vector2 size, Color color, List<Sprite> walls, double player_rotation, Vector2 Shot_velocity, int time_to_remove, TankPlayerSprite owner)
            : base(texture, position, size, color, walls)
        {
            Remove_after_kill = true;
            Velocity_coefficient = new Vector2((float)Math.Sin(player_rotation), (float)-Math.Cos(player_rotation));
            Startup_position = position;
            Movement_angle = player_rotation;
            Velocity_const = Shot_velocity * SC.resv_ratio;
            Shot_timer = new Timer(time_to_remove);
            Shot_timer.Tick += new EventHandler(Shot_timer_Tick);
            Shot_timer.Start();
            Net_ID = NextID;
            NextID++;
            Bouncing = true;
            this.owner = owner;
        }

        public Shot()
            :base()
        {
            Net_ID = NextID;
            NextID++;
        }

        void Shot_timer_Tick(object sender, EventArgs e)
        {
            Delete = true;
        }

        public virtual void Update(GameTime game_time)
        {
            Next_position = Position + new Vector2((float)(Velocity.X * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio),
                    (float)(Velocity.Y * game_time.ElapsedGameTime.TotalMilliseconds * SC.res_ratio));

            Move(game_time);
        
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Move(GameTime gameTime)
        {
            if (Bouncing)
            {
                SetIM();
                if (IM.RightI && !IM.RightPI) // if horizontal bounce is true
                    Velocity_coefficient *= new Vector2(-1, 1);

                if (IM.TopI && !IM.TopPI) // if vertical bounce is true
                    Velocity_coefficient *= new Vector2(1, -1);
            }
            base.Move(gameTime);
        }

        public virtual bool IsKillingRectangle(Rectangle rect)
        {
            return rect.Intersects(Rectangle);
        }

    


    }
}
