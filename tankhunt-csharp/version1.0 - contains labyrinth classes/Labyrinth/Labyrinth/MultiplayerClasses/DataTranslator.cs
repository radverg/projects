using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;

namespace Labyrinth
{
    public class DataTranslator
    {
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
            RandomLevel level = new RandomLevel(false);
            level.Square_size = (int)(mess.ReadFloat() * SC.res_ratio);
            level.Size = new Vector2(mess.ReadFloat(), mess.ReadFloat()) * SC.resv_ratio;
            int walls_count = mess.ReadInt32();
            for (int i = 0; i < walls_count; i++)
            {
                Sprite new_wall = new Sprite();
                new_wall.Position = new Vector2(mess.ReadFloat(), mess.ReadFloat()) * SC.resv_ratio;
                if (mess.ReadByte() == 0)
                {
                    new_wall.Texture = RandomLevel.Horizontal_wall;
                    new_wall.Size = new Vector2(level.Square_size + RandomLevel.Horizontal_wall.Height, RandomLevel.Horizontal_wall.Height);
                }
                else
                {
                    new_wall.Texture = RandomLevel.Vertical_wall;
                    new_wall.Size = new Vector2(RandomLevel.Vertical_wall.Width, level.Square_size + RandomLevel.Vertical_wall.Width);
                }
                level.Walls.Add(new_wall);
            }
            return level;
        }

        public void WriteLevel(NetBuffer mess, RandomLevel level)
        {
            mess.Write(Normalize(level.Square_size));
            mess.Write(Normalize(level.Size.X));
            mess.Write(Normalize(level.Size.Y));
            mess.Write((Int32)level.Walls.Count);
            foreach (Sprite s in level.Walls)
            {
                mess.Write(Normalize(s.Position.X));
                mess.Write(Normalize(s.Position.Y));
                if (s.Texture == RandomLevel.Vertical_wall)
                    mess.Write((byte)1);
                else
                    mess.Write((byte)0);
            }
        }

       
        public void WriteAllPlayerInfo(NetBuffer mess, TankPlayerSprite player)
        {
            mess.Write((byte)player.Net_ID);
            mess.Write(player.Player_name);
            mess.Write(Normalize(player.Position.X));
            mess.Write(Normalize(player.Position.Y));
            mess.Write((float)player.Rotation);
            mess.Write((Int32)player.Wins);
            mess.Write((Int32)player.Kills);
            mess.Write((Int32)player.Deaths);
        }

        public TankPlayerSprite ReadAllPlayerInfo(NetBuffer mess)
        {
            TankPlayerSprite info = new TankPlayerSprite(null, Vector2.Zero, Vector2.Zero, Color.White, -1);
            info.Net_ID = mess.ReadByte();
            info.Player_name = mess.ReadString();
            info.Position = new Vector2(mess.ReadFloat(), mess.ReadFloat()) * SC.resv_ratio;
            info.Rotation = mess.ReadFloat();
            info.Wins = mess.ReadInt32();
            info.Kills = mess.ReadInt32();
            info.Deaths = mess.ReadInt32();
            return info;
        }

        public void WritePlayerUpdate(NetBuffer mess, TankPlayerSprite player)
        {
            mess.Write((byte)player.Net_ID);
            mess.Write(Normalize(player.Position.X));
            mess.Write(Normalize(player.Position.Y));
            mess.Write((float)player.Rotation);
            if (player.Weapon == null)
                mess.Write((byte)WeaponItem.WeaponType.None);
            else
                mess.Write((byte)player.Weapon.Weapon_type);
        }

        public ShortPlayerData ReadPlayerUpdate(NetBuffer mess)
        {
            return new ShortPlayerData() { id = mess.ReadByte(), position = new Vector2(mess.ReadFloat(), mess.ReadFloat()) * SC.resv_ratio, rotation = mess.ReadFloat(), Weapon = (WeaponItem.WeaponType)mess.ReadByte() };
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
            buff.Write(Normalize(shot.Startup_position.X)); // Shot startup position x
            buff.Write(Normalize(shot.Startup_position.Y)); // Shot startup position y
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
