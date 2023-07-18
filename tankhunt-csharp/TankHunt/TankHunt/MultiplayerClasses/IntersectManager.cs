using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankHunt
{
    public class IntersectManager
    {
        public bool RightI { get; set; }
        public bool RightPI { get; set; }

        public bool LeftI { get; set; }
        public bool LeftPI { get; set; }

        public bool TopI { get; set; }
        public bool TopPI { get; set; }

        public bool DownI { get; set; }
        public bool DownPI { get; set; }

        public IntersectManager()
        {
            SetAll(false);
        }

        public void SetAll(bool value)
        {
            RightI = value;
            RightPI = value;
            LeftI = value;
            LeftPI = value;
            TopI = value;
            TopPI = value;
            DownI = value;
            DownPI = value;

        }

        public void SetAllI(bool value)
        {
            RightI = value;
            LeftI = value;
            TopI = value;
            DownI = value;
        }

        public void SetPreviousStatus()
        {
            RightPI = RightI;
            LeftPI = LeftI;
            TopPI = TopI;
            DownPI = DownI;
        }

    }
}
