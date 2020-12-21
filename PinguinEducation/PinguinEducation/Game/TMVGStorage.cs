using System;
using System.Collections.Generic;
using System.Text;

namespace PinguinEducation.Game
{
    class TMVGStorage
    {
        public Xamarin.Forms.Button first { get; set; }
        public Xamarin.Forms.Button second { get; set; }
        public int turn { get; set; }
        public int score { get; set; }
        public Xamarin.Forms.Color[] color { get 
            {
                return new Xamarin.Forms.Color[] { Xamarin.Forms.Color.FromHex("C6D8FF"), Xamarin.Forms.Color.FromHex("FFFADD"), Xamarin.Forms.Color.FromHex("FED6BC"), Xamarin.Forms.Color.FromHex("C3FBD8") };
            } }
    }
}
