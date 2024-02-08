using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {
        Button btn_Start,btn_Time,btn_BV;
        Label lbl;
        StackLayout st;
        Editor ed;
        public EntryPage()
        {
            Title = "Entry Leht";
            #region Элементы
            btn_Start = new Button
            {
                Text = "Start leht",
            };
            btn_Start.Clicked+=async(s, e) => await Navigation.PopAsync(true);

            btn_Time = new Button
            {
                Text = "Time leht",
            };
            btn_Time.Clicked+=async (s, e) => await Navigation.PushAsync(new TimePage());

            btn_BV = new Button
            {
                Text = "BoxView leht",
            };
            btn_BV.Clicked+=async (s, e) => await Navigation.PushAsync(new BoxViewPage());

            lbl = new Label {
                Text = "Mingi tekst",
                BackgroundColor = Color.Yellow,
                HorizontalTextAlignment= TextAlignment.Center,
                FontSize = 12,
                TextColor= Color.Black,
            };

            ed = new Editor {
                Placeholder = "Sisesta tekst..",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                TextColor= Color.Black,
                WidthRequest = 400,
                MaxLength = 50,
            };
            ed.TextChanged += (s, e) => { lbl.Text = ed.Text; } ;
            #endregion

            Content = st = new StackLayout {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.LightGray,
                Children = { lbl, btn_Start, btn_Time,btn_BV, ed },
            };
        }
    }
}