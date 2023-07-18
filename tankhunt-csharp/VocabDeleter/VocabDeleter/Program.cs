using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VocabDeleter
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("cs_CZ.dic", Encoding.UTF8);
            StreamWriter sw = new StreamWriter("new.dic", false, Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                sw.WriteLine(line.Split('/')[0]);
            }

            sw.Flush();
            sw.Close();
            sr.Close();

        }
    }
}
