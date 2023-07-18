using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TankHuntServer.WebSocketTankHuntServer;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public class Player : Sprite
    {
        public TcpClient tcpPlayer { get; private set; }

        public Dictionary<string, bool> KeysWatch = new Dictionary<string, bool>();

        public bool Ready { get; private set; }
      
        public Player(TcpClient playerConnection)
            :base(new Vector2(0, 0), new Vector2(40, 40))
        {
            tcpPlayer = playerConnection;
            Ready = false;

            Net_ID = IDManager.GetNextID();

            Velocity_coefficient = new Vector2(0,0);
            Velocity_const = new Vector2(0.16, 0.16);
            Rotation = 0;
            Rotation_velocity = 0.004f;

            KeysWatch.Add("Left", false);
            KeysWatch.Add("Right", false);
            KeysWatch.Add("Up", false);
            KeysWatch.Add("Down", false);
            KeysWatch.Add("S", false);
            KeysWatch.Add("D", false);

        }

        public string AcceptAndExecuteData()
        {
            if (!tcpPlayer.Connected)
                return "";

            NetworkStream stream = tcpPlayer.GetStream();
            if (stream.DataAvailable)
            {
                byte[] dataBuffer = new byte[tcpPlayer.Available];
                stream.Read(dataBuffer, 0, tcpPlayer.Available);
                string dataString = Encoding.UTF8.GetString(dataBuffer);
                string decodedDataString = BufferTranslator.DecodeIncomingRawData(dataBuffer);

                if (BufferTranslator.handshakeCheckRegex.IsMatch(dataString)) // It's websocket handshake request
                {
                    byte[] response = BufferTranslator.GetHandshakeResponse(dataString);
                    stream.Write(response, 0, response.Length); // Send websocket handshake response
                    return "Sending handshake response to " + tcpPlayer.Client.RemoteEndPoint.ToString();
                }

                string[] splittedData = decodedDataString.Split(' ');

                if (splittedData.Length > 0)
                {
                    int ident = 999;
                    int.TryParse(splittedData[0], out ident);
                    WebSocketServer.DataPacketType packetIdentifier = (WebSocketServer.DataPacketType)ident;

                    switch (packetIdentifier)
                    {
                        case WebSocketServer.DataPacketType.HandshakeComplete: // When player is connected, send him level
                            {
                                byte[] levelBuffer = BufferTranslator.GetSendableDataFromRawData(Level.CurrentLevel.Pattern);
                                stream.Write(levelBuffer, 0, levelBuffer.Length);
                                // Send him id
                                WebSocketServer.WriteStream(stream, BufferTranslator.GetSendableDataFromRawData(((byte)WebSocketServer.DataPacketType.IDUpdate).ToString() + " " + Net_ID.ToString()));
                                Ready = true;
                                break;
                            }
                        case WebSocketServer.DataPacketType.KeyChanged:
                            {
                                string key = splittedData[1]; // Key code
                                bool pressed = (splittedData[2] == "1");
                                KeysWatch[key] = pressed;
                                break;
                            }
                    }


                }


                return "";


            }
            return "";
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} ", Net_ID, Position.X, Position.Y, Rotation);

        }
    }
}
