using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TankHunt
{
    class Query
    {
        public Query(string query, TankHunt th, NetworkComponent.UserType sender_type, TankPlayerSprite sender)
        {
            string outputMessage = "Command has been successfully performed!";
            try
            {
                DoQuery(query, th, sender_type, sender);
            }
            catch (IndexOutOfRangeException)
            {
                outputMessage = "Invalid command!";
            }
            catch (NullReferenceException)
            {
                outputMessage = "Invalid command!";
            }
            catch (FormatException)
            {
                outputMessage = "Invalid command!";
            }
            catch (Exception ex)
            {
                outputMessage = ex.Message;
            }

            if (sender == th.container.Player_tank_c.Player)
                MessageLog.CreateMessage(outputMessage);
        }

        private void DoQuery(string query, TankHunt th, NetworkComponent.UserType sender_type, TankPlayerSprite sender)
        {
            bool isServerSender = (sender_type == NetworkComponent.UserType.LeadingClient || sender_type == NetworkComponent.UserType.ServerUser);
            bool isServerCurrent = (th.container.Network_c.user_type == NetworkComponent.UserType.ServerUser || th.container.Network_c.user_type == NetworkComponent.UserType.LeadingClient);
            string loc_query = query;
            // Remove minus char
            loc_query = loc_query.Substring(1);

            // Parse query
            string[] query_parts = loc_query.Split(' ');

            if (query_parts.Length == 4)
            {
                // Reset stats query
                if (query_parts[0] == "reset" && query_parts[1] == "stats" && query_parts[2] == "to" && query_parts[3] != "" && isServerSender)
                {
                    List<TankPlayerSprite> targets = GetPlayers(query_parts[3], th);
                    foreach (TankPlayerSprite player in targets)
                    {
                        player.ResetStats();
                    }
                }

                // Set darkness query
                if (query_parts[0] == "set" && query_parts[1] == "darkness" && query_parts[2] == "to" && query_parts[3] != "" && isServerSender
                    && isServerCurrent)
                {                  
                     int value = int.Parse(query_parts[3]);
                     if (value < 0 || value > 100)
                         throw new Exception("Percentage number cannot be larger than 100 or lower than 0!");
                     th.container.Srl_c.Darkness_coeff = (byte)value;
                }

                // Set minmax square size query
                if (query_parts[0] == "set" && query_parts[1] == "minmaxsquare" && query_parts[2] == "to" && query_parts[3].Contains('-') && isServerSender
                    && isServerCurrent)
                {
                    int[] values = new int[] { int.Parse(query_parts[3].Split('-')[1]), int.Parse(query_parts[3].Split('-')[0]) };
                    th.container.Srl_c.Max_min_square_size = new Vector2(values.Max(), values.Min());
                }

                // Set minmax level size query
                if (query_parts[0] == "set" && query_parts[1] == "minmaxsize" && query_parts[2] == "to" && query_parts[3].Contains('-') && isServerSender
                    && isServerCurrent)
                {
                    int[] values = new int[] { int.Parse(query_parts[3].Split('-')[1]), int.Parse(query_parts[3].Split('-')[0]) };
                    th.container.Srl_c.Max_min_size = new Vector2(values.Max(), values.Min());
                }
            }

            if (query_parts.Length == 6)
            {
                // Set color query
                if (query_parts[0] == "set" && query_parts[1] == "color" && query_parts[2] == "to" && query_parts[3] != "" && query_parts[4] == "to" && query_parts[5] != "" && isServerSender)
                {
                    Color desired = Color.Red;
                    if (query_parts[5].Contains('-')) // Color is determined by rgb
                    {
                        string[] splitted_rgb = query_parts[5].Split('-');
                        desired = new Color(int.Parse(splitted_rgb[0]), int.Parse(splitted_rgb[1]), int.Parse(splitted_rgb[2]));
                    }
                    else // Color is determined by word
                    {
                        System.Drawing.Color s_color = System.Drawing.Color.FromName(query_parts[5]);
                        desired = new Color(s_color.R, s_color.G, s_color.B);
                    }

                    if (desired.R + desired.B + desired.G < 40 || desired.R + desired.B + desired.G > 725)
                        throw new Exception("Required color is too dark or too light!");

                    List<TankPlayerSprite> targets = GetPlayers(query_parts[3], th);
                    IEnumerable<Color> colorsToRecolor = from p in targets select p.Color;
                    List<Wall> wallsToRecolor = (from w in th.container.Srl_c.Level.Walls where colorsToRecolor.Contains(w.Color) select w).ToList(); 

                    foreach (TankPlayerSprite player in targets)
                    {
                        player.Color = desired;
                    }

                    foreach (Wall w in wallsToRecolor)
                    {
                        w.Color = desired;
                    }
                }
            }



            if (query_parts.Length == 2) 
            {
                // Kick player query
                if (query_parts[0] == "kick" && query_parts[1] != "" && isServerSender)
                {
                    List<TankPlayerSprite> targets = GetPlayers(query_parts[1], th);                   
                    foreach (TankPlayerSprite player in targets)
                    {
                        if (player == th.container.Player_tank_c.Player)
                            th.container.Network_c.Disconnect();
                    }
                   
                }

                // Kill player query
                if (query_parts[0] == "kill" && query_parts[1] != "" && isServerSender)
                {
                    List<TankPlayerSprite> targets = GetPlayers(query_parts[1], th);
                    foreach (TankPlayerSprite player in targets)
                    {
                        if (player == th.container.Player_tank_c.Player && player.IsAlive)
                            th.container.Player_tank_c.Die(-1);
                       

                    }
                }

                // Set autorotating
                if (query_parts[0] == "autorotating" && query_parts[1] != "" && sender == th.container.Player_tank_c.Player)
                    th.container.Player_tank_c.Player.Even_up = bool.Parse(query_parts[1]);

                
            }

            if (query_parts.Length > 1)
            {
                // Take leadership
                if (query_parts[0] == "take" && query_parts[1] == "leadership" && th.container.Network_c.user_type != NetworkComponent.UserType.ServerUser)
                {
                    if (sender == th.container.Player_tank_c.Player)
                    {
                        th.container.Network_c.user_type = NetworkComponent.UserType.LeadingClient;

                    }
                    else
                        th.container.Network_c.user_type = NetworkComponent.UserType.ClientOnly;

                   /* string pass = "";
                    if (query_parts.Length == 3)
                        pass = query_parts[2];

                    if (th.container.Network_c.LeaderShipPass == "")
                        th.container.Network_c.LeaderShipPass = pass;
                    else if (th.container.Network_c.LeaderShipPass != pass)
                        throw new Exception("Invalid password!");

                    if (sender == th.container.Player_tank_c.Player)
                    {
                        th.container.Network_c.user_type = NetworkComponent.UserType.LeadingClient;

                    }
                    else
                        th.container.Network_c.user_type = NetworkComponent.UserType.ClientOnly;*/
                }
            }


            // Run avi kungfu query
            if (query_parts.Length == 1)
                if (query_parts[0] == "avikungfu")
                    TankHunt.avi_kungfu.Play();
        }
         
        private List<TankPlayerSprite> GetPlayers(string query_part, TankHunt th)
        {
            List<TankPlayerSprite> result = new List<TankPlayerSprite>();

            if (query_part == "all")
                return th.container.Player_tank_c.Players;

            string[] string_ids = query_part.Split('-');

            try
            {
                for (int i = 0; i < string_ids.Length; i++)
                {
                    int id = int.Parse(string_ids[i]);
                    TankPlayerSprite found_player = th.container.Player_tank_c.ReturnPlayer(id);
                    if (found_player != null)
                        result.Add(found_player);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

    }
    
}
