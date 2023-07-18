using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public class IDManager
    {
        private static List<int> takenIDs = new List<int>();

        public static int GetNextID()
        {
            for (int i = 0; true; i++)
            {
                if (!takenIDs.Contains(i))
                {
                    takenIDs.Add(i);
                    return i;
                }
            }
        }

    }
}
