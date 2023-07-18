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
using Microsoft.Xna.Framework;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace TankHuntWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int pos = 0;
        Image img = new Image();
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan elapsed;
        DateTime lastFrame;
        private Random rnd = new Random();
        private List<Sprite> sprts = new List<Sprite>();

        public MainWindow()
        {
            InitializeComponent();
            img.Source = new BitmapImage(new Uri("Content/Sprites/circle_tank.png", UriKind.Relative));
            img.Width = 40;
            img.Height = 40;
            timer.Interval = TimeSpan.FromMilliseconds(12);
            timer.Start();
            timer.Tick += timer_Tick;
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            elapsed = DateTime.Now - lastFrame;


            foreach (Sprite s in sprts)
            {
                s.posX = s.posX += elapsed.TotalMilliseconds * s.velocity * s.velocityX;
                s.posY = s.posY += elapsed.TotalMilliseconds * s.velocity * s.velocityY;


                Canvas.SetLeft(s.img, s.posX);
                Canvas.SetTop(s.img, s.posY);
            }

            lastFrame = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
           for (int i = 0; i < 20; i++)
           {
               Sprite newone = new Sprite() { img = new Image() { Width = 40, Height = 40, Source = new BitmapImage(new Uri("Content/Sprites/circle_tank.png", UriKind.Relative)) }, posX = -20, posY = -20, size = 40, velocity = 0.4, velocityX = rnd.NextDouble(), velocityY = rnd.NextDouble() };
               sprts.Add(newone);
               can.Children.Add(newone.img);
           }
            
          
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            
            
        }

    }

    public class Sprite
    {
        public Image img;

        public int size;
        public double velocityX;
        public double velocityY;

        public double velocity;
        public double posX;
        public double posY;

    }
}
