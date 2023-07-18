using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Net;

namespace Labyrinth
{
    public partial class ConnectionForm : Form
    {
        private ConnectionInfo connection_info;
        string data_file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labyrinth\\form_data.xml");
        public ConnectionForm(ConnectionInfo connection_info)
        {
            InitializeComponent();
            this.connection_info = connection_info;
        }

        private void button_create_game_Click(object sender, EventArgs e)
        {
            int port = 0;
            if (textBox_player_name.Text == "" || !int.TryParse(textBox_create_host_port.Text, out port))
            {
                MessageBox.Show("Invalid input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            connection_info.IP_adress = "";
            connection_info.Player_name = textBox_player_name.Text;
            connection_info.Port = port;
            connection_info.Server = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_connect_to_game_Click(object sender, EventArgs e)
        {
            int port = 0;
            if (textBox_host_ip.Text == "" || textBox_player_name.Text == "" || !int.TryParse(textBox_host_port.Text, out port))
            {
                MessageBox.Show("Invalid input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            connection_info.IP_adress = textBox_host_ip.Text;
            connection_info.Player_name = textBox_player_name.Text;
            connection_info.Port = port;
            connection_info.Server = false;
            DialogResult = DialogResult.OK;
            Close();
        }


        private void WriteInfoToXML()
        {
            XmlWriterSettings set = new XmlWriterSettings();
            set.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(data_file_path, set))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("FormInput");

                writer.WriteStartElement("Name");
                writer.WriteValue(textBox_player_name.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("PortForServer");
                writer.WriteValue(textBox_create_host_port.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("ServerIP");
                writer.WriteValue(textBox_host_ip.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("ServerPort");
                writer.WriteValue(textBox_host_port.Text);
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteEndDocument();

                writer.Flush();
            }
        }

        private void ReadFromXml()
        {
            using (XmlReader reader = XmlReader.Create(data_file_path))
            {
                string element_name = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        element_name = reader.Name;                       
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        switch (element_name)
                        {
                            case "Name": textBox_player_name.Text = reader.Value; break;
                            case "PortForServer": textBox_create_host_port.Text = reader.Value; break;
                            case "ServerIP": textBox_host_ip.Text = reader.Value; break;
                            case "ServerPort": textBox_host_port.Text = reader.Value; break;
                        }
                    }
                }
            }

        }

        private void ConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteInfoToXML();
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            try
            {
                ReadFromXml();
                tb_my_ip.Text = GetExternalIP();
            }
            catch
            {
                WriteInfoToXML();
            }
        }

        private string GetExternalIP()
        {
            WebClient client = new WebClient();
            return client.DownloadString("https://api.ipify.org");
        }

       
       
    }
}
