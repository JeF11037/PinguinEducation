using System;
using System.Collections.Generic;
using System.Text;

namespace PinguinEducation.Alphabet
{
    public class AlphabetStorage
    {
        public string[] ALPHABET_alphabet { get
            { return new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" }; } }
        public string ALPHABET_letter { get; set; }
        public string[] ALPHABET_unlocked { get; set; }
        public double goal { get; set; }
    }
}
