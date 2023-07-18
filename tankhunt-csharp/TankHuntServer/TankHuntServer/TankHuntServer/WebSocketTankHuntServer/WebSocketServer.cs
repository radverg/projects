using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public class WebSocketServer
    {
        private TcpListener tcpListener = null;

        private Thread executingThread = null;
        private Thread acceptingThread = null;


        private GamePlay gamePlay;
        private List<Player> players = new List<Player>();

        public MessageLog MsgLog { get; private set; }

        public enum DataPacketType { Level = 1, HandshakeComplete, KeyChanged, SoftPlayerUpdate, IDUpdate }

        public WebSocketServer()
        {
            MsgLog = new MessageLog();
            Level.CurrentLevel = new Level("1 150 30 20 29 18 1 29 1 1 28 19 0 28 18 0 28 18 1 28 2 0 28 1 0 28 1 1 10 19 1 10 18 1 10 17 1 10 16 1 10 15 1 10 14 1 10 13 1 10 12 1 10 12 0 10 11 1 10 10 1 10 9 1 10 8 1 10 7 1 10 6 1 10 5 1 10 4 1 10 3 1 10 2 1 10 1 1 10 0 1 9 19 1 9 18 1 9 17 1 9 16 1 9 15 1 9 14 1 9 13 1 9 12 1 9 11 1 9 10 1 9 9 1 9 8 1 9 7 1 9 6 1 9 5 1 9 4 1 9 3 1 9 2 1 9 1 1 9 0 1 8 12 0 8 1 0 7 1 0 6 1 0 5 1 0 4 1 0 3 1 0 2 18 1 2 1 0 1 19 0 1 18 0 1 18 1 1 2 0 1 1 0 1 1 1");
        }

        public void StartListening(int port)
        {
            tcpListener = new TcpListener(new IPEndPoint(IPAddress.Any, port));
            tcpListener.Start();

            if (acceptingThread == null)
            {
                acceptingThread = new Thread(AcceptLoop);
                acceptingThread.IsBackground = true;
            }
            acceptingThread.Start();

            if (executingThread == null)
            {
                executingThread = new Thread(ExecutingLoop);
                executingThread.IsBackground = true;
            }
            executingThread.Start();

            gamePlay = new GamePlay(players); // Start updating and packet sending loop
            gamePlay.Start();

            MsgLog.CreateMessage("Started WebSocket server at port " + port.ToString() + "!");
        }

        private void ExecutingLoop()
        {
            while (true)
            {
                lock (this)
                {
                    foreach (Player p in players)
                    {
                        string resultMessage = p.AcceptAndExecuteData();
                        if (resultMessage != "")
                            MsgLog.CreateMessage(resultMessage);
                    } 
                }

                Thread.Sleep(1);
            }
        }



        private void AcceptLoop()
        {
            while (true)
            {
                lock (this)
                {
                    if (tcpListener.Pending())
                    {
                        TcpClient client = tcpListener.AcceptTcpClient();
                        players.Add(new Player(client));
                    } 
                }

                Thread.Sleep(2000);
            }
        }


        public static void WriteStream(NetworkStream stream, byte[] data)
        {

            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch
            {
                
            }
        }
    }
}
