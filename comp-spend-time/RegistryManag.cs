using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows.Forms ;

namespace CompSpendTime
{
     static class RegistryManag
    {
         public static RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

         public static void SetRegister(bool value)
         {
             MessageBox.Show(Application.ExecutablePath.ToString());
             if (value)
                 regkey.SetValue("CompSpendTime", Application.ExecutablePath.ToString());
             else
                 regkey.DeleteValue("CompSpendTime", false);
         }

         public static bool IsSet()
         {
             if (regkey.GetValue("CompSpendTime") == null) return false;
             if (!File.Exists(regkey.GetValue("CompSpendTime").ToString()))
             {
                 regkey.SetValue("CompSpendTime", Application.ExecutablePath.ToString());
             }
             return true;
         }
    }

   
}
