using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class Timer
    {
        private double Temporary_time { get; set; }
        public double Interval { get; set; }
        public bool Running { get; private set; }
        public bool IsTicked { get; private set; }

        public event EventHandler Tick;

        public Timer(int interval)
        {
            Interval = interval;
            Running = false;
        }

       /// <summary>
       /// Updates and checks timer tick, calls tick event if timer ticked
       /// </summary>
       /// <param name="game_time">the time from last frame</param>
        public void Update(GameTime game_time)
        {
            if (Running)
            {
                IsTicked = false;
                Temporary_time += game_time.ElapsedGameTime.TotalMilliseconds;
                if (Temporary_time >= Interval)
                {
                    IsTicked = true;
                    if (Tick != null)
                       Tick(this, EventArgs.Empty);
                    Temporary_time = 0;
                }
            }
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start()
        {
            Temporary_time = 0;
            Running = true;
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            Running = false;
            IsTicked = false;
        }
           

    }
}
