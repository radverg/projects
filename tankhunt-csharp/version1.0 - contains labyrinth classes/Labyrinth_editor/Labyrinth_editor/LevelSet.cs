using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Windows.Forms;


namespace Labyrinth_editor
{
    public class LevelSet
    {
        public List<Map> Levels { get; private set; }
        private string name { get; set; }
        public string Name
        {
            get
            {
               
                return name;
            }
            set
            {
                name = value;
                SPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labyrinth", "Level_sets", value + ".dat");
            }
        }
        public List<Texture2D> Level_sprites;
        public string SPath { get; set; }
        public bool Playable { get; set; }
        public bool Editable { get; set; }
        public int Levels_count { get; set; }
        public Map Selected_level { get; set; }
        private string Edit_level_name { get; set; }

     
        public LevelSet(string name, List<Texture2D> level_sprites, string edit_level_name)
        {
            Name = name;
            Levels = new List<Map>();
            Level_sprites = level_sprites;
            Edit_level_name = edit_level_name;
            
            LoadSet(level_sprites);
        }

        /// <summary>
        /// Saves set to a binary file with same name as set
        /// </summary>
        public void SaveSet()
        {
            BinaryWriter bwriter = new BinaryWriter(new FileStream(SPath, FileMode.Create));
            try
            {               
                bwriter.Write(Name);
                bwriter.Write(Levels.Count);
                bwriter.Write(Editable);
                bwriter.Write(Playable);

                for (int i_level = 0; i_level < Levels.Count; i_level++)
                {
                    bwriter.Write(Levels[i_level].Name);
                    bwriter.Write(Levels[i_level].Field.GetLength(0));
                    bwriter.Write(Levels[i_level].Field.GetLength(1));
                    bwriter.Write(Levels[i_level].Unlocked);
                    bwriter.Write(Levels[i_level].Editable);
                    bwriter.Write(Levels[i_level].Playable);


                    for (int dim_y = 0; dim_y < Levels[i_level].Field.GetLength(1); dim_y++)
                    {
                        for (int dim_x = 0; dim_x < Levels[i_level].Field.GetLength(0); dim_x++)
                        {
                            //MessageBox.Show(Levels[i_level].Field[dim_x, dim_y].Writenumber.ToString());
                            bwriter.Write(Levels[i_level].Field[dim_x, dim_y].Writenumber);

                        }
                    }
                }              
            }
            catch
            {
                MessageBox.Show("Chyba při zápisu do souboru!", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                bwriter.Flush();
                bwriter.Close();
            }
        }


        /// <summary>
        /// Loades set and ads it into list, must corespond with method save set!
        /// </summary>
        public void LoadSet(List<Texture2D> textures)
        {
            Levels.Clear();
            BinaryReader breader = new BinaryReader(new FileStream(SPath, FileMode.Open));
           // try
           // {
                breader.ReadString();
                Levels_count = breader.ReadInt32();
                Editable = breader.ReadBoolean();
                Playable = breader.ReadBoolean();

                for (int i_level = 0; i_level < Levels_count; i_level++)
                {
                    Levels.Add(new Map(breader.ReadString(), new Vector2(breader.ReadInt32(), breader.ReadInt32()), Level_sprites, breader.ReadBoolean(), breader.ReadBoolean(), breader.ReadBoolean(), i_level));
                  //  MessageBox.Show(breader.ReadString() + "    " + breader.ReadInt32() + "   " + breader.ReadInt32().ToString() + "     " + breader.ReadBoolean() + breader.ReadBoolean() + breader.ReadBoolean());
                    for (int dim_y = 0; dim_y < Levels[i_level].Size.Y; dim_y++)
                    {
                        for (int dim_x = 0; dim_x < Levels[i_level].Size.X; dim_x++)
                        {

                      
                            int sprite_number = breader.ReadInt32();
                            Levels[i_level].Field[dim_x, dim_y] = new Sprite(Level_sprites[sprite_number], new Vector2(SC.square_size * dim_x, SC.square_size * dim_y), new Vector2(SC.square_size, SC.square_size), Color.White, sprite_number);
                     
                        }
                    
                    }
                 
                    if (Levels[i_level].Name == Edit_level_name)                    
                        Selected_level = Levels[i_level];
                  
                }
                breader.Close();
            }
         //   catch
         //   {
              // MessageBox.Show("Chyba při načítání souboru!", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
          //  }
           // finally
          //  {
           ///     breader.Close();
          //  }
        }

     
        }
    

