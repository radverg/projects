using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnglishWords
{
    public class Word
    {
        public string czech_word;
        public string english_word;
        public bool I_can;
        public bool viable;

        public Word(string czech, string english, string I_can)
        {
            czech_word = czech;
            english_word = english;
            if (I_can == "1")
                this.I_can = true;
            else
                this.I_can = false;
        }
    }
}
