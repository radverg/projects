using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TankHuntServer
{
    public class FormDataManager
    {
        public string dataFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TankHunt\\serverFormData.dat";


        public int Port { get; set; }
        public bool Checked { get; set; }

        public FormDataManager()
        {
            
        }

        public void SaveData()
        {
            if (!Directory.Exists(Path.GetDirectoryName(dataFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dataFilePath));
            }

            using (StreamWriter sv = new StreamWriter(dataFilePath))
            {
                sv.WriteLine(Port.ToString());
                sv.WriteLine(Checked.ToString());
            }
        }

        public void LoadData()
        {
            using (StreamReader sr = new StreamReader(dataFilePath))
            {
                Port = int.Parse(sr.ReadLine());
                Checked = bool.Parse(sr.ReadLine());
            }
        }

    }
}
