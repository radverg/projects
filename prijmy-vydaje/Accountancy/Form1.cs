using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accountancy
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Collection of items
        /// </summary>

        int ActualAmount = 0;

        public Form1()
        {
            InitializeComponent();

            
       
            LoadContent();
            
        }

        /// <summary>
        /// Loades all data and saves it into "items"
        /// </summary>
        private void LoadContent()
        {
            using (StreamReader reader = new StreamReader("database.dat"))
            {
                while (!reader.EndOfStream)
                {
                    AddItem(DateTime.Parse(reader.ReadLine()), float.Parse(reader.ReadLine()), reader.ReadLine());
                }

                CountAmount();
            }
        }


        /// <summary>
        /// Writes data to "database.dat" from all items.
        /// </summary>
        private void WriteContent()
        {
            using (StreamWriter writer = new StreamWriter("database.dat"))
            {
                foreach (ListViewItem i in listView1.Items)
                {
                    writer.WriteLine(i.Text);
                    writer.WriteLine(i.SubItems[1].Text);
                    writer.WriteLine(i.SubItems[2].Text);                    
                }

                writer.Flush();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteContent();
        }

        private void AddItem(DateTime date, float amount, string note)
        {
           
            ListViewItem item = new ListViewItem();
            item.Text = date.ToString();
            item.SubItems.Add(amount.ToString());
            item.SubItems.Add(note);
            if (amount < 0)
                item.ForeColor = Color.Red;
            else
                item.ForeColor = Color.Blue;
            listView1.Items.Add(item);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                AddItem(dlg.dateTimePicker1.Value, (float)dlg.numericUpDown1.Value, dlg.textBox1.Text);
                CountAmount();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                CountAmount();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                Form2 dlg = new Form2();

                dlg.dateTimePicker1.Value = DateTime.Parse(listView1.SelectedItems[0].Text);
                dlg.numericUpDown1.Value = decimal.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                dlg.textBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    listView1.SelectedItems[0].Text = dlg.dateTimePicker1.Value.ToString();
                    listView1.SelectedItems[0].SubItems[1].Text = dlg.numericUpDown1.Value.ToString();
                    listView1.SelectedItems[0].SubItems[2].Text = dlg.textBox1.Text;

                    CountAmount();
                }
            }

        }

        private void CountAmount()
        {
            ActualAmount = 0;

            foreach (ListViewItem i in listView1.Items)
            {
                ActualAmount += int.Parse(i.SubItems[1].Text);
            }
            
            textBox1.Text = ActualAmount.ToString();
        }
    }
}
