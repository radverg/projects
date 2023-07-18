using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Lidgren.Network;


namespace TankHunt
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class NetworkComponent : Microsoft.Xna.Framework.GameComponent
    {
        private ConnectionInfo con_info;
        private TankHunt tankhunt;
        public enum UserType { None, ServerUser, ClientOnly, WaitingUser, LeadingClient }
        public UserType user_type;
        public DataTranslator Data_translator { get; private set; }

        private Server server = null;
        private Client client = null;

        public int Current_ping { get; private set; }

        public enum DataPacketType { LevelUpdate, PlayerAllInfoUpdate, PositionAndRotationUpdate, Shot, Invisibility, NewWeaponItem,
            OneShootCannonShot, WeaponShot, WeaponItemCollect, PlayerDied, PlayerDisconnected, NewLevelRequest, ServerTimeRequest, ServerTime, ChatMessage, LeadingClientPromotion, ShotsRefresh }

        private Timer client_acceptor_timer = new Timer(1000);
        private Timer data_send_timer = new Timer(30);
        private Timer server_send_timer = new Timer(100);
        private Timer delay_timer = new Timer(10000);
        private Timer server_time_send_timer = new Timer(1000);

        public TimeSpan Server_time { get; private set; }

        public string LeaderShipPass { get; set; }

        public NetworkComponent(TankHunt game)
            : base(game)
        {
            tankhunt = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            LeaderShipPass = "";
            Data_translator = new DataTranslator();
            client_acceptor_timer.Start();
            data_send_timer.Start();
            server_send_timer.Start();
            server_time_send_timer.Start();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update timers -------------------------------
            client_acceptor_timer.Update(gameTime);
            data_send_timer.Update(gameTime);
            server_send_timer.Update(gameTime);
            delay_timer.Update(gameTime);
            server_time_send_timer.Update(gameTime);
            // ---------------------------------------------

            if (server == null && client == null) // No connection case - show connection dialog
            {
                if (!ConnectionLoop())
                    return;
                if (user_type == UserType.ServerUser || user_type == UserType.LeadingClient)
                    tankhunt.container.Srl_c.CreateNewRandomLevel(from p in tankhunt.container.Player_tank_c.Players select p.Color);
                
            }

            
            // Server user part--------------------------------------------------------------------------------------
            if (user_type == UserType.ServerUser) // Do this if server is hosted by this instantion
            {
                Server_time = gameTime.TotalGameTime;
                if (data_send_timer.IsTicked)
                {
                    if (tankhunt.container.Player_tank_c.Player.PositionChanged() || tankhunt.container.Player_tank_c.Player.RotationChanged())
                    {
                        SendPlayerUpdate(tankhunt.container.Player_tank_c.Player); // Send player
                    }
                }

                server.Receive_data(); // Receive waiting data from clients
                CheckAndApplyData(server.Received_data); 
                server.ForwardDataToClients(); // Forward messages
            }
            // -------------------------------------------------------------------------------------------------------

            // Client user part --------------------------------------------------------------------------------------
            if (user_type == UserType.ClientOnly || user_type == UserType.WaitingUser || user_type == UserType.LeadingClient)
            {
                Server_time += gameTime.ElapsedGameTime;

                if (data_send_timer.IsTicked && (tankhunt.container.Player_tank_c.Player.PositionChanged() || tankhunt.container.Player_tank_c.Player.RotationChanged() ) && user_type != UserType.WaitingUser)
                {
                    SendPlayerUpdate(tankhunt.container.Player_tank_c.Player); // send player here
                }

                if (server_time_send_timer.IsTicked) // Send request for server time
                    SendServerTimeRequest();

                // Reconect in game -------------------------------------------------------------
                if (!client.Connected && user_type != UserType.WaitingUser && SC.keystate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C) &&
                    SC.previous_keystate.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.C))
                {
                    try
                    {
                        user_type = UserType.WaitingUser;
                        client.AttemptConnection(con_info.IP_adress, con_info.Port);
                        if (client.LeadingClient)
                            user_type = UserType.LeadingClient;
                        else
                            tankhunt.SwitchScreen(tankhunt.WaitingScreen, true);
                        tankhunt.container.Player_tank_c.Players.RemoveAll((p) => p != tankhunt.container.Player_tank_c.Player);
                    }
                    catch
                    {
                        MessageLog.CreateMessage("No response from server! \nConnection attempt failed!");
                    } 
                }
                // ------------------------------------------------------------------------------

                client.SendData();
                client.ReceiveData();
                CheckAndApplyData(client.Received_data);
            }
            // --------------------------------------------------------------------------------------------------------
            base.Update(gameTime);
        }

       

        public bool ConnectionLoop()
        {
            bool locked = true;
            server = null;
            client = null;
 
            while (locked)
            {
                con_info = tankhunt.ShowConnectionScreen(null);

                if (con_info == null)
                    return false;

                tankhunt.container.Srl_c.Darkness_coeff = con_info.Darkness_percentage;
                if (con_info.Server)
                {
                    user_type = UserType.ServerUser;
                    server = new Server();
                    try
                    {
                        server.CreateServer(con_info.Port, con_info.Max_players);
                        tankhunt.container.Player_tank_c.Player.Net_ID = 0;
                        locked = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured while creating a new server!\n" + ex.Message, "Error", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }

                if (!con_info.Server)
                {
                    user_type = UserType.WaitingUser;
                    try
                    {
                        client = new Client(con_info.Player_name);
                        client.AttemptConnection(con_info.IP_adress, con_info.Port);
                        if (client.LeadingClient)
                            user_type = UserType.LeadingClient;
                        else
                            tankhunt.SwitchScreen(tankhunt.WaitingScreen, true); // Connection succesfull, wait until game is finished
                        tankhunt.container.Player_tank_c.Player.Net_ID = client.ID;
                       
                        locked = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                tankhunt.container.Player_tank_c.Player.Player_name = con_info.Player_name;
                tankhunt.container.Srl_c.Max_min_square_size = con_info.Max_min_square_size;
                tankhunt.container.Srl_c.Max_min_size = con_info.Max_min_size;
                tankhunt.IsFixedTimeStep = con_info.StableFPS;
                tankhunt.graphics.SynchronizeWithVerticalRetrace = con_info.Vsync;
                tankhunt.container.Player_tank_c.Player.Even_up = con_info.Even_up;
                tankhunt.container.Items_c.SpawnItemProbabilities = con_info.SpawnProbabilities;
                
            }
            return true;
        }

        public void CheckAndApplyData(List<NetBuffer> data_messages)
        {
            if (data_messages != null)
            {
                foreach (NetBuffer mess in data_messages)
                {
                    byte data_type = mess.ReadByte();

                    if (user_type == UserType.WaitingUser && (DataPacketType)data_type != DataPacketType.LevelUpdate && (DataPacketType)data_type != DataPacketType.PlayerAllInfoUpdate)
                        return;

                    switch ((DataPacketType)data_type)
                    {
                       case DataPacketType.LevelUpdate: // This occurs only on client side
                           {
                               if (tankhunt.container.Player_tank_c.Player.IsAlive && tankhunt.container.Player_tank_c.Players.FindAll((p) => p.IsAlive).Count == 1)
                                   tankhunt.container.Player_tank_c.Player.Wins++;
                               RandomLevel level = Data_translator.ReadLevel(mess);
                               tankhunt.container.Srl_c.SetExistingLevel(level, mess.ReadDouble());
                               if (user_type == UserType.WaitingUser)
                               {
                                   user_type = UserType.ClientOnly;
                                   tankhunt.SwitchScreen(tankhunt.PlayingMScreen, true);
                                   tankhunt.container.Darkness_c.Active = level.Darkness;
                               }
                               SendPlayerAllData(tankhunt.container.Player_tank_c.Player); // Send all player data to server
                               break;
                           }
                       case DataPacketType.PlayerAllInfoUpdate:
                            {
                                tankhunt.container.Player_tank_c.UpdatePlayer(Data_translator.ReadAllPlayerInfo(mess));

                                break;
                            }
                        case DataPacketType.PlayerDied:
                            {
                                tankhunt.container.Player_tank_c.PlayerDied(mess.ReadByte(), mess.ReadInt32());
                                break;
                            }
                        case DataPacketType.PositionAndRotationUpdate:
                            {
                                DataTranslator.ShortPlayerData data = Data_translator.ReadPlayerUpdate(mess, tankhunt.container.Srl_c.Level.Absolute_size);
                                tankhunt.container.Player_tank_c.UpdatePlayer(data.id, data.position, data.rotation);
                                break;
                            }
                        case DataPacketType.PlayerDisconnected:
                            {
                                tankhunt.container.Player_tank_c.RemovePlayer(mess.ReadByte());
                                break;
                            }
                        case DataPacketType.NewWeaponItem:
                            {
                                tankhunt.container.Items_c.AddItem(Data_translator.ReadNewWeaponItem(mess));
                                break;
                            }
                        case DataPacketType.Invisibility:
                            {
                                tankhunt.container.Player_tank_c.StartInvisibility(mess.ReadByte(), mess.ReadInt32(), true);
                                break;
                            }
                        case DataPacketType.WeaponItemCollect:
                            {
                                tankhunt.container.Items_c.CollectItem(mess.ReadInt32(), mess.ReadByte());
                                break;
                            }
                        case DataPacketType.OneShootCannonShot:
                            {
                                tankhunt.container.Player_tank_c.NetOneShoot(mess.ReadByte(), mess.ReadInt32(), new Vector2((float)(mess.ReadDouble() * DataTranslator.Level_absolute_size.X), (float)(mess.ReadDouble() * DataTranslator.Level_absolute_size.Y)), mess.ReadDouble())
                                ;
                               
                                break;
                            }
                        case DataPacketType.WeaponShot:
                            {
                                tankhunt.container.Player_tank_c.NetWeaponShoot(mess.ReadByte(), mess.ReadInt32(), new Vector2((float)(mess.ReadDouble() * DataTranslator.Level_absolute_size.X), (float)(mess.ReadDouble() * DataTranslator.Level_absolute_size.Y)), mess.ReadDouble());
                                break;
                            }
                        case DataPacketType.NewLevelRequest:
                            {
                                if (user_type == UserType.ServerUser)
                                    tankhunt.container.Srl_c.CreateNewRandomLevel(from p in tankhunt.container.Player_tank_c.Players select p.Color);
                                break;
                            }
                        case DataPacketType.ServerTime:
                            {
                                double server_time = mess.ReadDouble();
                                Current_ping = (int)delay_timer.Temporary_time;
                                Server_time = TimeSpan.FromMilliseconds(server_time + delay_timer.Temporary_time);
                                delay_timer.Stop();
                                break;
                            }
                        case DataPacketType.ServerTimeRequest:
                            {
                                if (user_type == UserType.ServerUser)
                                    SendServerTime();
                                break;
                            }
                        case DataPacketType.ChatMessage:
                            {
                                TankPlayerSprite sender = tankhunt.container.Player_tank_c.ReturnPlayer(mess.ReadByte());
                                string text = mess.ReadString();
                                if (text[0] == '-')
                                    if (user_type == UserType.ServerUser || user_type == UserType.LeadingClient)
                                        new Query(text, tankhunt, UserType.ClientOnly, sender);
                                    else
                                        new Query(text, tankhunt, UserType.ServerUser, sender);

                                else
                                    MessageLog.CreateMessage(text);
                                break;
                            }
                        case DataPacketType.LeadingClientPromotion:
                            {
                                if (mess.ReadInt32() == tankhunt.container.Player_tank_c.Player.Net_ID)
                                {
                                    user_type = UserType.LeadingClient;
                                    MessageLog.CreateMessage("You have just become leading client!");
                                }
                                break; 
                            }
                        case DataPacketType.ShotsRefresh:
                            {
                                byte count = mess.ReadByte();
                                for (int i = 0; i < count; i++)
                                {
                                    Shot sh = tankhunt.container.Mshots_c.ReturnShot(mess.ReadInt32());
                                    if (sh != null)
                                    {
                                        Vector2 pos = new Vector2(mess.ReadFloat(), mess.ReadFloat());
                                        Vector2 coeff = new Vector2(mess.ReadFloat(), mess.ReadFloat());
                                        double rot = SC.ArcusSinusCosinus(coeff * new Vector2(1, -1));
                                        if ((SC.GetDistance(pos * DataTranslator.Level_absolute_size, sh.Position) > 75 * SC.res_ratio && MathHelper.Distance((float)rot, (float)sh.Movement_angle) > 0.02f)) // Distance between objects is greater than X pixels
                                        {
                                            if (sh is Rocket)
                                                    ((Rocket)sh).centerPoint = pos * DataTranslator.Level_absolute_size + (sh.Size / 2);
                                                    sh.Rotation = rot;

                                            sh.Velocity_coefficient = coeff;
                                            sh.Movement_angle = rot;
                                            sh.Position = pos * DataTranslator.Level_absolute_size;
                                        }
                                    }
                                        
                                }
                                break;
                            }

                   }
                }
            }
        }

        
        public void SendLevel(RandomLevel Level)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.LevelUpdate);
            Data_translator.WriteLevel(buff, Level);
            buff.Write(Server_time.TotalMilliseconds);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendPlayerAllData(TankPlayerSprite player)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.PlayerAllInfoUpdate);
            Data_translator.WriteAllPlayerInfo(buff, player);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendDeath(byte id, int shot_id)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.PlayerDied);
            buff.Write(id);
            buff.Write(shot_id);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
        }

        public void SendNewItem(WeaponItem item)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.NewWeaponItem);
            Data_translator.WriteNewWeaponItem(item, buff);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
        }

        public void SendPlayerUpdate(TankPlayerSprite player)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.PositionAndRotationUpdate);
            Data_translator.WritePlayerUpdate(buff, player, tankhunt.container.Srl_c.Level.Absolute_size);
            CheckAndSend(buff, NetDeliveryMethod.Unreliable);
        }

        public void SendPlayerInvisibility(TankPlayerSprite player)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.Invisibility);
            buff.Write((byte)player.Net_ID);
            buff.Write((Int32)player.Invisibility_timer.Interval);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
        }

        public void SendItemCollect(WeaponItem item, TankPlayerSprite player)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.WeaponItemCollect);
            buff.Write(item.Net_ID);
            buff.Write((byte)player.Net_ID);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
        }

        public void SendOneShootCannonShot(Shot shot, byte player_id)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.OneShootCannonShot);
            buff.Write(player_id);
            buff.Write((Int32)shot.Net_ID);
            buff.Write((double)shot.Position.X / (double)DataTranslator.Level_absolute_size.X);
            buff.Write((double)shot.Position.Y / (double)DataTranslator.Level_absolute_size.Y);
            buff.Write(shot.Movement_angle);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
        }

        public void SendWeaponShot(Shot shot, byte player_id)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.WeaponShot);
            Data_translator.WriteShotByWeapon(buff, shot, player_id);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);

        }

        public void SendNewLevelRequest()
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.NewLevelRequest);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
        }

        public void SendServerTimeRequest()
        {
            delay_timer.Stop();
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.ServerTimeRequest);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
            delay_timer.Start();
        }

        public void SendServerTime()
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.ServerTime);
            buff.Write(Server_time.TotalMilliseconds);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
        }

        public void SendChatMessage(string message, TankPlayerSprite sender)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.ChatMessage);
            buff.Write((byte)sender.Net_ID);
            buff.Write(message);
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);
        }

        public void SendLeadingClientPromotion(TankPlayerSprite target)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.LeadingClientPromotion);
            buff.Write(target.Net_ID);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendShotsRefresh(IEnumerable<Shot> shots)
        {
            if (shots.Count() == 0)
                return;
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.ShotsRefresh);
            buff.Write((byte)shots.Count());
            foreach (Shot s in shots)
            {
                buff.Write(s.Net_ID);
                buff.Write(s.Position.X / DataTranslator.Level_absolute_size.X);
                buff.Write(s.Position.Y / DataTranslator.Level_absolute_size.Y);
                buff.Write(s.Velocity_coefficient.X);
                buff.Write(s.Velocity_coefficient.Y);
            }
            CheckAndSend(buff, NetDeliveryMethod.ReliableUnordered);

        }

        private void CheckAndSend(NetBuffer buff, NetDeliveryMethod method)
        {
            if (server == null && client != null)
                client.SendDataNow(buff, method);
            else if (server != null && client == null) 
                server.SendDataNow(buff, method);

        }

        public void Disconnect()
        {
            if (server == null && client != null)
            {
                client.Disconnect();
                client = null;
            }
            else if (client == null && server != null)
            {
                server.Disconnect();
                server = null;
            }
            user_type = UserType.None;
            tankhunt.container.Player_tank_c.Players.RemoveAll((p) => p != tankhunt.container.Player_tank_c.Player);
     
        }

       
    }
}
