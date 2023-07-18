using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace TankHunt
{
    public class Shot : Sprite
    {
        public static int NextID { get; set; }
        public static SoundEffect Universal_shot_bounce { protected get; set; }
        public bool Play_shot_bounce_sound { get; protected set; }
        public Timer Shot_timer { get; private set; }
        public bool Bouncing { get; set; }

        protected double movement_angle;
        public double Movement_angle { get { return movement_angle; } set { movement_angle = SC.ClampAngle((float)value); } }


        public TankPlayerSprite owner { get; protected set; }
        public Vector2 Startup_position { get; protected set; }
        public bool Remove_after_kill { get; set; }
        public bool Kills_owner { get; set; }
        protected RandomLevel Level;

        protected Bouncer bouncer;

        public Shot(Texture2D texture, Vector2 position, Vector2 size, Color color, RandomLevel level, double player_rotation, Vector2 Shot_velocity, int time_to_remove, TankPlayerSprite owner)
            : base(texture, position, size, color)
        {
            Remove_after_kill = true;
            Velocity_coefficient = new Vector2((float)Math.Sin(player_rotation), (float)-Math.Cos(player_rotation));
            Startup_position = position;
            Movement_angle = player_rotation;
            Velocity_const = Shot_velocity;
            Shot_timer = new Timer(time_to_remove);
            Shot_timer.Tick += new EventHandler(Shot_timer_Tick);
            Shot_timer.Start();
            Net_ID = NextID;
            NextID++;
            Bouncing = true;
            bouncer = new Bouncer(level.Walls);
            this.Level = level;
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
                bouncer.SetIM(Rectangle, new Rectangle((int)Next_position.X, (int)Next_position.Y, Rectangle.Width, Rectangle.Height));
                if (bouncer.RightTotalBounce()) // if horizontal bounce is true
                {
                    Velocity_coefficient *= new Vector2(-1, 1);
                    if (Universal_shot_bounce != null)
                        Universal_shot_bounce.Play();
                }

                if (bouncer.TopTotalBounce()) // if vertical bounce is true
                {
                    Velocity_coefficient *= new Vector2(1, -1);
                    if (Universal_shot_bounce != null)
                        Universal_shot_bounce.Play();
                }
            }
            base.Move(gameTime);
        }

        public virtual bool IsKillingPlayer(TankPlayerSprite player)
        {
            return SC.CircleIntersectsCircle(player.Rectangle, Rectangle);
        }

    


    }
}
