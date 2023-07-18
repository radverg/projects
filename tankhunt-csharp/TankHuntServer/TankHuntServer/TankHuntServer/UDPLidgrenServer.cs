using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.Threading;

namespace TankHuntServer
{
    class UDPLidgrenServer
    {
        public enum DataPacketType
        {
            LevelUpdate, PlayerAllInfoUpdate, PositionAndRotationUpdate, Shot, Invisibility, NewWeaponItem,
            OneShootCannonShot, WeaponShot, WeaponItemCollect, PlayerDied, PlayerDisconnected, NewLevelRequest, ServerTimeRequest, ServerTime, ChatMessage, LeadingClientPromotion
        }

        public NetServer serverLidgren { get; private set; }
        private int maxPeers = 255;
        public const string APP_IDENTIFIER = "LABYRINTH_TANKS";
        public MessageLog messageLog = new MessageLog();
        private List<int> usedIds = new List<int>();
        private List<NetBuffer> messagesForSend = new List<NetBuffer>();
        private object pseudoObject = new object();
        Thread readT;

        struct ClientInfo
        {
            public byte ID;
            public string Name;
        }

        public UDPLidgrenServer()
        {

        }

        public void CreateServer(int port)
        {
            if (serverLidgren == null)
            {
                NetPeerConfiguration npc = new NetPeerConfiguration(APP_IDENTIFIER);
                npc.Port = port;

                serverLidgren = new NetServer(npc);
                serverLidgren.Start();
                readT = new Thread(ReadSendLoop);
                readT.IsBackground = true;
                readT.Start();
                messageLog.CreateMessage("Server started at port " + port.ToString());

            }
        }

        public void StopServer()
        {
            if (serverLidgren == null)
                return;

            readT.Abort();
            serverLidgren.Shutdown("shutdown");
            serverLidgren = null;
            messageLog.CreateMessage("Server has been successfully stopped!");
        }

        public void ReadData()
        {

            NetIncomingMessage received_message;
            while ((received_message = serverLidgren.ReadMessage()) != null)
            {
                switch (received_message.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        {
                            NetOutgoingMessage response = serverLidgren.CreateMessage();
                            response.Write(true);
                            response.Write(GetNextID()); // Write id to response, if -1, server is full
                            serverLidgren.SendDiscoveryResponse(response, received_message.SenderEndPoint);
                            break;
                        }
                    case NetIncomingMessageType.Data:
                        {
                            if (received_message.LengthBits >= 8)
                            {
                                messagesForSend.Add(received_message);
                            }
                            break;
                        }
                    case NetIncomingMessageType.StatusChanged:
                        {
                            NetConnectionStatus status_type = (NetConnectionStatus)received_message.ReadByte();
                            if (status_type == NetConnectionStatus.Connected)
                            {
                                received_message.SenderConnection.Tag = new ClientInfo() { Name = received_message.SenderConnection.RemoteHailMessage.ReadString(), ID = received_message.SenderConnection.RemoteHailMessage.ReadByte() };
                                messageLog.CreateMessage(string.Format("Connected {0} from {1}!", ((ClientInfo)received_message.SenderConnection.Tag).Name, received_message.SenderEndPoint));
                            }

                            if (status_type == NetConnectionStatus.Disconnected)
                            {
                                messageLog.CreateMessage(string.Format("Player {0} disconnected!", ((ClientInfo)received_message.SenderConnection.Tag).Name));
                                usedIds.Remove(((ClientInfo)received_message.SenderConnection.Tag).ID); // Remove player id from used id's

                                NetOutgoingMessage buff = serverLidgren.CreateMessage();
                                buff.Write((byte)DataPacketType.PlayerDisconnected);
                                buff.Write((byte)((ClientInfo)received_message.SenderConnection.Tag).ID);
                                messagesForSend.Add(buff);

                                
                            }
                            break;
                        }

                }


            }
        }

        public void SendData()
        {
            foreach (NetBuffer mess in messagesForSend)
            {
                if (mess is NetIncomingMessage)
                {
                    NetOutgoingMessage outMess = serverLidgren.CreateMessage();
                    outMess.Write(mess.Data);
                    serverLidgren.SendToAll(outMess, ((NetIncomingMessage)mess).SenderConnection, ((NetIncomingMessage)mess).DeliveryMethod, 0);
                }
                else if (mess is NetOutgoingMessage)
                {
                    serverLidgren.SendToAll((NetOutgoingMessage)mess, NetDeliveryMethod.ReliableOrdered);
                }                              
            }
            messagesForSend.Clear();
        }


        public void ReadSendLoop()
        {
            while (true)
            {
                lock (pseudoObject)
                {
                    ReadData();
                    SendData();
                }
                System.Threading.Thread.Sleep(1);
            }
        }
        private int GetNextID()
        {
            for (int i = 0; i < maxPeers; i++)
            {
                if (!usedIds.Contains((byte)i))
                {
                    usedIds.Add(i);
                    return i;
                }
            }
            return -1;
        }      

        public IEnumerable<string> GetPeerStrings()
        {
            if (serverLidgren != null)
            {
                foreach (NetConnection c in serverLidgren.Connections)
                {
                    yield return string.Format("{0} - {1} - {2} - Status: {3}", ((ClientInfo)c.Tag).ID, ((ClientInfo)c.Tag).Name, c.RemoteEndPoint, c.Status);
                }
            }
        }
    }
}
