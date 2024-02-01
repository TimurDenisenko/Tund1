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
    public partial class BoxViewPage : ContentPage
    {
        BoxView box;
        Label lbl;
        int r = 0, g = 0, b = 0, rx=0;
        bool t = false;
        public BoxViewPage()
        {
            box = new BoxView { 
                Color= Color.FromRgb(r,g,b),
                CornerRadius = 10,
                WidthRequest= 200,
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions= LayoutOptions.CenterAndExpand,
            };
            lbl = new Label{
                Text = "Mingi tekst",
                BackgroundColor = Color.White,
                HorizontalTextAlignment= TextAlignment.Center,
                FontSize = 12,
                TextColor= Color.Black,
                VerticalOptions= LayoutOptions.Start,
            };
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped +=Tap_Tapped;
            TapGestureRecognizer tap1 = new TapGestureRecognizer();
            tap1.NumberOfTapsRequired= 2;
            tap1.Tapped+=Tap1_Tapped;
            box.GestureRecognizers.Add(tap);
            box.GestureRecognizers.Add(tap1);

            Content = new StackLayout { Children = {box,lbl} };
        }

        private void Tap1_Tapped(object sender, EventArgs e)
        {
            t=!t;
            ARotate();
        }

        private async void ARotate()
        {
            while (!t)
            {
                rx -= 10;
                await box.RotateXTo(rx);
                await box.RotateYTo(rx);
            }
        }
        private async void Rotate()
        {
            while(t)
            {
                rx += 10;
                await box.RotateXTo(rx);
                await box.RotateYTo(rx);
            }
        }
        private void Tap_Tapped(object sender, EventArgs e)
        {
            box.Color = Color.FromRgb(new Random().Next(256), new Random().Next(256), new Random().Next(256));
            t=!t;
            lbl.Text = int.TryParse(lbl.Text, out int result) ? (++result).ToString() : "1";
            Rotate();
        }
    }
}