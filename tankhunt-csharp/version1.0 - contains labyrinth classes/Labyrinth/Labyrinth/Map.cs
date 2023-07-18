using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth
{
     public class Map
    {
         public string Name { get; set; }                        
        public Sprite[,] Field { get; set; }
        public Vector2 Size
        {
            get
            {
                return new Vector2(Field.GetLength(0), Field.GetLength(1));
            }
        }
        private string File_path { get; set; }
        private List<Texture2D> Sprites_list { get; set; }
        public int Level_number { get; set; }
        public bool Editable { get; private set; }
        public bool Playable { get; private set; }
        public bool Unlocked { get; private set; }
        public Sprite Finish { get; set; }

        public Map(string name, Vector2 size, List<Texture2D> list, bool unlocked, bool playable, bool editable, int level_number)
        {
            Name = name;
            Field = new Sprite[(int)size.X, (int)size.Y];
            Sprites_list = list;
            Level_number = level_number;
            Editable = editable;
            Playable = playable;
            Unlocked = unlocked;
        }

        /// <summary>
        /// Clears whole map to specifed texture
        /// </summary>
        /// <param name="sprite_number">Specify texture by sprite number</param>
        public void ClearMap(int sprite_number)
        {
            for (int X  = 0; X < Field.GetLength(0); X++)
            {
                for (int Y = 0; Y < Field.GetLength(1); Y++)
                {
                    Field[Y, X] = new Sprite(Sprites_list[sprite_number], new Vector2(X * (float)Math.Round(80 * SC.res_ratio), Y * (float)Math.Round(80 * SC.res_ratio)), new Vector2((float)Math.Round(80 * SC.res_ratio), (float)Math.Round(80 * SC.res_ratio)), Color.White, sprite_number);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Jméno: {0}\n Velikost: {1}\n Cesta k souboru: {2}", Name, Size.ToString(), File_path);
        }
    }
}
