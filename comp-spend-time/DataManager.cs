using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CompSpendTime
{
    public class DataManager
    {
        public string AppFolder { get; set; }
        public string DataFile { get; set; }
        public TimeSpan TotalTimeSpent { get; set; }
        public TimeSpan TodayTimeSpent { get; set; }
        public TimeSpan YesterdayTimeSpent { get; set; }
        public TimeSpan TimeFromFirstRun { get { return DateTime.Today - FirstRun; } }
        public bool Run_hidden { get; set; }
        public TimeSpan MaxDayTimeSpent { get; set; }
        public TimeSpan AverageSpentTime { get {
          if (TimeFromFirstRun.Days == 0)
              return new TimeSpan(0,0,0);
          else
          {
            
              double total_hours_per_day = TotalTimeSpent.TotalHours / TimeFromFirstRun.TotalDays;
              return new TimeSpan((int)Math.Floor(total_hours_per_day), (int)((total_hours_per_day - Math.Floor(total_hours_per_day)) * 60), 0);
          }
             

      }  }
        public DateTime FirstRun { get; set; }
        public bool RunOnStartUp { get; set; }
        public DateTime CurrentDay { get; set; }
       
        

        public DataManager()
        {
            SetFilesAndFolders();
    
        }

        private void SetFilesAndFolders()
        {
            AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CompTime");
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);

            if (!Directory.Exists(Path.Combine(AppFolder, "Backup")))
                Directory.CreateDirectory(Path.Combine(AppFolder, "Backup"));

            DataFile = Path.Combine(AppFolder, "data") + ".dat";
            if (!File.Exists(DataFile))
            {
                ResetAllData();
                FirstRun = DateTime.Now;
                try { SaveData(DataFile);}
                catch { MessageBox.Show("Nepodařilo se uložit data!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                
            }
            else
            {
                try 
                {
                    LoadData();
                }
                catch
                {
                    try {
                        MaxDayTimeSpent = new TimeSpan(0);
                        LoadOldData(); }
                    catch { MessageBox.Show("Nepodařilo se nahrát data!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
             
                
      
                
            }
               
        }

        public void ResetAllData()
        {
            FirstRun = DateTime.Now;
            TotalTimeSpent = new TimeSpan(0);
            TodayTimeSpent = new TimeSpan(0);
            YesterdayTimeSpent = new TimeSpan(0);
            CurrentDay = DateTime.Today;
            RunOnStartUp = true;
            Run_hidden = false;
            MaxDayTimeSpent = new TimeSpan(0);
        }

        public void SaveData(string path)
        {
          
           using (StreamWriter writer = new StreamWriter(path))
           {
               writer.WriteLine(FirstRun.ToString());
               writer.WriteLine(TotalTimeSpent.ToString());
               writer.WriteLine(TodayTimeSpent.ToString());
               writer.WriteLine(YesterdayTimeSpent.ToString());
               writer.WriteLine(CurrentDay.ToString());
               writer.WriteLine(RunOnStartUp.ToString());
               writer.WriteLine(Run_hidden.ToString());
               writer.WriteLine(MaxDayTimeSpent.ToString());
               writer.Flush();
            
           }
                
        }

        public void LoadData()
        {
          
            using (StreamReader reader = new StreamReader(DataFile))
            {
                FirstRun = DateTime.Parse(reader.ReadLine());
                TotalTimeSpent = TimeSpan.Parse(reader.ReadLine());
                TodayTimeSpent = TimeSpan.Parse(reader.ReadLine());
                YesterdayTimeSpent = TimeSpan.Parse(reader.ReadLine());
                CurrentDay = DateTime.Parse(reader.ReadLine());
                RunOnStartUp = bool.Parse(reader.ReadLine());
                Run_hidden = bool.Parse(reader.ReadLine());
                MaxDayTimeSpent = TimeSpan.Parse(reader.ReadLine());
            }
        }

        public void LoadOldData()
        {
            using (StreamReader reader = new StreamReader(DataFile))
            {
                FirstRun = DateTime.Parse(reader.ReadLine());
                TotalTimeSpent = TimeSpan.Parse(reader.ReadLine());
                TodayTimeSpent = TimeSpan.Parse(reader.ReadLine());
                YesterdayTimeSpent = TimeSpan.Parse(reader.ReadLine());
                CurrentDay = DateTime.Parse(reader.ReadLine());
                RunOnStartUp = bool.Parse(reader.ReadLine());
                Run_hidden = bool.Parse(reader.ReadLine());
            }
        }
    }
}
