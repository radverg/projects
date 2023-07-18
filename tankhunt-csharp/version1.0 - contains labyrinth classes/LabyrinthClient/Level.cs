using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabyrinthClient
{
    class Level
    {
        private Random rnd = new Random();
        public string Name { get; set; }
        public bool Unlocked { get; set; }
        public bool Editable { get; set; }
        public bool Playable { get; set; }
        public int Size_x { get; set; }
        public int Size_y { get; set; }
        public int[,] Elements;
        public int Level_number { get; set; }

        public Level(string name, int size_x, int size_y,  bool unlocked, bool editable, bool playable, int level_number)
        {
            this.Name = name;
            this.Unlocked = unlocked;
            this.Editable = editable;
            this.Playable = playable;
            this.Size_x = size_x;
            this.Size_y = size_y;
            this.Level_number = level_number;

            Elements = new int[size_x, size_y];
            Level_Clear(3);
        }

        /// <summary>
        /// Clears every element of level to stated element number
        /// </summary>
        public void Level_Clear(int element_number)
        {
            for (int dim_y = 0; dim_y < Elements.GetLength(1); dim_y++)
			{
			    for (int dim_x = 0; dim_x < Elements.GetLength(0); dim_x++)
			{
                Elements[dim_x, dim_y] = element_number;
			 
			}
			}
        }

        public override string ToString()
        {
            return string.Format("Jméno: {0}\nVelikost: {1}, {2}\n Číslo levelu: {3}", Name, Size_x, Size_y, Level_number);
        }
    }
}
