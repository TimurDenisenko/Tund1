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
    public partial class StartPage : ContentPage
    {
        Button btn_Entry;
        StackLayout st;
        public StartPage()
        {
            #region Элементы
            btn_Entry = new Button {
                Text = "Entry leht",
                TextColor = Color.Black,
                BackgroundColor = Color.White,

            };
            btn_Entry.Clicked+=Btn_Entry_Clicked;
            #endregion

            Content = st = new StackLayout{
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.LightGray,
                Children = { btn_Entry }
            };
        }

        private async void Btn_Entry_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntryPage());
        }
    }
}