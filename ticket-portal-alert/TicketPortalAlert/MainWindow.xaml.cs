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
using System.Net;
using System.Windows.Threading;
using System.Media;
namespace TicketPortalAlert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebClient wc = new WebClient();
        DispatcherTimer dt = new DispatcherTimer();
        TicketPortal ticketPortal = new TicketPortal();
        int defaultInterval = 4;
        SoundPlayer successSong = new SoundPlayer("song.wav");
        SoundPlayer error = new SoundPlayer("error.wav");
        
        public MainWindow()
        {
            InitializeComponent();
            // Init timer
            dt.Tick += dt_Tick;
            dt.Interval = TimeSpan.FromSeconds(defaultInterval);

            try
            {
                successSong.Load();
                error.Load();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nelze nahrát zvuky!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
            

        }

        void dt_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!ContainsWord(DownloadSource(wc, tbUrl.Text), "vyprod&#225;no"))
                {
                    successSong.PlayLooping();
                    dt.Stop();
                }

                tblState.Text = "Stav: běží...";
            }
            catch (Exception ex)
            {
                error.Play();
                tblState.Text = "Stav: Něco se posralo.../posrává. Připojení na internet?";
            }
        }

        public string DownloadSource(WebClient wc, string url)
        {
            return wc.DownloadString(url);
            
        }

        public bool ContainsWord(string str, string contSTR)
        {
           // int startIndex = str.IndexOf("acc_block_box");
           // int endIndex = str.IndexOf("<!--end acc_container-->");
            //string strToSearchIn = str.Substring(startIndex, endIndex - startIndex);
            return str.Contains(contSTR);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            
        }

        private void tbUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            tblurlStatus.Text = "Čekejte prosím, kontroluji adresu...";
            
            try
            {              
              //  string actionName = ticketPortal.GetActionName(DownloadSource(wc, tbUrl.Text));
              //  tblurlStatus.Text = actionName;
               butStart.IsEnabled = true;
            }
            catch
            {
                tblurlStatus.Text = "Neplatná adresa.";
                butStart.IsEnabled = false;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dt.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Start test
            successSong.Play();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            successSong.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                string myIP = wc.DownloadString("http://tankhunt.wz.cz/pokusy/onlyIP.txt");
                string onlyIP = wc.DownloadString("http://icanhazip.com");
                onlyIP = onlyIP.Remove(onlyIP.Length - 1);
                if (myIP == "" || onlyIP == "")
                {
                    throw new Exception("Něco se podělalo, zkontrolujte připojení k internetu!");
                }

                if (myIP != onlyIP)
                {
                    //throw new Exception("Nejste oprávněn/a používat tento program! Pro oprávnění zažádejte autora.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }


    }
}
