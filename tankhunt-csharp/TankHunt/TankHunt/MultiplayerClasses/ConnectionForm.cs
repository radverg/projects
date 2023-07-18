using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Lidgren.Network;
using System.Xml;
using System.IO;
using System.Net;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TankHunt
{
    public partial class ConnectionForm : Form
    {
        private ConnectionInfo connection_info;
        private Settings settings;
        string data_file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TankHunt\\form_data.xml");
        private KeyInputForm key_input_form = new KeyInputForm();

        private bool recounted = true;
        private bool Recounted { get { return recounted; } set { buttonApply.Enabled = !value; recounted = value; } }
        private byte[] trackBarBackup = new byte[9];
      

        private List<TrackBar> trackBars = new List<TrackBar>();

        public ConnectionForm(ConnectionInfo connection_info, Settings sett)
        {
            InitializeComponent();
            pictureBoxLogo.Image = Image.FromFile("Content/logo.png");
            pictureBoxBouncingLaser.Image = Image.FromFile("Content/weapon_item_bouncing_laser.png");
            pictureBoxEliminator.Image = Image.FromFile("Content/weapon_item_eliminator.png");
            pictureBoxLaser.Image = Image.FromFile("Content/weapon_item_laser.png");
            pictureBoxMine.Image = Image.FromFile("Content/weapon_item_mine.png");
            pictureBoxPulsar.Image = Image.FromFile("Content/weapon_item_pulsar.png");
            pictureBoxMultiShootCannon.Image = Image.FromFile("Content/weapon_item_multishot_cannon.png");
            pictureBoxDoubleMine.Image = Image.FromFile("Content/weapon_item_double_mine.png");
            pictureBoxRocket.Image = Image.FromFile("Content/weapon_item_rocket.png");
            pictureBoxFlatLaser.Image = Image.FromFile("Content/weapon_item_flat_laser.png");


            for (int i = 1; i < 10; i++) // Bind labels to trackbars
            {
                trackBars.Add((TrackBar)groupBoxSpawn.Controls["trackBar" + i.ToString()]);
                ((TrackBar)groupBoxSpawn.Controls["trackBar" + i.ToString()]).ValueChanged += new EventHandler(delegate(object sender, EventArgs e) {
                    ((Label)groupBoxSpawn.Controls["labelI" + ((TrackBar)sender).Name.Last()]).Text = ((TrackBar)sender).Value.ToString()+"%";
                    Recounted = false;
                });
            }

            for (int i = 0; i < panelKeys.Controls.Count; i++) // Set click events on keysbuttons
            {
                panelKeys.Controls[i].Click += new EventHandler(delegate(object sender, EventArgs e)
                {
                    key_input_form.ShowDialog();
                    ((Button)sender).Text = key_input_form.Key_code;
                });
            }

         
            settings = sett;
            this.connection_info = connection_info;
            List<DisplayMode> modes = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.ToList();
            foreach (DisplayMode mode in modes)
            {
                string mode_s = mode.Width + "x" + mode.Height;
                comboBox_res.Items.Add(mode_s);
            }
        }

        private void button_create_game_Click(object sender, EventArgs e)
        {
            int port = 0;
            
            if (textBox_player_name.Text == "" || !int.TryParse(textBox_create_host_port.Text, out port))
            {
                MessageBox.Show("Invalid input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            connection_info.Port = port;
            SetConInfo(true);
            SetKeys();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_connect_to_game_Click(object sender, EventArgs e)
        {
            int port = 0;
            int local_port = 0;
            if (textBox_host_ip.Text == "" || textBox_player_name.Text == "" || !int.TryParse(textBox_host_port.Text, out port))
            {
                MessageBox.Show("Invalid input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            connection_info.Port = port;
            SetConInfo(false);
            SetKeys();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SetConInfo(bool is_server)
        {
            connection_info.IP_adress = textBox_host_ip.Text;
            connection_info.Player_name = textBox_player_name.Text;

            connection_info.Server = is_server;
            connection_info.Max_players = (int)numericUpDown1.Value;

            connection_info.Darkness_percentage = (byte)numeric_up_down_percentage.Value;
            connection_info.StableFPS = checkBox_fps.Checked;
            connection_info.Vsync = checkbox_vsync.Checked;
            connection_info.Max_min_size = new Microsoft.Xna.Framework.Vector2((int)numericUpDown_max_squares.Value, (int)numericUpDown_min_squares.Value);
            connection_info.Max_min_square_size = new Microsoft.Xna.Framework.Vector2((int)numeric_up_down_max_square_size.Value, (int)numeric_up_down_min_square_size.Value);
            connection_info.Even_up = checkBox_even_up.Checked;
            if (!Recounted)
                RecountAndSetPercentages();
           
        }

        private void WriteInfoToXML()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TankHunt")))
                Directory.CreateDirectory((Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TankHunt")));

            XmlWriterSettings set = new XmlWriterSettings();
            set.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(data_file_path, set))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FormInput");

                IEnumerable<Control> controls_to_write = GetControls(this);
                foreach (Control c in controls_to_write)
                {
                    if (c.Name != "")
                    {
                        writer.WriteStartElement(c.Name);
                        if (c is TextBox)
                            writer.WriteValue((c as TextBox).Text);
                        else if (c is NumericUpDown)
                            writer.WriteValue((c as NumericUpDown).Value);
                        else if (c is CheckBox)
                            writer.WriteValue((c as CheckBox).Checked.ToString());
                        else if (c is TrackBar)
                            writer.WriteValue(trackBarBackup[byte.Parse(c.Name.Last().ToString()) - 1].ToString());
                        else if (c is Button)
                        {
                            if (c.Parent == Controls["tabControl"].Controls["tab_controls"])
                                writer.WriteValue((c as Button).Text);
                        }
                        writer.WriteEndElement();
                    }
                }              

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
        }

        private void ReadFromXml()
        {
            List<Control> readable_controls = GetControls(this).ToList();
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
                        Control current = readable_controls.Find((c) => c.Name == element_name);
                        if (current != null)
                        {
                            if (current is TextBox)
                                ((TextBox)current).Text = reader.Value;
                            else if (current is NumericUpDown)
                                ((NumericUpDown)current).Value = int.Parse(reader.Value);
                            else if (current is CheckBox)
                                ((CheckBox)current).Checked = bool.Parse(reader.Value);
                            else if (current is Button)
                                ((Button)current).Text = reader.Value;
                            else if (current is TrackBar)
                                ((TrackBar)current).Value = int.Parse(reader.Value);
                        }
                    }
                }
            }
            RecountAndSetPercentages();
        }

        private void ConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetKeys();
            WriteInfoToXML();
            settings.SaveSettings();
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {            
            try
            {
                ReadFromXml();
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

        private void comboBox_res_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] s_res = ((string)comboBox_res.SelectedItem).Split('x');
            settings.ChangeResolution(int.Parse(s_res[0]), int.Parse(s_res[1]));
            settings.SaveSettings();

            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://tankhunt.wz.cz");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                WriteInfoToXML();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot save settings! \n" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Process current_process = Process.GetCurrentProcess();
            string exe_path = Path.Combine(Environment.CurrentDirectory, current_process.ProcessName) + ".exe";
            Process.Start(exe_path);
            System.Threading.Thread.Sleep(500);
            Environment.Exit(0);
        }

        private IEnumerable<Control> GetControls(Control container)
        {
            List<Control> list_of_controls = new List<Control>();
            foreach (Control c in container.Controls)
            {
                list_of_controls.AddRange(GetControls(c));
                if (c is TextBox || c is NumericUpDown || c is CheckBox || c is Button || c is TrackBar)
                    list_of_controls.Add(c);
              
            }
            return list_of_controls;
        }

        private void linkLabel_ip_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://tankhunt.wz.cz/ip.php");
        }


        private void SetKeys()
        {
            SC.KeysAssociation.Clear();
            IEnumerable<Control> tab_controls = GetControls(Controls["tabControl"].Controls["tab_controls"]);
            Button[] buttons = (from but in tab_controls where but is Button select (Button)but).ToArray();
            foreach (Button b in buttons)
            {
                SC.KeysAssociation.Add(b.Name.Split('_')[2], (int)Enum.Parse(typeof(Keys), b.Text));
            }
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < trackBars.Count; i++)
            {
                trackBars[i].Value = 11;
            }
            RecountAndSetPercentages();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            RecountAndSetPercentages();
        }

        private void RecountAndSetPercentages()
        {
            
            int sum = (from t in trackBars select t.Value).Sum();
            trackBarBackup = (from tr in trackBars select (byte)tr.Value).ToArray();
            double ratio = (sum == 0) ? 0 : 100d / (double)sum;
            connection_info.SpawnProbabilities.Clear();
            for (byte i = 0; i < trackBars.Count; i++)
            {
                double exactValue = ratio * trackBars[i].Value;
                trackBars[i].Value = (int)Math.Ceiling(exactValue);
                groupBoxSpawn.Controls["labelI" + (i + 1).ToString()].Text = Math.Round(exactValue, 1).ToString() + "%";
                connection_info.SpawnProbabilities.Add(new KeyValuePair<byte, float>((byte)(i + 1), (float)Math.Round(exactValue, 2)));
            }
            Recounted = true;
            connection_info.SpawnProbabilities.Sort((k, l) => k.Value.CompareTo(l.Value));
           
        }
            
    }
}
