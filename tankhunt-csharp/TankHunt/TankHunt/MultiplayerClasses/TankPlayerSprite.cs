using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class TankPlayerSprite : StoppingSprite
    {
        public string Player_name { get; set; }

        public Weapon Weapon { get; set; }
        public OneShotCannon One_shoot_cannon { get; set; }

        public static Texture2D tomb { get; set; }

        private int wins;
        public int Wins
        {
            get
            {
                return wins;
            }
            set
            {
                wins = value;
                if (WinsKillDeathPropertyChanged != null)
                    WinsKillDeathPropertyChanged(this, EventArgs.Empty);
            }
        }
        private int kills;
        public int Kills
        {
            get
            {
                return kills;
            }
            set
            {
                kills = value;
                if (WinsKillDeathPropertyChanged != null)
                    WinsKillDeathPropertyChanged(this, EventArgs.Empty);
            }
        }
        private int deaths;
        public int Deaths
        {
            get
            {
                return deaths;
            }
            set
            {
                deaths = value;
                if (WinsKillDeathPropertyChanged != null)
                    WinsKillDeathPropertyChanged(this, EventArgs.Empty);
            }
        }
        private int suicides;
        public int Suicides
        {
            get
            {
                return suicides;
            }
            set
            {
                suicides = value;
                if (WinsKillDeathPropertyChanged != null)
                    WinsKillDeathPropertyChanged(this, EventArgs.Empty);
            }
        }
        

        public bool Visible { get; set; }
        public bool IsAlive { get; set; }
        public bool Controlable { get; set; }
        public bool Observer { get; set; }
        public bool Keep_observing { get; set; }

        public Vector2 Default_forward_velocity_const { get; private set; }
        public Vector2 Default_backward_velocity_const { get; private set; }

        public Timer Invisibility_timer { get; private set; }
        private bool Can_cloak { get; set; }
        public int Invisibility_cooldown_time { get; private set; }
        public Timer Invisibility_cooldown_timer { get; private set; }

       

        public event EventHandler WinsKillDeathPropertyChanged;

        public Vector2 Shot_position
        {
            get
            {
                return (SC.GetCenter(Rectangle) - ((Size / 2) * new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)))) - new Vector2((float)-Math.Sin(Rotation) * 15, (float)Math.Cos(Rotation) * 15);
            }
        }

        public TankPlayerSprite(Texture2D texture, Vector2 position, Vector2 size, Color color, int writenumber)
            : base(texture, position, size, color, writenumber)
        {
            One_shoot_cannon = new OneShotCannon();
            IsAlive = true;
            Visible = false;
            Observer = false;
            Weapon = null;
            Wins = 0;
            Kills = 0;
            Deaths = 0;
            Invisibility_cooldown_time = 25000;
            Invisibility_timer = new Timer(6000);
            Invisibility_timer.Tick += new EventHandler(Invisibility_timer_Tick);
            Invisibility_cooldown_timer = new Timer(Invisibility_cooldown_time);
            Invisibility_cooldown_timer.Tick += new EventHandler(Invisibility_cooldown_timer_Tick);
            Can_cloak = true;
            Controlable = true;

            Default_forward_velocity_const = new Vector2(0.2f, 0.2f);
            Default_backward_velocity_const = new Vector2(0.15f, 0.15f);
        }


        /// <summary>
        /// Invisibles the player
        /// </summary>
        /// <param name="totally">Determines if player will be invisible also for this instance - true, or player will be invisible only for others - false</param>
        public bool StartInvisibility(bool totally, int interval)
        {
            if (!Can_cloak)
                return false;

            Invisibility_timer.Interval = interval;
            if (totally)
                Transparency = 0f;
            else
                Transparency = 0.5f;
            Invisibility_timer.Start();
            Can_cloak = false;
            return true;
        }

        public void StopInvisibility()
        {
            Can_cloak = true;
            Invisibility_timer.Stop();
            Invisibility_cooldown_timer.Start();
            Transparency = 1;
        }

        private void Invisibility_timer_Tick(object sender, EventArgs e)
        {
            Visible = true;
            Transparency = 1;
            Invisibility_timer.Stop();
            Invisibility_cooldown_timer.Start();
        }

        private void Invisibility_cooldown_timer_Tick(object sender, EventArgs e)
        {
            Can_cloak = true;
            Invisibility_cooldown_timer.Stop();
            
        }

        public Vector2 GetShotPosition(Vector2 shot_size, float distance)
        {
            return ((SC.GetCenter(Rectangle) - ((Size / 2) * new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)))) - new Vector2((float)-Math.Sin(Rotation) * distance, (float)Math.Cos(Rotation) * distance)) - shot_size / 2;
        }

        public void ResetStats()
        {
            Wins = 0;
            Kills = 0;
            Deaths = 0;
            Suicides = 0;
        }





    }
}
