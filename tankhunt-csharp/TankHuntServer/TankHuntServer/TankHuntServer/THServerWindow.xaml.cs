using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.IO;
using Microsoft.Win32;
using TankHuntServer.WebSocketTankHuntServer;

namespace TankHuntServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FormDataManager fm = new FormDataManager();
     
        UDPLidgrenServer udpServer = new UDPLidgrenServer();
        WebSocketServer webSocketServer = new WebSocketServer();

        public MainWindow()
        {
            InitializeComponent();
            new Window1().Show();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();

            string[] args = Environment.GetCommandLineArgs();
            try
            {
                fm.LoadData();
                chbRunHiddenOnStartup.IsChecked = fm.Checked;
                tbPort.Text = fm.Port.ToString();
                foreach (string s in args)
                {
                    if (s == "hidden")
                    {
                        udpServer.CreateServer(fm.Port);
                        ShowInTaskbar = false;
                        Hide();
                        break;
                    }
                }
            }
            catch
            {
                fm.SaveData();
            }
        }

        void timer_Tick(object sender, EventArgs e) // Refresh listboxes
        {
            listBoxmessageLog.ItemsSource = null;
            listBoxPeersConnected.ItemsSource = null;

            if (comboBoxServer.SelectedIndex == 0) // Udp server?
            {
                listBoxmessageLog.ItemsSource = udpServer.messageLog.Messages;
                listBoxPeersConnected.ItemsSource = udpServer.GetPeerStrings();   
           

            }
            else // WebSocket server
            {
                listBoxmessageLog.ItemsSource = webSocketServer.MsgLog.Messages;
            }

            listBoxmessageLog.SelectedIndex = listBoxmessageLog.Items.Count - 1;
            listBoxmessageLog.SelectedIndex = -1;

 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void chbRunHiddenOnStartup_Checked(object sender, RoutedEventArgs e)
        {
            RegistryKey runAppsKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            runAppsKey.SetValue("TankHuntServer", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + " hidden");
            fm.Checked = (bool)chbRunHiddenOnStartup.IsChecked;
            fm.SaveData();
            
        }

        private void chbRunHiddenOnStartup_Unchecked(object sender, RoutedEventArgs e)
        {
            RegistryKey runAppsKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            
            if (((string)runAppsKey.GetValue("TankHuntServer")) != "")
            {
                runAppsKey.DeleteValue("TankHuntServer");
            }
            fm.Checked = (bool)chbRunHiddenOnStartup.IsChecked;
            fm.SaveData();
        }

        private void btnHideWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowInTaskbar = false;
            Hide();
        }

        private void btnStartServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxServer.SelectedIndex == 0)
                    udpServer.CreateServer(int.Parse(tbPort.Text));
                else
                    webSocketServer.StartListening(int.Parse(tbPort.Text));
            }
            catch
            {
                udpServer.messageLog.CreateMessage("Failed! Server hasn't been started.");
            }
        }

        private void btnStopServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                udpServer.StopServer();
               
            }
            catch (Exception ex)
            {
                udpServer.messageLog.CreateMessage("Error!\n" + ex.Message);
                throw;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                fm.SaveData();
            }
            catch
            {
            }
        }

        private void tbPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                fm.Port = int.Parse(tbPort.Text);
            }
            catch
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
