using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketPortalAlert
{
    public class TicketPortal
    {
        public string URL { get; private set; }

        private string actionNameRecognizer = "ctl00_ContentPlaceHolder1_hNadpis";

        public TicketPortal()
        {
            URL = "http://ticketportal.cz";
        }

        /// <summary>
        /// Extracts the name of the action from page with details about action
        /// </summary>
        /// <param name="sourceCode">Web page source</param>
        /// <returns></returns>
        public string GetActionName(string sourceCode)
        {         
            string cuttedCode = sourceCode.Substring(sourceCode.IndexOf(actionNameRecognizer));
            int firstCharIndex = cuttedCode.IndexOf(">") + 1;
            string extracted = cuttedCode.Substring(firstCharIndex, cuttedCode.IndexOf("</h1>") - firstCharIndex);
            return NormalizeText(extracted);
        }

        public string NormalizeText(string text)
        {
           return Encoding.UTF8.GetString(Encoding.Default.GetBytes(text));
        }

    }
}
