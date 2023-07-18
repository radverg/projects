using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;




namespace BinaryReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.IO.BinaryReader br = new System.IO.BinaryReader(File.Open("level001.map", FileMode.Open));
            richTextBox1.Text = br.ReadChars(5)[4].ToString();
            

           // br.BaseStream.Position
               
        }
    }
}
