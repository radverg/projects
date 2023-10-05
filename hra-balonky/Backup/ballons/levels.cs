using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace balonky
{
    public class levels
    {
        public int[] sestrelenocelk = new int[5];
        public int target;
        public bool jeAktivni;
        public int minutesp, secondp, minutesa, secondsa;
        public bool win;

        public levels(int minutes, int seconds, int targett)
        {
            minutesp = minutesa;
            minutesa = minutes;
            secondp = seconds;
            secondsa = seconds;
            target = targett; 
        }
    }
}
