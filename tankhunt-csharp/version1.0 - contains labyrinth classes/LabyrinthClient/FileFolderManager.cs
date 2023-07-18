using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabyrinthClient
{
    class FileFolderManager
    {
        public string App_data_ro {get; private set;}
        public BasicForm basic_form;
        public string Game_folder { get; private set; }
        public string Level_sets_folder { get; private set; }
        public List<LevelSet> Level_sets { get; private set; }
        public LevelSet Selected_set { get;  set; }
        public Settings Settings { get; set; }
        public string Temporary { get; private set; }

        public FileFolderManager(BasicForm basic_form)
        {
            App_data_ro = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Game_folder = Path.Combine(App_data_ro, "Labyrinth");
            Level_sets_folder = Path.Combine(Game_folder, "Level_sets");
            this.basic_form = basic_form;
            Level_sets = new List<LevelSet>();
            CreateGameFolders();
            Temporary = CreateTemporaryFile();
            LoadSets();
            Settings = new Settings(Path.Combine(Game_folder, "settings") + ".dat");
            Settings.SetDefaults();
            if (File.Exists(Path.Combine(Game_folder, "settings") + ".dat"))
                Settings.LoadSettings();
            else
                Settings.SaveSettings();
           
        }
        public bool WriteTemporary()
        {
            StreamWriter swriter = new StreamWriter(Temporary);
            try
            {
                swriter.WriteLine(Selected_set.Name);
                swriter.WriteLine(basic_form.listBoxLevel.SelectedItem);
                return true;
            }
            catch
            {
                MessageBox.Show("Chyba při zápisu do souboru temporary.dat!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                swriter.Flush();
                swriter.Close();
            }
        }

        /// <summary>
        /// Saves all sets to their files
        /// </summary>
        public void SaveSets()
        {
            foreach (LevelSet set in Level_sets)
            {
                set.SaveSet();
            }
        }

        /// <summary>
        /// Creates basic game folders in appdat\roaming
        /// </summary>
        private void CreateGameFolders()
        {
            // Create game folder if doesn't exist
            if (!Directory.Exists(Game_folder))
                Directory.CreateDirectory(Game_folder);

            // Create levels folder if doesn't exist
            if (!Directory.Exists(Level_sets_folder))
                Directory.CreateDirectory(Level_sets_folder);              
        }

        /// <summary>
        /// Creates temporary file which programs communicate through
        /// </summary>
        private string CreateTemporaryFile()
        {          
            string path = Path.Combine(Game_folder, "temporary") + ".dat";
            if (!File.Exists(path))
                File.Create(path);
            return path;
        }
        /// <summary>
        /// Removes specifed file from levelsets folder
        /// </summary>
        /// <param name="name">Name of the file you want to remove</param>
        public void RemoveLevelSetFile(string name)
        {
            string path = Path.Combine(Level_sets_folder, name) + ".dat";
            if (File.Exists(path))
                File.Delete(path);
           
        }

        /// <summary>
        /// Creates new level set and adds it into level set collection
        /// </summary>
        /// <param name="name">Name of level set</param>
        public void CreateNewSet(string name)
        {
            LevelSet new_level_set = new LevelSet(name, this, basic_form);
            Level_sets.Add(new_level_set);
            new_level_set.ResetDefaults();
            new_level_set.SaveSet();

        }

        // Loades level sets from Level_sets directory
        private void LoadSets()
        {
            string[] l;
            l = Directory.GetFiles(Level_sets_folder);
            for (int i = 0; i < l.Length; i++)
            {
                LevelSet loaded_set = new LevelSet(Path.GetFileNameWithoutExtension(l[i]), this, basic_form);
                Level_sets.Add(loaded_set);
                loaded_set.LoadSet();
            }
            if (Level_sets.Count == 0)
                MessageBox.Show("Nebyla nalezena žádná sada!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        /// <summary>
        /// Fills set listbox by  loaded levelsets
        /// </summary>
        public void RefreshSetListBox()
        {
            basic_form. listBoxSet.Items.Clear();
            foreach (LevelSet seet in Level_sets)
            {
                basic_form.listBoxSet.Items.Add(seet.Name);
            }
        }

        /// <summary>
        /// Fills level listbox by names of levels in currently selected set
        /// </summary>
        public void RefreshLevelListBox()
        {
            basic_form.listBoxLevel.Items.Clear();
            foreach (Level level in Selected_set.Levels)
            {
                basic_form.listBoxLevel.Items.Add(level.Name);
            }
        }

        /// <summary>
        /// Gets a reference to levelset object containing specifed name
        /// </summary>
        /// <param name="name">Name of levelset</param>
        /// <returns>reference to object levelset</returns>
        public LevelSet GetLevelSetObject(string name)
        {
            foreach (LevelSet item in Level_sets)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }

        
        
        



    }
}
