using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JSFileMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            char last = 's';
            while (true)
            {
                try
                {
                    string resultFileName = "final.js";
                    StreamReader sr = new StreamReader("files.txt");
                    StreamWriter sw = new StreamWriter(resultFileName);



                    sw.WriteLine("$(function() {");
                    while (!sr.EndOfStream)
                    {
                        StreamReader fileSr = new StreamReader(sr.ReadLine());
                        string fileC = fileSr.ReadToEnd();
                        fileSr.Close();
                        sw.Write(fileC);
                        sw.Flush();
                    }
                    sw.WriteLine("});");
                    sw.Flush();
                    sw.Close();
                    sr.Close();
                    Console.WriteLine("Success!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }

                if (last == 's')
                    System.Diagnostics.Process.Start(@"..\index.html");
               
                ConsoleKeyInfo cki = Console.ReadKey();
                last = cki.KeyChar;
                if (last != 'a' && last != 's')
                    break;
                
                
            }
        }
    }
}
