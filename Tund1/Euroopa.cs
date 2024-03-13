using System.IO;
using Xamarin.Forms;

namespace Tund1
{
    public class Euroopa
    {
        public string Nimi { get; set; }
        public string Pealinn { get; set; }
        public int Rahvaarv { get; set; }
        public ImageSource Lipp { get; set; }
        public Euroopa(string nimi, string pealinn, int rahvaarv, byte[] lipp)
        {
            Nimi=nimi;
            Pealinn=pealinn;
            Rahvaarv=rahvaarv;
            Lipp=ImageSource.FromStream(() => new MemoryStream(lipp));
        }
        public Euroopa(string nimi, string pealinn, int rahvaarv, ImageSource lipp)
        {
            Nimi=nimi;
            Pealinn=pealinn;
            Rahvaarv=rahvaarv;
            Lipp=lipp;
        }
        public Euroopa(string nimi, string pealinn, int rahvaarv)
        {
            Nimi=nimi;
            Pealinn=pealinn;
            Rahvaarv=rahvaarv;
            Lipp=ImageSource.FromStream(() => new MemoryStream(Properties.Resources.white));
        }
    }
}
