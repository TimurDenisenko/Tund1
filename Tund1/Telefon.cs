using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Tund1
{
    public class Telefon
    {
        public string Nimetus { get; set; }
        public string Tootja { get; set; }
        public int Hind { get; set; }
        public ImageSource Pilt { get; set; }
        
        public Telefon(string nimetus, string tootja, int hind, byte[] pilt) 
        { 
            Nimetus = nimetus;
            Tootja = tootja;
            Hind = hind;
            Pilt = ImageSource.FromStream(() => new MemoryStream(pilt));
        }
    }
}
