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


namespace Labyrinth
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class NetworkComponent : Microsoft.Xna.Framework.GameComponent
    {
        private ConnectionInfo con_info;
        private Labyrinth labyrinth;
        public enum UserType { None, ServerUser, ClientOnly, WaitingUser }
        public UserType user_type;
        public DataTranslator Data_translator { get; set; }

        private Server server = null;
        private Client client = null;

        public enum DataPacketType { LevelUpdate, PlayerAllInfoUpdate, PositionAndRotationUpdate, Shot, Invisibility, NewWeaponItem,
            OneShootCannonShot, WeaponShot, WeaponItemCollect, PlayerDied, PlayerDisconnected, NewLevelRequest }

        private Timer client_acceptor_timer = new Timer(1000);
        private Timer data_send_timer = new Timer(30);
        private Timer server_send_timer = new Timer(100);

        public NetworkComponent(Labyrinth game)
            : base(game)
        {
            labyrinth = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            Data_translator = new DataTranslator();
            client_acceptor_timer.Start();
            data_send_timer.Start();
            server_send_timer.Start();
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
            // ---------------------------------------------

            if (server == null && client == null) // No connection case - show connection dialog
            {
                if (!ConnectionLoop())
                    return;
                if (server == null)
                    labyrinth.SwitchScreen(labyrinth.WaitingScreen, true);
            }

            // Disconnect user if key esc was pressed
            if (SC.keystate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape) && SC.previous_keystate.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape))
                Disconnect();

            // Server user part--------------------------------------------------------------------------------------
            if (user_type == UserType.ServerUser) // Do this if server is hosted by this instantion
            {
                if (data_send_timer.IsTicked)
                {
                    if (labyrinth.container.Player_tank_c.Player.PositionChanged() || labyrinth.container.Player_tank_c.Player.RotationChanged())
                    {
                        SendPlayerUpdate(labyrinth.container.Player_tank_c.Player); // Send player
                    }
                }

                server.Receive_data(); // Receive waiting data from clients
                CheckAndApplyData(server.Received_data); 
                server.SendForwardDataToClients(); // Forward messages
            }
            // -------------------------------------------------------------------------------------------------------

            // Client user part --------------------------------------------------------------------------------------
            if (user_type == UserType.ClientOnly || user_type == UserType.WaitingUser)
            {
                    if (data_send_timer.IsTicked && (labyrinth.container.Player_tank_c.Player.PositionChanged() || labyrinth.container.Player_tank_c.Player.RotationChanged() ) && user_type != UserType.WaitingUser)
                    {
                        SendPlayerUpdate(labyrinth.container.Player_tank_c.Player); // send player here
                    }
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
                con_info = labyrinth.ShowConnectionScreen(null);

                if (con_info == null)
                    return false;
  
               
                if (con_info.Server)
                {
                    user_type = UserType.ServerUser;
                    server = new Server();
                    try
                    {
                        server.CreateServer(con_info.Port);
                        labyrinth.container.Player_tank_c.Player.Net_ID = 0;
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
                        labyrinth.container.Player_tank_c.Player.Net_ID = client.ID;
                        labyrinth.SwitchScreen(labyrinth.WaitingScreen, true); // Connection succesfull, wait until game is finished
                        locked = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                labyrinth.container.Player_tank_c.Player.Player_name = con_info.Player_name;
            }
            return true;
        }

        public void CheckAndApplyData(List<NetBuffer> data_messages)
        {
            if (data_messages != null)
            {
                foreach (NetBuffer mess in data_messages)
                {
                    switch ((DataPacketType)mess.ReadByte())
                    {
                       case DataPacketType.LevelUpdate: // This occurs only on client side
                           {
                               RandomLevel level = Data_translator.ReadLevel(mess);
                               labyrinth.container.Srl_c.SetExistingLevel(level.Square_size, level.Size, level.Walls);
                               if (user_type == UserType.WaitingUser)
                               {
                                   user_type = UserType.ClientOnly;
                                   labyrinth.SwitchScreen(labyrinth.PlayingMScreen, true);
                               }
                               SendPlayerAllData(labyrinth.container.Player_tank_c.Player); // Send all player data to server
                               break;
                           }
                       case DataPacketType.PlayerAllInfoUpdate:
                            {
                                labyrinth.container.Player_tank_c.UpdatePlayer(Data_translator.ReadAllPlayerInfo(mess));

                                break;
                            }
                        case DataPacketType.PlayerDied:
                            {
                                labyrinth.container.Player_tank_c.PlayerDied(mess.ReadByte(), mess.ReadInt32());
                                break;
                            }
                        case DataPacketType.PositionAndRotationUpdate:
                            {
                                DataTranslator.ShortPlayerData data = Data_translator.ReadPlayerUpdate(mess);
                                labyrinth.container.Player_tank_c.UpdatePlayer(data.id, data.position, data.rotation);
                                break;
                            }
                        case DataPacketType.PlayerDisconnected:
                            {
                                labyrinth.container.Player_tank_c.RemovePlayer(mess.ReadByte());
                                break;
                            }
                        case DataPacketType.NewWeaponItem:
                            {
                                labyrinth.container.Items_c.AddItem(Data_translator.ReadNewWeaponItem(mess));
                                break;
                            }
                        case DataPacketType.Invisibility:
                            {
                                labyrinth.container.Player_tank_c.StartInvisibility(mess.ReadByte(), mess.ReadInt32(), true);
                                break;
                            }
                        case DataPacketType.WeaponItemCollect:
                            {
                                labyrinth.container.Items_c.CollectItem(mess.ReadInt32(), mess.ReadByte());
                                break;
                            }
                        case DataPacketType.OneShootCannonShot:
                            {
                                labyrinth.container.Player_tank_c.NetOneShoot(mess.ReadByte(), mess.ReadInt32(), new Vector2(mess.ReadFloat(), mess.ReadFloat()) * SC.resv_ratio, mess.ReadDouble()); 
                                break;
                            }
                        case DataPacketType.WeaponShot:
                            {
                                labyrinth.container.Player_tank_c.NetWeaponShoot(mess.ReadByte(), mess.ReadInt32(), new Vector2(mess.ReadFloat(), mess.ReadFloat()) * SC.resv_ratio, mess.ReadDouble());
                                break;
                              
                            }
                        case DataPacketType.NewLevelRequest:
                            {
                                if (user_type == UserType.ServerUser)
                                    labyrinth.container.Srl_c.CreateNewRandomLevel();
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
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendNewItem(WeaponItem item)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.NewWeaponItem);
            Data_translator.WriteNewWeaponItem(item, buff);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendPlayerUpdate(TankPlayerSprite player)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.PositionAndRotationUpdate);
            Data_translator.WritePlayerUpdate(buff, player);
            CheckAndSend(buff, NetDeliveryMethod.Unreliable);
        }

        public void SendPlayerInvisibility(TankPlayerSprite player)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.Invisibility);
            buff.Write((byte)player.Net_ID);
            buff.Write((Int32)player.Invisibility_timer.Interval);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendItemCollect(WeaponItem item, TankPlayerSprite player)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.WeaponItemCollect);
            buff.Write(item.Net_ID);
            buff.Write((byte)player.Net_ID);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendOneShootCannonShot(Shot shot, byte player_id)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.OneShootCannonShot);
            buff.Write(player_id);
            buff.Write((Int32)shot.Net_ID);
            buff.Write(Data_translator.Normalize(shot.Position.X));
            buff.Write(Data_translator.Normalize(shot.Position.Y));
            buff.Write(shot.Movement_angle);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendWeaponShot(Shot shot, byte player_id)
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.WeaponShot);
            Data_translator.WriteShotByWeapon(buff, shot, player_id);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);

        }

        public void SendNewLevelRequest()
        {
            NetBuffer buff = new NetBuffer();
            buff.Write((byte)DataPacketType.NewLevelRequest);
            CheckAndSend(buff, NetDeliveryMethod.ReliableOrdered);
        }

        private void CheckAndSend(NetBuffer buff, NetDeliveryMethod method)
        {
            if (server == null)
                client.SendDataNow(buff, method);
            else
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
            labyrinth.container.Player_tank_c.Players.RemoveAll((p) => p != labyrinth.container.Player_tank_c.Player);
     
        }
    }
}
