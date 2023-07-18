using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Lidgren.Network;

namespace Labyrinth
{
    public class Client
    {
        private NetClient client_Lidgren;
        public List<NetBuffer> Received_data { get; set; }
        public List<NetOutgoingMessage> Outgoing_data = new List<NetOutgoingMessage>();
        public string Client_Name { get; set; }
        public int ID { get; set; }

        public Client(string name)
        {
            Client_Name = name;
            Received_data = new List<NetBuffer>();
            client_Lidgren = new NetClient(new NetPeerConfiguration(Server.APP_IDENTIFIER));
            client_Lidgren.Start();
        }

        private void Connect(IPEndPoint sender_end_point)
        {
            NetOutgoingMessage mess = client_Lidgren.CreateMessage();
            mess.Write(Client_Name);
            client_Lidgren.Connect(sender_end_point, mess);
        }

        public void SendData(NetBuffer net_buff)
        {
            NetOutgoingMessage mess = client_Lidgren.CreateMessage();
            mess.Write(net_buff.Data);
            Outgoing_data.Add(mess);
        }

        public void ReceiveData()
        {
            Received_data.Clear();
            NetIncomingMessage received_message;
 
            while ((received_message = client_Lidgren.ReadMessage()) != null)
            {
                switch (received_message.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        {
                            if (received_message.ReadByte() < Server.MAX_PEERS)
                            {
                                ID = received_message.ReadByte();
                                Shot.NextID = (ID + 1) * 1000000;
                                NetOutgoingMessage mess = client_Lidgren.CreateMessage();
                                Connect(received_message.SenderEndPoint); // Connect to server
                            }
                            else
                                throw new Exception("Server is full!");
                            break;
                        }
                    case NetIncomingMessageType.Data:
                        {
                            if (received_message.LengthBits >= 8)
                                Received_data.Add(received_message);

                            break;
                        }
                }
            }
        }

        public void SendData()
        {
            foreach (NetOutgoingMessage mess in Outgoing_data)
            {
                client_Lidgren.SendMessage(mess, NetDeliveryMethod.ReliableOrdered);
            }
            Outgoing_data.Clear();
        }

        public void AttemptConnection(string server_ip, int port)
        {
            client_Lidgren.DiscoverKnownPeer(server_ip, port);
            for (int i = 0; i < 10; i++)
			{
                ReceiveData(); // try to connect by checking for discovery response
                if (client_Lidgren.ConnectionStatus == NetConnectionStatus.Connected)
                    return;
                System.Threading.Thread.Sleep(200);                			 
			}

            throw new Exception("Unable to connect to server, because it doesn't respond to your connection request. \nServer may not exist or port used by server is being blocked.");
        }


        public void SendDataNow(NetBuffer buff, NetDeliveryMethod method)
        {
            NetOutgoingMessage mess = client_Lidgren.CreateMessage();
            mess.Write(buff);
            client_Lidgren.SendMessage(mess, method);
        }

        public void Disconnect()
        {
            client_Lidgren.Disconnect(Client_Name + " disconnecting!");
        }
    }
}
