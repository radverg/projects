using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class TankPlayerSprite : StoppingSprite
    {
        public string Player_name { get; set; }
        public Weapon Weapon { get; set; }
        public OneShotCannon One_shoot_cannon { get; set; }

        private int wins;
        public int Wins
        {
            get
            {
                return wins;
            }
            set
            {
                if (WinsKillDeathPropertyChanged != null)
                    WinsKillDeathPropertyChanged(this, EventArgs.Empty);
                wins = value;
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
                if (WinsKillDeathPropertyChanged != null)
                    WinsKillDeathPropertyChanged(this, EventArgs.Empty);
                kills = value;
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
                if (WinsKillDeathPropertyChanged != null)
                    WinsKillDeathPropertyChanged(this, EventArgs.Empty);
                deaths = value;
            }
        }

        public bool Visible { get; set; }
        public bool IsAlive { get; set; }

        public Timer Invisibility_timer { get; private set; }
        private bool Can_cloak { get; set; }
        private int Invisibility_cooldown_time { get; set; }
        public Timer Invisibility_cooldown_timer { get; private set; }

       

        public event EventHandler WinsKillDeathPropertyChanged;

        public Vector2 Shot_position
        {
            get
            {
                return (SC.GetCenter(Rectangle) - ((Size / 2) * new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)))) - new Vector2((float)-Math.Sin(Rotation) * 10, (float)Math.Cos(Rotation) * 10);
            }
        }

        public TankPlayerSprite(Texture2D texture, Vector2 position, Vector2 size, Color color, int writenumber)
            : base(texture, position, size, color, writenumber)
        {
            One_shoot_cannon = new OneShotCannon();
            IsAlive = true;
            Visible = true;
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
                Visible = false;
            else
                Transparency = 0.5f;
            Invisibility_timer.Start();
            Can_cloak = false;
            return true;
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






    }
}
