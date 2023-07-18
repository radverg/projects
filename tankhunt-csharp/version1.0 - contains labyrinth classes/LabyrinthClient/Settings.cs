using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LabyrinthClient
{
    class Settings
    {
        public int Screen_width { get; private set; }
        public int Screen_height { get; private set; }
        public bool Is_full_screen { get; private set; }
        public string File_path { get; private set; }
        private SetDialog set_dialog;

        public Settings(string file_path)
        {
            this.File_path = file_path;
            set_dialog = new SetDialog();
            
        }

        /// <summary>
        /// Shows dialog for settings and sets entered values if dialog was confirmed by button ok
        /// </summary>
        public void ShowDialog()
        {
            if (set_dialog.ShowDialog() == DialogResult.OK)
            {
                Screen_width = (int)set_dialog.numericUpDown1_width.Value;
                Screen_height = (int)set_dialog.numericUpDown2_height.Value;
                Is_full_screen = set_dialog.checkBox1_full_screen.Checked;
                SaveSettings();
            }
        }

        /// <summary>
        /// Sets default values of settings: resolution being used, fullscreen true
        /// </summary>
        public void SetDefaults()
        {
            Screen_width = Screen.PrimaryScreen.Bounds.Width;
            Screen_height = Screen.PrimaryScreen.Bounds.Height;
            Is_full_screen = true;
            SetComponents();
                
        }

        /// <summary>
        /// Sets components in settingform by values saved in properties
        /// </summary>
        private void SetComponents()
        {
            set_dialog.numericUpDown1_width.Maximum = Screen.PrimaryScreen.Bounds.Width;
            set_dialog.numericUpDown2_height.Maximum = Screen.PrimaryScreen.Bounds.Height;
            set_dialog.numericUpDown1_width.Value = Screen_width;
            set_dialog.numericUpDown2_height.Value = Screen_height;
            set_dialog.checkBox1_full_screen.Checked = Is_full_screen;
        }

        /// <summary>
        /// loades settings from file settings.dat
        /// </summary>
        public void LoadSettings()
        {
            StreamReader settings_reader = new StreamReader(File_path);
            try
            {
                Screen_width = int.Parse(settings_reader.ReadLine());
                Screen_height = int.Parse(settings_reader.ReadLine());
                Is_full_screen = bool.Parse(settings_reader.ReadLine());
                SetComponents();               
            }
            catch
            {
                MessageBox.Show("Chyba při čtení souboru s nastavením!\nJe možné, že došlo k poškození souboru uživatelem. Soubor bude smazán a znovu vytvořen s výchozím nastavením." , "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                settings_reader.Close();
                SaveSettings();

            }
   
        }

        /// <summary>
        /// Saves setting into file settings.dat
        /// </summary>
        public void SaveSettings()
        {
            StreamWriter settings_writer = new StreamWriter(File_path);
            try
            {
                settings_writer.WriteLine(Screen_width);
                settings_writer.WriteLine(Screen_height);
                settings_writer.WriteLine(Is_full_screen.ToString());
            }
            catch
            {
                MessageBox.Show("Chyba při zápisu do souboru s nastavením!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                settings_writer.Flush();
                settings_writer.Close();
            }
        }
    }
}
