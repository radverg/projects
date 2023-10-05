using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CompSpendTime
{
    class Timer
    {
        private MainWindow M_w {get; set; }
        private DataManager Data_m { get; set; }
        DispatcherTimer timer = new DispatcherTimer();
        public Point MousePosition { get; set; }
        public Point PreviousMousePosition { get; set; }
        public bool PressedKey { get; set; }


        public Timer(MainWindow wind, DataManager data_m)
        {
            M_w = wind;
            Data_m = data_m;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 30);
        }

        public void Run()
        {
            MousePosition = Control.MousePosition;
            PreviousMousePosition = MousePosition;
            timer.IsEnabled = true;
            timer.Start();
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
       
            MousePosition = Control.MousePosition;
            if (Data_m.CurrentDay != DateTime.Today)
            {
             
                Data_m.YesterdayTimeSpent = Data_m.TodayTimeSpent;
                Data_m.TodayTimeSpent = new TimeSpan(0, 0, 0);
                Data_m.CurrentDay = DateTime.Today;
                Data_m.SaveData(Path.Combine(Data_m.AppFolder, "Backup" + "data") + ".dat");
            }
           
            if (MousePosition != PreviousMousePosition || PressedKey)
            {
                           
                Data_m.TodayTimeSpent = Data_m.TodayTimeSpent.Add(new TimeSpan(0, 0, 30));
                Data_m.TotalTimeSpent = Data_m.TotalTimeSpent.Add(new TimeSpan(0, 0, 30));
                if (Data_m.TodayTimeSpent > Data_m.MaxDayTimeSpent)
                    Data_m.MaxDayTimeSpent = Data_m.TodayTimeSpent;
            }


            try { Data_m.SaveData(Data_m.DataFile); }
            catch { MessageBox.Show("Nepodařilo se uložit nová data!", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            PreviousMousePosition = MousePosition;
            PressedKey = false;
            M_w.UpdateControls();
        }



        public void Stop()
        {
            timer.IsEnabled = false;
            timer.Stop();
        }
    }
}
