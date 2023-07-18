using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth
{
    public class ClientInfo
    {
        public string Client_name { get; set; }
        public byte ID { get; set; }

        public ClientInfo(string name, byte id)
        {
            Client_name = name;
            ID = id;
        }
            


    }
}
