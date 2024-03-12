using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        Label lbl;
        ListView listView;
        ObservableCollection<Telefon> telefons = new ObservableCollection<Telefon> {
            new Telefon("iPhone 7","Apple",500,Properties.Resources.tel), 
            new Telefon("Samsung Galaxy S8", "Android", 300,Properties.Resources.tel), 
            new Telefon("Huawei P10", "Android", 200,Properties.Resources.tel), 
            new Telefon("iPhone 17", "Apple", 800,Properties.Resources.tel) };
        Telefon selectedTelefon;
        Button Lisa, Kustuta;
        public ListPage()
        {
            Title = "List page";
            listView = new ListView
            {
                ItemsSource = telefons,
                Footer = DateTime.Now.ToString("t"),
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell ic = new ImageCell { TextColor = Color.White, DetailColor = Color.Green };
                    ic.SetBinding(ImageCell.TextProperty, "Nimetus");
                    Binding companyBinding = new Binding { Path = "Tootja", StringFormat = "Tore telefon firmalt {0}" };
                    ic.SetBinding(ImageCell.DetailProperty, companyBinding);
                    ic.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                    return ic;
                })
            };
            lbl = new Label
            {
                Text = "Telefonide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            listView.ItemTapped+=ListView_ItemTapped;
            Lisa = new Button { Text = "Lisa telefon" };
            Lisa.Clicked+=Lisa_Clicked;
            Kustuta = new Button { Text = "Kustuta telefon" };
            Kustuta.Clicked+=Kustuta_Clicked;
            this.Content = new StackLayout { Children = {lbl,listView, Lisa, Kustuta } };
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            telefons.Remove(selectedTelefon);
            lbl.Text = "Telefonide loetelu";
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {
            string nimetus = await DisplayPromptAsync("Nimetus", "Kirjuta nimetus");
            if (nimetus == null)
                return;
            string tootja = await DisplayPromptAsync("Tootja", "Kirjuta tootja");
            if (tootja == null)
                return;
            string hind = await DisplayPromptAsync("Hind", "Kirjuta hind", keyboard: Keyboard.Numeric);
            if (hind == null)
                return;
            Telefon tel = new Telefon(nimetus, tootja, int.Parse(hind), Properties.Resources.tel);
            if (telefons.Any(x => x.Nimetus == tel.Nimetus))
                return;
            telefons.Add(tel);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            selectedTelefon = e.Item as Telefon;
            lbl.Text = $"{selectedTelefon.Tootja} | {selectedTelefon.Nimetus} - {selectedTelefon.Hind} eurot";
        }
    }
}