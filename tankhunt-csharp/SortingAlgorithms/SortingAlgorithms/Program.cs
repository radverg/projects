using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            // Merge two arrays ---- 
            int[] first = { 1, 3, 20, 21, 45, 90 };
            int[] second = { 4, 46, 100, 130 };
            int[] final = new int[first.Length + second.Length];
            
            for (int j = 0, total = final.Length, firstIndex = 0, secondIndex = 0 ; j < total; j++)
			{
                if (firstIndex < first.Length && secondIndex < second.Length)
                {
                   if (first[firstIndex] > second[secondIndex])
                   {
                       final[j] = second[secondIndex];
                       secondIndex++;
                   }
                   else
                   {
                       final[j] = first[firstIndex];
                       firstIndex++;
                   }
                }
                else if (firstIndex >= first.Length)			 
                {
                    final[j] = second[secondIndex];
                    secondIndex++;
                }
                else 
                {
                    final[j] = first[firstIndex];
                    firstIndex++;
                }
                   
			}


            // -----


           /* Random rnd = new Random();
            int length = 100000;
            int[] data = new int[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = rnd.Next(0, length * 2);
            }

            Stopwatch sw = new Stopwatch();

            sw.Start();
            /* Selection sort -----
            for (int i = 0; i < length; i++)
            {
                int currentMinIndex = i;
                for (int j = i; j < length; j++)
                {
                    if (data[j] < data[currentMinIndex])
                        currentMinIndex = j;
                }
                int temp = data[currentMinIndex];
                data[currentMinIndex] = data[i];
                data[i] = temp;
            }
            
            // -------------------
            sw.Stop();
            Console.WriteLine("\nSelection sort: " + sw.Elapsed.TotalMilliseconds.ToString());
            sw.Restart();
            // Bubble sort -----
            bool unfinished = true;
            while (unfinished)
            {
                unfinished = false;
                for (int i = 0; i < length - 1; i++)
                {
                    if (data[i] > data[i + 1])
                    {
                        unfinished = true;
                        int temp = data[i];
                        data[i] = data[i + 1];
                        data[i + 1] = temp;
                    }
                }
            }
            sw.Stop();
            // ----------
           /* string output = "";
            for (int i = 0; i < length; i++)
            {
                output += data[i].ToString() + " ";
            } 
           // Console.Write(output);
            Console.WriteLine("\nBubble sort: " + sw.Elapsed.TotalMilliseconds.ToString());
            Console.ReadKey();
            */
        }
    }
}
