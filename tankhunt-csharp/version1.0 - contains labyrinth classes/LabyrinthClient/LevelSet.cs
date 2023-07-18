using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace LabyrinthClient
{
    class LevelSet
    {
        private FileFolderManager f_manager;
        private BasicForm basic_form;
        public List<Level> Levels { get; private set; }
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
                SPath = Path.Combine(f_manager.Level_sets_folder, value + ".dat");
            }
        }
        public string SPath { get; set; }
        public bool Playable { get; set; }
        public bool Editable { get; set; }
        public int Levels_count { get; set; }
        public Level Selected_level { get; set; }

        // Ads object level into collection levels
        public void AddLevel(Level level)
        {
            Levels.Add(level);
        }

        public LevelSet(string name, FileFolderManager f_manager, BasicForm basic_form)
        {
            this.f_manager = f_manager;
            Name = name;
            Levels = new List<Level>();
            this.basic_form = basic_form;                    
        }

        public void RefreshLevelListBox()
        {
          
            foreach (Level item in Levels)
            {
                basic_form.listBoxLevel.Items.Clear();
                basic_form.listBoxLevel.Items.Add(item.Name);
            }
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
                    bwriter.Write(Levels[i_level].Size_x);
                    bwriter.Write(Levels[i_level].Size_y);
                    bwriter.Write(Levels[i_level].Unlocked);
                    bwriter.Write(Levels[i_level].Editable);
                    bwriter.Write(Levels[i_level].Playable);


                    for (int dim_y = 0; dim_y < Levels[i_level].Size_y; dim_y++)
                    {
                        for (int dim_x = 0; dim_x < Levels[i_level].Size_x; dim_x++)
                        {
                            bwriter.Write(Levels[i_level].Elements[dim_x, dim_y]);

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
        public void LoadSet()
        {
            Levels.Clear();
            BinaryReader breader = new BinaryReader(new FileStream(SPath, FileMode.Open));
            try
            {
                breader.ReadString();
                Levels_count = breader.ReadInt32();
                Editable = breader.ReadBoolean();
                Playable = breader.ReadBoolean();

                for (int i_level = 0; i_level < Levels_count; i_level++)
                {
                    Levels.Add(new Level(breader.ReadString(), breader.ReadInt32(), breader.ReadInt32(), breader.ReadBoolean(), breader.ReadBoolean(), breader.ReadBoolean(), i_level));
                    for (int dim_y = 0; dim_y < Levels[i_level].Size_y; dim_y++)
                    {
                        for (int dim_x = 0; dim_x < Levels[i_level].Size_x; dim_x++)
                        {
                            Levels[i_level].Elements[dim_x, dim_y] = breader.ReadInt32();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Chyba při načítání souboru!", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                breader.Close();
            }
        }

        public Level GetLevelObject(string name)
        {
            foreach (Level item in Levels)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }

        public void CreateNewLevel(string name, int size_x, int size_y, bool editable, bool playable, bool unlocked)
        {
            Levels.Add(new Level(name, size_x, size_y, unlocked, editable, playable, Levels.Count));
            Levels_count++;
            SaveSet();
            
        }

        public void SortLevels()
        {
            List<Level> sorted_levels = new List<Level>();
            for (int i = 0; i < Levels.Count; i++)
            {
                for (int j = 0; j < Levels.Count; j++)
                {
                    if (Levels[j].Level_number == i)
                        sorted_levels.Add(Levels[j]);
                }
            }
            Levels = sorted_levels;
        }

        public void ResetDefaults()
        {
            Playable = true;
            Editable = true;
        }
      
        }
    }

