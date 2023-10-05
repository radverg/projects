using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using system32;

namespace CompSpendTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataManager data_m = new DataManager();
        Timer timer;
        NotifyIcon notify_icon;
        int length = 0;
        DispatcherTimer hidtimer = new DispatcherTimer();

        public MainWindow()
        {
 
            InitializeComponent();
            hidtimer.Tick += new EventHandler(hidtimer_Tick);
            hidtimer.Interval = new TimeSpan(0,0,0,0,1);
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "icon1.ico"))
            {
                System.Windows.Forms.MessageBox.Show(@"Nebyl nalezen soubor ikony icon1.ico do notifikační oblasti systému Windows. Ujistěte se, že se soubor nachází ve stejné složce jako applikace. ", "CHyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            runMinimizedCheckBox.IsChecked = data_m.Run_hidden;
            todayTextBlock.Text = DateTime.Now.ToString();
            timer = new Timer(this, data_m);
            notify_icon = new NotifyIcon();
          
            notify_icon.Click += new EventHandler(notify_icon_Click);
            runOnStartUpCheckBox.IsChecked = RegistryManag.IsSet();
            notify_icon.Icon = new Icon(AppDomain.CurrentDomain.BaseDirectory + "icon1.ico", 15, 15);
      
            UpdateControls();
            timer.Run();
            this.Title = "Měřič času v1.0 - Spuštěn";
            notify_icon.Visible = true;

            if (data_m.Run_hidden)
            {
                this.WindowState = System.Windows.WindowState.Minimized;
                this.ShowInTaskbar = false;
                hidtimer.Start();
            }
         
          
           
        }


        private void hidtimer_Tick(object sender, EventArgs e)
        {
            this.Hide();
            hidtimer.Stop();
        }

        private void notify_icon_Click(object sender, EventArgs e)
        {

            notify_icon.Visible = false;
            this.Show();
            ShowInTaskbar = true;
            WindowState = System.Windows.WindowState.Normal;
           
        }

       public void UpdateControls()
        {
            firstRunTextBlock.Text = string.Format("{0} ({1} dní)", data_m.FirstRun.ToShortDateString(), data_m.TimeFromFirstRun.Days);
            totalTimeOnComputerTextBox.Text = string.Format("{0} dní {1} hod {2} min" ,data_m.TotalTimeSpent.Days, data_m.TotalTimeSpent.Hours, data_m.TotalTimeSpent.Minutes);
            todayOnComputerTextBox.Text = string.Format("{0} hod {1} min", data_m.TodayTimeSpent.Hours, data_m.TodayTimeSpent.Minutes);
            yesterdayOnComputerTextBox.Text = string.Format("{0} hod {1} min", data_m.YesterdayTimeSpent.Hours, data_m.YesterdayTimeSpent.Minutes);
            averageOncomputerTextBox.Text = string.Format("{0} hod {1} min", data_m.AverageSpentTime.Hours, data_m.AverageSpentTime.Minutes);
            maxOnComputerTextBox.Text = string.Format("{0} hod {1} min", data_m.MaxDayTimeSpent.Hours, data_m.MaxDayTimeSpent.Minutes);
            todayTextBlock.Text = DateTime.Now.ToString();
        }

      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Run();
            this.Title = "Měřič času v1.0 - Spuštěn";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            this.Title = "Měřič času v1.0 - Zastaven";
        }
    
        private void hideButton_Click(object sender, RoutedEventArgs e)
        {
            
            notify_icon.Visible = true;
            this.Hide();
           
        }

      

        private void runOnStartUpCheckBox_Click(object sender, RoutedEventArgs e)
        {
            
            if (runOnStartUpCheckBox.IsChecked.Value)
                RegistryManag.SetRegister(true);
            else
                RegistryManag.SetRegister(false);
        }

        private void runMinimizedCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void runMinimizedCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (runMinimizedCheckBox.IsChecked.Value)
            {
                data_m.Run_hidden = true;

            }
            else
                data_m.Run_hidden = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}
