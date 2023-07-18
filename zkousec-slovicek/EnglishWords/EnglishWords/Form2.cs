using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnglishWords
{
    public partial class Form2 : Form
    {
        List<Word> words;
        bool help_checked;

        public Form2(List<Word> words)
        {
            InitializeComponent();
            this.words = words;
            comboBox1.SelectedIndex = 0; 
        }

        public void FillListBox()
        {     
            listView1.Items.Clear();
            for (int i = 0; i < words.Count; i++)
            {
                ListViewItem listed_word = new ListViewItem();
                listed_word.Text = words[i].czech_word;
                listed_word.SubItems.Add(words[i].english_word);
                if (words[i].I_can)
                    listed_word.ForeColor = Color.Green;
                else
                    listed_word.ForeColor = Color.Red;
                listView1.Items.Add(listed_word);
            }
        }


        public void FillOnlyCan()
        {
            listView1.Items.Clear();
            for (int i = 0; i < words.Count; i++)
            {
                ListViewItem listed_word = new ListViewItem();
                listed_word.Text = words[i].czech_word;
                listed_word.SubItems.Add(words[i].english_word);
                if (words[i].I_can)
                    listed_word.ForeColor = Color.Green;
                else
                    listed_word.ForeColor = Color.Red;
                if (words[i].I_can)                
                    listView1.Items.Add(listed_word);
            }
        }

        public void FillOnlyCannot()
        {
            listView1.Items.Clear();
            for (int i = 0; i < words.Count; i++)
            {
                ListViewItem listed_word = new ListViewItem();
                listed_word.Text = words[i].czech_word;
                listed_word.SubItems.Add(words[i].english_word);
                if (words[i].I_can)
                    listed_word.ForeColor = Color.Green;
                else
                    listed_word.ForeColor = Color.Red;
                if (!words[i].I_can)              
                    listView1.Items.Add(listed_word);
            }
        }
        public int GetRealIndex()
        {
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].czech_word == listView1.SelectedItems[0].Text)
                {
                    return i;
                }
            }
            return -1;
        }

        public void ChangeICan()
        {         
            if (words[GetRealIndex()].I_can == true)
            {
                words[GetRealIndex()].I_can = false;
                listView1.SelectedItems[0].ForeColor = Color.Red;
            }
            else
            {
                words[GetRealIndex()].I_can = true;
                listView1.SelectedItems[0].ForeColor = Color.Green;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (help_checked)
            {
                if (listView1.SelectedItems.Count == 1)
                    ChangeICan();
                else
                    MessageBox.Show("Není vybráno slovíčko!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            help_checked = false;
            if (listView1.SelectedItems.Count == 1)
            {

                if (listView1.SelectedItems[0].ForeColor == Color.Green && checkBox1.Checked == false)
                    checkBox1.Checked = true;
                else if (listView1.SelectedItems[0].ForeColor == Color.Red && checkBox1.Checked == true)
                    checkBox1.Checked = false;
            }
            help_checked = true;            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    FillListBox();
                    break;
                case 1:
                    FillOnlyCannot();
                    break;
                case 2:
                    FillOnlyCan();
                    break;
            }
        }

        public string GetTrueFalse(Color color)
        {
            if (color == Color.Red)
                return "0";
            else
                return "1";
        }
    }
}
