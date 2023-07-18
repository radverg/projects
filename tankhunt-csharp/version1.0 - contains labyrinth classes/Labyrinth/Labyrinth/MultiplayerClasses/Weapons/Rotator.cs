using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth
{
    public class Rotator : Weapon
    {



        public Rotator(int max_shots)
            : base(max_shots)
        {
            Weapon_timer = new Timer(1);
        }



    }
}
