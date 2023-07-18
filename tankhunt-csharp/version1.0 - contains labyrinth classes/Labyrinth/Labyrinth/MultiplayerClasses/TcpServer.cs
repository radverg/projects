using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    public class TcpServer
    {
        /// <summary>
        /// Struct that provides basic information about client : user name, id, and TcpClient instancion
        /// </summary>
        struct TcpClientUser
        {
            public TcpClient client;
            public string user_name;
            public int id;
        }

        struct DataPack
        {
            public string data;
            public TcpClient client_sender;
        }

        
        private TcpListener listener = null;
        public bool Running { get { return (listener != null); } } 
        private List<TcpClientUser> clients = new List<TcpClientUser>();
        public List<string> data = new List<string>();
        private List<TcpClientUser> To_remove = new List<TcpClientUser>();
        private List<int> Taken_IDs = new List<int>();
        
        private List<int> Allowed_IDs;

        public TcpServer()
        {
            int[] allowed = { 1, 2, 3, 4, 5 };
            Allowed_IDs = new List<int>(allowed);
        }

        /// <summary>
        /// Creates new server on specified port and runs it 
        /// </summary>
        /// <param name="port">Port which you want to start server on</param>
        public void CreateServer(int port)
        {          
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start(); 
            
        }

        /// <summary>
        /// Tries to accept any new client trying to connect to the server
        /// </summary>
        public void AcceptClient()
        {
            if (listener == null)
                return;

            if (listener.Pending())
            {
                int i = ReturnID(); // Prepare new id number for new player
                if (i != -1)
                {
                    TcpClient new_client = listener.AcceptTcpClient();
                    TcpClientUser tcpcu = new TcpClientUser { client = new_client, user_name = StringSender.AcceptString(new_client), id = i };
                    StringSender.SendString(new_client, tcpcu.id.ToString());
                    clients.Add(tcpcu);
                }
                else
                    throw new TooManyPlayersException();               
            }          
        }

        /// <summary>
        /// Accepts all data from clients found in clients list and saves data to data list
        /// </summary>
        public void ReceiveAllData()
        {
            foreach (TcpClientUser client_user in clients)
            {
                try
                {
                    while (client_user.client.GetStream().DataAvailable)
                        data.Add(StringSender.AcceptString(client_user.client));
                }
                catch
                {
                    To_remove.Add(client_user);
                }
            }
        }

        /// <summary>
        /// Sends all data found in data list to all clients found in clients list
        /// </summary>
        public void SendAllDataToAll()
        {
            foreach (TcpClientUser client_user in clients)
            {
                try
                {
                    if (data.Count > 0)
                       StringSender.SendString(client_user.client, string.Join("$", data));
                }
                catch
                {                   
                    To_remove.Add(client_user);
                }
            }
            data.Clear();
        }

        /// <summary>
        /// Removes players being on disconnected list
        /// </summary>
        public void RemoveDisconected()
        {
            foreach (TcpClientUser tcp in To_remove)
            {
                clients.Remove(tcp);
                Taken_IDs.Remove(tcp.id);
            }
            To_remove.Clear();
        }

        /// <summary>
        /// Gets player id that is still not taken
        /// </summary>
        /// <returns>ID, -1 if no id available</returns>
        public int ReturnID()
        {
            foreach (int i in Allowed_IDs)
            {
                if (!Taken_IDs.Contains(i))
                {
                    Taken_IDs.Add(i);
                    return i;
                }
            }
            return -1;
        }

       
    }
}
