using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework;

namespace Labyrinth
{
    class Settings
    {
        public int Screen_width { get; private set; }
        public int Screen_height { get; private set; }
        public bool Is_full_screen { get; private set; }
        public string File_path { get; private set; }
        public bool Show_mouse { get; set; }

        public Settings(string file_path)
        {
            this.File_path = file_path;
            
        }

        /// <summary>
        /// Sets default values of settings: resolution being used, fullscreen true
        /// </summary>
        public void SetDefaults()
        {          
            Is_full_screen = true;
         
         
        }

        public void ApplyVideoSettings(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = Screen_width;
            graphics.PreferredBackBufferHeight = (int)((Screen_width / 16.0f) * 9);
            graphics.IsFullScreen = Is_full_screen;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// loades settings from file settings.dat
        /// </summary>
        public void LoadSettings()
        {
            using (StreamReader settings_reader = new StreamReader(File_path))
            {
                Screen_width = int.Parse(settings_reader.ReadLine());
                Screen_height = int.Parse(settings_reader.ReadLine());
                Is_full_screen = bool.Parse(settings_reader.ReadLine());
            }

        }

        /// <summary>
        /// Saves setting into file settings.dat
        /// </summary>
        public void SaveSettings()
        {
            using (StreamWriter settings_writer = new StreamWriter(File_path))
            {
                settings_writer.WriteLine(Screen_width);
                settings_writer.WriteLine(Screen_height);
                settings_writer.WriteLine(Is_full_screen.ToString());
            }
        }
    }
}
