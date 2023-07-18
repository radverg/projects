using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace LabyrinthClient
{
    public partial class MultiplayerForm : Form
    {
        TcpServer server = null;
        TcpUser client;
        public string player_name;
        public MultiplayerForm()
        {
            InitializeComponent();
        }

        private void MultiplayerForm_Load(object sender, EventArgs e)
        {
           
        }

        private void button_create_game_Click(object sender, EventArgs e)
        {
            server = new TcpServer();
            server.CreateServer(int.Parse(textBox_create_host_port.Text));
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(server.AcceptClient());
        }

        private void button_connect_to_game_Click(object sender, EventArgs e)
        {
            client = new TcpUser(int.Parse(textBox_host_port.Text), textBox_host_ip.Text, player_name);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (server == null)
            {

            }
        }


    }
}
