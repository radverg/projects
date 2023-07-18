using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace TankHunt
{
    public class Server
    {
        private int Max_peers;
        public const string APP_IDENTIFIER = "LABYRINTH_TANKS";
        private NetServer server_Lidgren;
        public List<NetBuffer> Received_data = new List<NetBuffer>();
        public List<NetOutgoingMessage> Outgoing_data = new List<NetOutgoingMessage>();
        private List<NetIncomingMessage> Forward_data = new List<NetIncomingMessage>();

        private List<int> used_ids = new List<int>();

     
        struct ClientInfo
        {
            public byte ID;
            public string Name;
        }

        public void CreateServer(int port, int max_peers)
        {
            NetPeerConfiguration config = new NetPeerConfiguration(APP_IDENTIFIER);
            config.Port = port;
            Max_peers = max_peers;
            config.MaximumConnections = max_peers - 1;
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            server_Lidgren = new NetServer(config);
            server_Lidgren.Start();
            Shot.NextID = 1000000;
            MessageLog.CreateMessage("Created server at port " + port.ToString());
        }

        public void Receive_data()
        {
            Received_data.Clear();
            NetIncomingMessage received_message;

            while ((received_message = server_Lidgren.ReadMessage()) != null)
            {
                switch (received_message.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        {
                            NetOutgoingMessage response = server_Lidgren.CreateMessage();
                            response.Write(server_Lidgren.ConnectionsCount < Max_peers);
                            response.Write(GetNextID()); // Write id to response, if -1, server is full
                            server_Lidgren.SendDiscoveryResponse(response, received_message.SenderEndPoint);
                            break;
                        }
                    case NetIncomingMessageType.Data:
                        {
                            if (received_message.LengthBits >= 8)
                            {
                                Received_data.Add(received_message);
                                Forward_data.Add(received_message);
                            }
                            break;
                        }
                    case NetIncomingMessageType.StatusChanged:
                        {
                            NetConnectionStatus status_type = (NetConnectionStatus)received_message.ReadByte();
                            if (status_type == NetConnectionStatus.Connected)
                            {
                                received_message.SenderConnection.Tag = new ClientInfo() { Name = received_message.SenderConnection.RemoteHailMessage.ReadString(), ID = received_message.SenderConnection.RemoteHailMessage.ReadByte() };
                                MessageLog.CreateMessage(string.Format("Connected {0} from {1}!", ((ClientInfo)received_message.SenderConnection.Tag).Name, received_message.SenderEndPoint));
                            }

                            if (status_type == NetConnectionStatus.Disconnected)
                            {
                                MessageLog.CreateMessage(string.Format("Player {0} disconnected!", ((ClientInfo)received_message.SenderConnection.Tag).Name));
                                used_ids.Remove(((ClientInfo)received_message.SenderConnection.Tag).ID); // Remove player id from used id's
                                NetBuffer buff = new NetBuffer();
                                buff.Write((byte)NetworkComponent.DataPacketType.PlayerDisconnected);
                                buff.Write((byte)((ClientInfo)received_message.SenderConnection.Tag).ID);
                                SendDataNow(buff, NetDeliveryMethod.ReliableOrdered);
                                Received_data.Add(buff);
                            }
                            break;
                        }
                }
            }
        }

        public void ForwardDataToClients()
        {
            foreach (NetIncomingMessage mess in Forward_data)
            {
                NetOutgoingMessage out_mess = server_Lidgren.CreateMessage();
                out_mess.Write(mess.Data);
                server_Lidgren.SendToAll(out_mess, mess.SenderConnection, mess.DeliveryMethod, 0);
            }
            Forward_data.Clear();
        }

        private int GetNextID()
        {           
            for (int i = 1; i < Max_peers + 1; i++)
            {
                if (!used_ids.Contains((byte)i))
                {
                    used_ids.Add(i);
                    return i;
                }
            }
            return -1;
        }      

        public void SendDataNow(NetBuffer buff, NetDeliveryMethod method)
        {
            NetOutgoingMessage mess = server_Lidgren.CreateMessage();
            mess.Write(buff);
            server_Lidgren.SendToAll(mess, method);
        }

        public void Disconnect()
        {
            server_Lidgren.Shutdown("Server disconnected!");
        }
    }
}
