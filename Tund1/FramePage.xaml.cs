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
    public partial class FramePage : ContentPage
    {
        Grid grid;
        Random random;
        Label lbl;
        Frame fr;
        Switch sw;
        Image image;
        public FramePage()
        {
            Title = "Frame page";
            random = new Random();
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped +=Tap_Tapped;
            grid = new Grid
            {
                HorizontalOptions= LayoutOptions.FillAndExpand,
                VerticalOptions= LayoutOptions.FillAndExpand,
            };
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    grid.Children.Add(
                    fr = new Frame { BackgroundColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)) },i,j
                    );
                    fr.GestureRecognizers.Add(tap);
                }
            }
            lbl = new Label { Text = "Tekst", FontSize = Device.GetNamedSize(NamedSize.Subtitle,typeof(Label)) };
            grid.Children.Add(lbl,1,2);
            image = new Image { Source = "cat.jpg" };
            sw = new Switch { IsToggled = true };
            sw.Toggled+=Sw_Toggled;
            grid.Children.Add(image,0,0);
            grid.Children.Add(sw, 1, 0);
            Grid.SetColumnSpan(grid, 6);

            Content = grid;
        }

        private void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            image.IsVisible = sw.IsToggled;
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            Frame fr = sender as Frame;
            int r = Grid.GetRow(fr)+1;
            int c = Grid.GetColumn(fr)+1;
            lbl.Text = "Rida: "+r+"\n Veerg: "+c;
        }
    }
}