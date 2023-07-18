using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public class GamePlay
    {
        private List<Player> players;
        private Thread mainGameLoopThread;

        private TimeSpan deltaTime;

        private Timer updatesTimer = new Timer(40);

        public GamePlay(List<Player> players)
        {
            this.players = players;
           
       
        }

        public void Start()
        {
            mainGameLoopThread = new Thread(GameLoopThreadMethod);
            mainGameLoopThread.IsBackground = true;
            mainGameLoopThread.Start();
            updatesTimer.Start();
        }


        private void GameLoopThreadMethod()
        {
            long totalMilisec = 0;
            Stopwatch sw = new Stopwatch();
            while (true)
            {               
                lock (this)
                {
                    sw.Stop();
                    deltaTime = sw.Elapsed;
                    totalMilisec += (long)deltaTime.TotalMilliseconds;
                    sw.Reset();
                    sw.Start();

                    // Update timers
                    updatesTimer.Update(deltaTime);
                    // -------

                    string playersSoftPacket = ((byte)WebSocketServer.DataPacketType.SoftPlayerUpdate).ToString() + " ";

                    foreach (Player p in players)
                    {
                        if (!p.Ready || !p.tcpPlayer.Connected)
                            continue;

                        if (p.KeysWatch["Up"])
                        {
                            p.Move(deltaTime);
                        }

                        if (p.KeysWatch["Right"])
                        {
                            if (p.Rotation_velocity < 0)
                                p.Rotation_velocity *= -1;

                            p.Rotate(deltaTime);
                            p.Velocity_coefficient = new Vector2(Math.Sin(p.Rotation), -Math.Cos(p.Rotation));
                        }

                        if (p.KeysWatch["Left"])
                        {
                            if (p.Rotation_velocity > 0)
                                p.Rotation_velocity *= -1;

                            p.Rotate(deltaTime);
                            p.Velocity_coefficient = new Vector2(Math.Sin(p.Rotation), -Math.Cos(p.Rotation));

                        }

                        playersSoftPacket += p.ToString();

                       

                       

                    }

                    if (updatesTimer.IsTicked)
                    {
                        foreach (Player p in players)
                        {
                            WebSocketServer.WriteStream(p.tcpPlayer.GetStream(), BufferTranslator.GetSendableDataFromRawData(playersSoftPacket.TrimEnd(' ')));
                        }
                    }

                }
                Thread.Sleep(1);
            }
        }
    }
}
