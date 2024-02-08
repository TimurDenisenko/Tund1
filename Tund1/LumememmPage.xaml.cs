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
    public partial class LumememmPage : ContentPage
    {
        BoxView bucket, head, body, footer;
        Button isVisible, randomColor, hotSnowman;
        public LumememmPage()
        {
            Title = "Lumememm Leht";
            bucket = new BoxView
            {
                Margin = 20,
                BackgroundColor = Color.LightGray,
                WidthRequest = 100,
                HeightRequest = 90,
                HorizontalOptions= LayoutOptions.Center,
            };
            head = new BoxView
            {
                Margin = -50,
                BackgroundColor = Color.White,
                WidthRequest = 100,
                HeightRequest = 100,
                HorizontalOptions= LayoutOptions.Center,
                CornerRadius = 360,
            };
            body = new BoxView
            {
                BackgroundColor = Color.White,
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions= LayoutOptions.Center,
                CornerRadius = 360,
            };
            footer = new BoxView
            {
                Margin = -50,
                BackgroundColor = Color.White,
                WidthRequest = 200,
                HeightRequest = 200,
                HorizontalOptions= LayoutOptions.Center,
                CornerRadius = 360,
            };
            isVisible = new Button
            {
                BackgroundColor = Color.Blue,
                WidthRequest= 100,
                HeightRequest= 50,
                Text = "Visible",
            };
            isVisible.Clicked+=IsVisible_Clicked;
            randomColor = new Button
            {
                BackgroundColor = Color.White,
                WidthRequest= 100,
                HeightRequest= 50,
                Text = "Random color",
            };
            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children= { bucket,head, body, footer},
            };
            StackLayout st1 = new StackLayout
            {
                Margin = 50,
                Orientation = StackOrientation.Horizontal,
                Children= { isVisible,randomColor},
            };
            StackLayout st2 = new StackLayout
            {
                Margin = 50,
                Orientation = StackOrientation.Horizontal,
                Children= {  },
            };
            Content = new StackLayout { Children = {st,st1,st2} };
        }

        private void IsVisible_Clicked(object sender, EventArgs e)
        {
            switch (bucket.Opacity)
            {
                case 0:
                    isVisible.BackgroundColor = Color.Blue;
                    isVisible.Text = "Visible";
                    foreach (BoxView item in new BoxView[] {bucket,head,body,footer})
                    {
                        item.Opacity = 1;
                    }
                    break;
                case 1:
                    isVisible.BackgroundColor = Color.Red;
                    isVisible.Text = "Invisible";
                    foreach (BoxView item in new BoxView[] { bucket, head, body, footer })
                    {
                        item.Opacity = 0;
                    }
                    break;
            }
        }
    }
}