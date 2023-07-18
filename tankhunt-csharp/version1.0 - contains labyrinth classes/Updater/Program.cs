using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
           /// if (args[0] == "")
             //   return;

           // string web_path = args[0];

            WebClient client = new WebClient();
            Console.WriteLine("Downloading...");
            client.DownloadFile("http://tankhunt.wz.cz/tank_hunt.zip", "downloaded.zip");



        }
    }
}
