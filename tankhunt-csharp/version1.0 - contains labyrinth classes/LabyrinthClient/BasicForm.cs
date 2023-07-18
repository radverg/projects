using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace LabyrinthClient
{
    public partial class BasicForm : Form
    {
        string Player_name { get; set; }
        /// <summary>
        /// Object of class FileFolderManager, managing directories, program files, and levelset objects
        /// </summary>
        FileFolderManager fmanager;
        /// <summary>
        /// Window dialog for creating new level
        /// </summary>
        NewLevelDialog new_level_dialog = new NewLevelDialog();
        /// <summary>
        /// Window dialog for setting game
        /// </summary>

        public BasicForm()
        {
            InitializeComponent();
            fmanager = new FileFolderManager(this);
            
            fmanager.RefreshSetListBox();

     
          

        }

        /// <summary>
        /// This is called when user select another item from listbox of levelsets
        /// </summary>     
        private void listBoxSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSet.SelectedIndex != -1)
            {
                fmanager.Selected_set = fmanager.GetLevelSetObject(listBoxSet.SelectedItem.ToString());
                fmanager.RefreshLevelListBox();
                fmanager.Selected_set.Selected_level = null;

            }
            else
            {
                fmanager.Selected_set = null;
          

            }

      
        }

       
        private void nováToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string set_name = Interaction.InputBox("Zadej název sady:", "Nová sada");
            if (set_name != "")
            {
                fmanager.CreateNewSet(set_name);
                fmanager.RefreshSetListBox();
            }
        }

        private void odstranitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fmanager.Selected_set != null)
            {
                if (MessageBox.Show("Opravdu odstranit sadu " + listBoxSet.SelectedItem.ToString() + "?", "Odstranit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    fmanager.Level_sets.Remove(fmanager.Selected_set);
                    fmanager.RemoveLevelSetFile(listBoxSet.SelectedItem.ToString());
                    listBoxSet.Items.RemoveAt(listBoxSet.SelectedIndex);
                    listBoxLevel.Items.Clear();
                    fmanager.Selected_set = null;
                }
            }
            else
                MessageBox.Show("Vyberte sadu k odstranění", "Vybrat sadu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             
        }

        private void nováToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            if (new_level_dialog.ShowDialog() == DialogResult.OK)
            {
                fmanager.Selected_set.CreateNewLevel(new_level_dialog.textBox1.Text, (int)new_level_dialog.numericUpDown1_width.Value, (int)new_level_dialog.numericUpDown2_height.Value, true, true, true);
                fmanager.RefreshLevelListBox();
            }
        }

        private void nastaveníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmanager.Settings.ShowDialog();
        }

        private void BasicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            fmanager.SaveSets();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (listBoxLevel.SelectedItems.Count != 0)
            {
                if (fmanager.WriteTemporary())
                    RunGame();     
            }
            else
                MessageBox.Show("Před spuštěním hry musíte nejdříve vybrat level, který chcete hrát!", "Vyberte level", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void RunGame()
        {
            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxLevel.SelectedItems.Count != 0)
            {
                if (fmanager.WriteTemporary())
                    RunEditor();     
            }
            else
                MessageBox.Show("Před spuštěním editoru musíte nejdříve vybrat level, který chcete editovat!", "Vyberte level", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        
        }

        private void RunEditor()
        {

            string Editor_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin") + "\\Labyrinth_editor.exe";
            if (File.Exists(Editor_path))
            {
                Process editor = new Process();
                editor.StartInfo.FileName = Editor_path;
                editor.Start();
                this.Hide();
                editor.WaitForExit();
                this.Show();
                fmanager.Selected_set.LoadSet();
            }
            else
                MessageBox.Show("Nebyl nalezen spustitelný soubor editoru. Přeinstalujte program nebo soubor vložte do složky se hrou do podsložky bin.", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);            
           
        }

        private void posunoutDolůToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (fmanager.Selected_set.Selected_level != null && fmanager.Selected_set.Levels.IndexOf(fmanager.Selected_set.Selected_level) != fmanager.Selected_set.Levels.Count - 1)
            {
                fmanager.Selected_set.Levels[fmanager.Selected_set.Levels.IndexOf(fmanager.Selected_set.Selected_level) + 1].Level_number -= 1;
                fmanager.Selected_set.Selected_level.Level_number += 1;
                fmanager.Selected_set.SortLevels();
                fmanager.RefreshLevelListBox();
            }
        }

        private void posunoutNahoruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fmanager.Selected_set.Selected_level != null && fmanager.Selected_set.Levels.IndexOf(fmanager.Selected_set.Selected_level) != 0)
            {
                fmanager.Selected_set.Levels[fmanager.Selected_set.Levels.IndexOf(fmanager.Selected_set.Selected_level) - 1].Level_number += 1;
                fmanager.Selected_set.Selected_level.Level_number -= 1;
                fmanager.Selected_set.SortLevels();
                fmanager.RefreshLevelListBox();
            }
        }

        private void listBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxLevel.SelectedIndex != -1 && fmanager.Selected_set != null)
            {
                fmanager.Selected_set.Selected_level = fmanager.Selected_set.GetLevelObject(listBoxLevel.SelectedItem.ToString());
            }
            else if (listBoxLevel.SelectedIndex == -1)
                fmanager.Selected_set.Selected_level = null;
        }

        private void přejmenovatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fmanager.Selected_set != null)
            {
                string new_name = Interaction.InputBox("Zadej nový název sady:", "Přejmenování");
                if (new_name != "")
                {
                    File.Delete(fmanager.Selected_set.SPath);
                    fmanager.Selected_set.Name = new_name;
                    fmanager.Selected_set.SaveSet();
                    fmanager.RefreshSetListBox();
                    fmanager.Selected_set = null;
                }
            }
            else
            {
                MessageBox.Show("Vyberte sadu pro přejmenování!", "Vybrat sadu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonMulti_Click(object sender, EventArgs e)
        {
            this.Hide();
            string name = Interaction.InputBox("Jméno hráče:", "Jméno");
            if (name != "")
                Player_name = name;
            else
                Player_name = "UNNAMED";
            MultiplayerForm m_form = new MultiplayerForm();
            m_form.player_name = Player_name;
            m_form.ShowDialog();
            this.Show();
           
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {
           
          
        }

       

       
      

      
    }
}
