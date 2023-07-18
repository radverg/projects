using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Labyrinth_editor
{
    public class Timer
    {
        public GameTime gametime;
        double temporary_time;
        double interval;
        public bool active;

        public Timer(int interval)
        {
            this.interval = interval;
            active = false;
        }

        public bool Check_tick()
        {
            if (active)
            {
                temporary_time += gametime.ElapsedGameTime.TotalMilliseconds;
                if (temporary_time >= interval)
                {
                    temporary_time = 0;
                    return true;
                }
                else
                    return false;

            }
            else
                return false;
        }
    }
}
