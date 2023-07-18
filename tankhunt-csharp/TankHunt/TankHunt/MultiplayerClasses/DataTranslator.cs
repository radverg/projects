using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;

namespace TankHunt
{
    public class DataTranslator
    {
        public static Vector2 Level_absolute_size { get; set; }

        public float Normalize(float number)
        {
            return number / SC.res_ratio;
        }

        public Vector2 Normalize(Vector2 vector)
        {
            return vector / SC.resv_ratio;
        }

        public RandomLevel ReadLevel(NetBuffer mess)
        {
            RandomLevel level = new RandomLevel();
            level.Square_size = (int)(mess.ReadFloat() * SC.res_ratio);
            level.Size = new Vector2(mess.ReadInt32(), mess.ReadInt32());
            level.Tiles = new Tile[(int)level.Size.X, (int)level.Size.Y];
            

            int walls_count = mess.ReadInt32();
            for (int i = 0; i < walls_count; i++)
            {
                Wall new_wall = new Wall((Wall.WallSide)mess.ReadByte(), new Vector2(mess.ReadInt32(), mess.ReadInt32()), level.Square_size, new Color(mess.ReadByte(), mess.ReadByte(), mess.ReadByte()));
                if (level.Tiles[(int)new_wall.Square_position.X, (int)new_wall.Square_position.Y] == null)
                    level.Tiles[(int)new_wall.Square_position.X, (int)new_wall.Square_position.Y] = new Tile(new Vector2(new_wall.Square_position.X, new_wall.Square_position.Y), level.Square_size);

                level.Walls.Add(new_wall);
                level.Tiles[(int)new_wall.Square_position.X, (int)new_wall.Square_position.Y].AddWall(new_wall);

            }
            level.Darkness = mess.ReadBoolean();
            level.Default_position = new Vector2(mess.ReadFloat(), mess.ReadFloat()) * SC.resv_ratio;
            return level;
        }

        public void WriteLevel(NetBuffer mess, RandomLevel level)
        {
            mess.Write(Normalize(level.Square_size));
            mess.Write((int)level.Size.X);
            mess.Write((int)level.Size.Y);
            mess.Write((Int32)level.Walls.Count);
            
            for (int x = 0; x < level.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < level.Tiles.GetLength(1); y++)
                {
                    IEnumerable<Wall> walls = level.Tiles[x, y].GetWalls();
                    foreach (Wall w in walls)
                    {
                        mess.Write((byte)w.Wall_side);
                        mess.Write(x);
                        mess.Write(y);
                        mess.Write((byte)w.Color.R);
                        mess.Write((byte)w.Color.G);
                        mess.Write((byte)w.Color.B);
                    }
                }
            }

            mess.Write(level.Darkness);
            mess.Write(Normalize(level.Default_position.X));
            mess.Write(Normalize(level.Default_position.Y));
        }

       
        public void WriteAllPlayerInfo(NetBuffer mess, TankPlayerSprite player)
        {
            mess.Write((byte)player.Net_ID);
            mess.Write(player.Player_name);
            mess.Write(player.Color.R);
            mess.Write(player.Color.G);
            mess.Write(player.Color.B);
            mess.Write((double)player.Position.X / (double)Level_absolute_size.X);
            mess.Write((double)player.Position.Y / (double)Level_absolute_size.Y);
            mess.Write((float)player.Rotation);
            mess.Write((Int32)player.Wins);
            mess.Write((Int32)player.Kills);
            mess.Write((Int32)player.Deaths);
            mess.Write((Int32)player.Suicides);
            mess.Write(player.Observer);
        }

        public TankPlayerSprite ReadAllPlayerInfo(NetBuffer mess)
        {
            TankPlayerSprite info = new TankPlayerSprite(null, Vector2.Zero, Vector2.Zero, Color.White, -1);
            info.Net_ID = mess.ReadByte();
            info.Player_name = mess.ReadString();
            info.Color = new Color(mess.ReadByte(), mess.ReadByte(), mess.ReadByte());
            info.Position = new Vector2((float)(mess.ReadDouble() * Level_absolute_size.X),
                (float)(mess.ReadDouble() * Level_absolute_size.Y));
            info.Rotation = mess.ReadFloat();
            info.Wins = mess.ReadInt32();
            info.Kills = mess.ReadInt32();
            info.Deaths = mess.ReadInt32();
            info.Suicides = mess.ReadInt32();
            info.Observer = mess.ReadBoolean();
            return info;
        }

        public void WritePlayerUpdate(NetBuffer mess, TankPlayerSprite player, Vector2 absolute_level_size)
        {
            mess.Write((byte)player.Net_ID);
            mess.Write((double)player.Position.X / (double)absolute_level_size.X);
            mess.Write((double)player.Position.Y / (double)absolute_level_size.Y);
            mess.Write((float)player.Rotation);
            if (player.Weapon == null)
                mess.Write((byte)WeaponItem.WeaponType.None);
            else
                mess.Write((byte)player.Weapon.Weapon_type);
        }

        public ShortPlayerData ReadPlayerUpdate(NetBuffer mess, Vector2 absolute_level_size)
        {
            return new ShortPlayerData() { id = mess.ReadByte(), position = new Vector2((float)(mess.ReadDouble() * absolute_level_size.X), (float)(mess.ReadDouble() * absolute_level_size.Y)), rotation = mess.ReadFloat(), Weapon = (WeaponItem.WeaponType)mess.ReadByte() };
        }

        public void WriteNewWeaponItem(WeaponItem item, NetBuffer mess)
        {
            mess.Write((Int32)item.Net_ID);
            mess.Write((byte)item.Weapon_type);
            mess.Write(Normalize(item.Position.X));
            mess.Write(Normalize(item.Position.Y));
        }

        public WeaponItemInfo ReadNewWeaponItem(NetBuffer mess)
        {
            return new WeaponItemInfo() { ID = mess.ReadInt32(), type = (WeaponItem.WeaponType)mess.ReadByte(), position = new Vector2(mess.ReadFloat(), mess.ReadFloat()) * SC.resv_ratio };
        }

        public void WriteShotByWeapon(NetBuffer buff, Shot shot, byte player_id)
        {
            buff.Write(player_id); // Player id
            buff.Write((Int32)shot.Net_ID); // Shot id
            buff.Write((double)shot.Startup_position.X / (double)DataTranslator.Level_absolute_size.X); // Shot startup position x
            buff.Write((double)shot.Startup_position.Y / (double)DataTranslator.Level_absolute_size.Y); // Shot startup position y
            buff.Write(shot.Movement_angle);
        }


        public struct ShortPlayerData
        {
            public byte id;
            public Vector2 position;
            public float rotation;
            public WeaponItem.WeaponType Weapon;
        }

        public struct WeaponItemInfo
        {
            public int ID;
            public WeaponItem.WeaponType type;
            public Vector2 position;
        }













      
    }
}
