using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnglishWords
{
    public partial class Form1 : Form
    {
        private string file_path;
        private List<Word> words = new List<Word>();
        private Random rnd;
        private int word_number = 0;
        private Form2 word_list;

        public Form1()
        {           
            InitializeComponent();
            rnd = new Random();
            word_list = new Form2(words);         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                words.Clear();
                file_path = openFileDialog1.FileName;
                Read_file();
            }          
        }

        private bool Get_Viable()
        {
            foreach (Word w in words)
            {
                if (w.viable)
                    return true;
            }
            return false;
        }

        private bool Get_Cant_Viable()
        {
            foreach (Word w in words)
            {
                if (w.viable && !w.I_can)
                    return true;
            }
            return false;
        }

        private void Set_All_Viable()
        {
            foreach (Word w in words)
                w.viable = true;                                    
        }

        private void Generate_Cant_Word()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            if (Get_Cant_Viable())
            {
                do
                    word_number = rnd.Next(0, words.Count);
                while (!words[word_number].viable || words[word_number].I_can);
            }
            else
            {
                MessageBox.Show("Konec okruhu! Slovíčka začnou znova.", "Konec", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Set_All_Viable();
            }

            words[word_number].viable = false;

            if (radioButton1.Checked == true)
                textBox1.Text = words[word_number].czech_word;
            else
                textBox2.Text = words[word_number].english_word;

        }

        private void Generate_word()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            
                if (Get_Viable())
                {
                    do
                        word_number = rnd.Next(0, words.Count);
                    while (!words[word_number].viable);
                }
                else
                {
                    Set_All_Viable();
                    MessageBox.Show("Konec okruhu! Slovíčka začnou znova.", "Konec", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Save();
                }
                words[word_number].viable = false;

            if (radioButton1.Checked == true)            
                textBox1.Text = words[word_number].czech_word;           
            else
                textBox2.Text = words[word_number].english_word;           
        }

        private void Read_file()
        {
            try
            {
                StreamReader sr = new StreamReader(file_path);
                while (!sr.EndOfStream)
                {
                    words.Add(new Word(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                }
                sr.Close();
                Set_All_Viable();
            }
            catch
            {
                MessageBox.Show("Soubor nebyl úspěšně nahrán!", ":(", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void label2_Click(object sender, EventArgs e)
        {
           
            if (textBox2.ForeColor != Color.Green)
            {
                textBox2.Text = words[word_number].english_word;
                textBox2.ForeColor = Color.Green;
                words[word_number].I_can = true;
            }
            else
            {
                textBox2.Text = words[word_number].english_word;
                textBox2.ForeColor = Color.Red;
                words[word_number].I_can = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {            
            if (textBox1.ForeColor != Color.Green)
            {
                textBox1.Text = words[word_number].czech_word;
                textBox1.ForeColor = Color.Green;
                words[word_number].I_can = true;
            }
            else
            {
                textBox1.Text = words[word_number].czech_word;
                textBox1.ForeColor = Color.Red;
                words[word_number].I_can = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (words.Count != 0)
            {
                if (checkBox1.Checked == true)
                    Generate_Cant_Word();
                else
                    Generate_word();



                textBox1.ForeColor = Color.Black;
                textBox2.ForeColor = Color.Black;
            }
            else
                MessageBox.Show("Musíte načíst soubor!", "LOL", MessageBoxButtons.OK, MessageBoxIcon.Error);         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (words.Count != 0)
                MessageBox.Show("Cesta k souboru: " + file_path + "\nPočet slovíček: " + words.Count.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Musíte načíst soubor!", "LOL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {          
            word_list.FillListBox();
            word_list.ShowDialog();
        }

        private void Save()
        {
            StreamWriter writer = new StreamWriter(file_path);
            for (int i = 0; i < words.Count; i++)
            {
                writer.WriteLine(words[i].czech_word);
                writer.WriteLine(words[i].english_word);
                if (words[i].I_can)
                    writer.WriteLine("1");
                else
                    writer.WriteLine("0");               
            }
            writer.Close();
        }
       

        private void Form1_FormClosing(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("xD");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Nastavení bylo změměno, slovíčka začnou znova!", "LOL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Set_All_Viable();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(file_path))
                Save();
        }
    }
}
