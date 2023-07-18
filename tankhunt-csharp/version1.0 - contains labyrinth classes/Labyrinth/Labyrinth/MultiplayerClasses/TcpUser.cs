using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Labyrinth
{
    public class TcpUser
    {
        TcpClient client = null;
        public List<string> Received_data { get; set; }
        public bool Connected { get { return client.Connected; } }
        public int ID =  -1;

        public TcpUser(int port, string server, string player_name)
        {
            Received_data = new List<string>();

            if (!AttemptConnection(port, server, player_name))
            {
                throw new Exception("Connection attempt failed!");
            }

                
           
        }

        public void AcceptAllData()
        {
            if (client.GetStream().DataAvailable)
            {
                 string data = StringSender.AcceptString(client);
                 Received_data = data.Split('$').ToList<String>();
           
            }
        }

        public string AcceptData()
        {
            if (client.GetStream().DataAvailable)
            {
                return StringSender.AcceptString(client);
            }
            return null;
        }

        public void SendData(string data)
        {
            if (data != "")
                StringSender.SendString(client, data);
        }

        public bool AttemptConnection(int port, string server, string player_name)
        {
            try
            {
                client = new TcpClient(server, port);
                StringSender.SendString(client, player_name);
                int id;
                while(!int.TryParse(AcceptData(), out id)) // Let's wait for id 
                {                }
                ID = id;
                client.NoDelay = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DisconnectClient()
        {
            try
            {
                client.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
