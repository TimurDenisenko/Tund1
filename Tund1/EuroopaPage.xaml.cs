using System;
using System.Collections.ObjectModel;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EuroopaPage : ContentPage
    {
        ListView listView;
        Label lbl;
        Button Lisa, Kustuta;
        ObservableCollection<Euroopa> riigid = new ObservableCollection<Euroopa> { 
            new Euroopa("Eesti","Tallinn",1200000,Properties.Resources.estonia),
            new Euroopa("USA","Washington",330000000,Properties.Resources.usa),
            new Euroopa("Island","Reykjavík",372520,Properties.Resources.iceland),
            new Euroopa("Norra","Oslo",5408000,Properties.Resources.norway)
        };
        Euroopa selectedRiik;
        public EuroopaPage()
        {
            Title = "Euroopa page";
            selectedRiik = riigid[0];
            listView = new ListView
            {
                ItemsSource = riigid,
                Footer = DateTime.Now.ToString("t"),
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell ic = new ImageCell { TextColor = Color.White, DetailColor = Color.Green };
                    ic.SetBinding(ImageCell.TextProperty, "Nimi");
                    Binding companyBinding = new Binding { Path = "Pealinn", StringFormat = "Pealinn - {0}" };
                    ic.SetBinding(ImageCell.DetailProperty, companyBinding);
                    ic.SetBinding(ImageCell.ImageSourceProperty, "Lipp");
                    return ic;
                })
            };
            lbl = new Label
            {
                Text = "Euroopa riikide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            listView.ItemTapped+=ListView_ItemTapped;
            Lisa = new Button { Text = "Lisa riik" };
            Lisa.Clicked+=Lisa_Clicked;
            Kustuta = new Button { Text = "Kustuta riik" };
            Kustuta.Clicked+=Kustuta_Clicked;
            this.Content = new StackLayout { Children = { lbl, listView, Lisa, Kustuta } };
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            selectedRiik = e.Item as Euroopa;
            lbl.Text = selectedRiik.Nimi;
            await DisplayAlert(selectedRiik.Nimi, $"Pealinn - {selectedRiik.Pealinn}, Rahvaarv - {selectedRiik.Rahvaarv}","Tühista");
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            riigid.Remove(selectedRiik);
            lbl.Text = "Euroopa riikide loetelu";
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {
            string nimi = await DisplayPromptAsync("Nimi", "Kirjuta nimi");
            if (nimi == null)
                return;
            string pealinn = await DisplayPromptAsync("Pealinn", "Kirjuta pealinn");
            if (pealinn == null)
                return;
            string rahvaarv = await DisplayPromptAsync("Rahvaarv", "Kirjuta rahvaarv", keyboard: Keyboard.Numeric);
            if (rahvaarv == null)
                return;
            Euroopa eur = new Euroopa(nimi, pealinn, int.Parse(rahvaarv));
            if (riigid.Any(x => x.Nimi == eur.Nimi))
                return;
            riigid.Add(eur);
        }
    }
}