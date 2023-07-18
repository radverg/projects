using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    public class ConnectionInfo
    {
        public string IP_adress { get; set; }
        public int Port { get; set; }
        public string Player_name { get; set; }
        public bool Server { get; set; }
        public int Max_players { get; set; }

        public byte Darkness_percentage { get; set; }

        public bool Vsync { get; set; }
        public bool StableFPS { get; set; }

        public Vector2 Max_min_square_size { get; set; }
        public Vector2 Max_min_size { get; set; }

        public bool Even_up { get; set; }

        public int Local_port { get; set; }

        public List<KeyValuePair<byte, float>> SpawnProbabilities = new List<KeyValuePair<byte,float>>();

    }
}
