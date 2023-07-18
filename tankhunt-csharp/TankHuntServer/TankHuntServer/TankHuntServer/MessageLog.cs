using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHuntServer
{
    public class MessageLog
    {
        public List<string> Messages { get; private set; }

        public MessageLog()
        {
            Messages = new List<string>();
        }

        public void CreateMessage(string message)
        {
            DateTime now = DateTime.Now;
            string result = string.Format("[{0}] {1}", now.ToString("T"), message);
            Messages.Add(result);
        }
    }
}
