using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LumememmPage : ContentPage
    {
        BoxView bucket, head, body, footer, changebox;
        Button isVisible, randomColor, hotSnowman,reset;
        Slider sr, sg, sb;
        Label r, g, b;
        public LumememmPage()
        {
            
            Title = "Lumememm Leht";
            bucket = new BoxView
            {
                BackgroundColor = Color.LightGray,
                WidthRequest = 100,
                HeightRequest = 90,
                HorizontalOptions= LayoutOptions.Center,
            };
            head = new BoxView
            {
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
                BackgroundColor = Color.Black,
                WidthRequest= 100,
                HeightRequest= 50,
                Text = "Random color",
            };
            randomColor.Clicked+=RandomColor_Clicked;
            hotSnowman = new Button
            {
                BackgroundColor = Color.Red,
                WidthRequest= 100,
                HeightRequest= 50,
                Text = "Melt the snowman",
            };
            hotSnowman.Clicked+=HotSnowman_Clicked;
            reset = new Button
            {
                WidthRequest= 100,
                HeightRequest= 50,
                Text = "Reset",
            };
            reset.Clicked+=Reset_Clicked;

            sr = new Slider(); sg = new Slider(); sb= new Slider();
            foreach (var item in new Slider[] { sr, sg, sb })
            {
                item.Minimum= 0;
                item.Maximum= 255;
                item.Value= 30;
                item.MinimumTrackColor= Color.White;
                item.MaximumTrackColor= Color.Black;
                item.ThumbColor= Color.Red;
                item.ValueChanged+=Item_ValueChanged;
            }
            r = new Label { Text = "Red"}; g = new Label { Text = "Green"}; b = new Label { Text = "Blue" };
            changebox = bucket;
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped+=Tap_Tapped;
            foreach (BoxView item in new BoxView[] {bucket,head,body,footer})
            {
                item.GestureRecognizers.Add(tap);
            }
            AbsoluteLayout al = new AbsoluteLayout
            {
                Children= { head, body, footer, bucket, isVisible, randomColor, hotSnowman,reset,sr,sg,sb,r,g,b }
            };
            AbsoluteLayout.SetLayoutBounds(bucket, new Rectangle(150, 150, bucket.Width,bucket.Height));
            AbsoluteLayout.SetLayoutBounds(head, new Rectangle(150, 225, head.Width, head.Height));
            AbsoluteLayout.SetLayoutBounds(body, new Rectangle(125, 300, body.Width, body.Height));
            AbsoluteLayout.SetLayoutBounds(footer, new Rectangle(100, 400, footer.Width, footer.Height));
            AbsoluteLayout.SetLayoutBounds(isVisible, new Rectangle(0, 650, isVisible.Width, isVisible.Height));
            AbsoluteLayout.SetLayoutBounds(randomColor, new Rectangle(100, 650, randomColor.Width, randomColor.Height));
            AbsoluteLayout.SetLayoutBounds(hotSnowman, new Rectangle(200, 650, hotSnowman.Width, hotSnowman.Height));
            AbsoluteLayout.SetLayoutBounds(reset, new Rectangle(300, 650, reset.Width, reset.Height));
            AbsoluteLayout.SetLayoutBounds(sr, new Rectangle(20, 0, 380, sr.Height));
            AbsoluteLayout.SetLayoutBounds(sg, new Rectangle(20, 20, 380, sg.Height));
            AbsoluteLayout.SetLayoutBounds(sb, new Rectangle(20, 40, 380, sb.Height));
            AbsoluteLayout.SetLayoutBounds(r, new Rectangle(0, 0, 40, r.Height));
            AbsoluteLayout.SetLayoutBounds(g, new Rectangle(0, 20, 40, g.Height));
            AbsoluteLayout.SetLayoutBounds(b, new Rectangle(0, 40, 40, b.Height));
            Content = al;
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            changebox = sender as BoxView;
        }

        private void Item_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            changebox.Color = Color.FromRgb((int)sr.Value, (int)sg.Value, (int)sb.Value);
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            foreach (BoxView item in new BoxView[] { head, body, footer })
            {
                item.Opacity = 1;
                item.BackgroundColor = Color.White;
            }
            AbsoluteLayout.SetLayoutBounds(bucket, new Rectangle(150, 150, bucket.Width, bucket.Height));
        }

        private async void HotSnowman_Clicked(object sender, EventArgs e)
        {
            for (double i = 1; i > 0.01; i-=0.01)
            {
                head.Opacity = i;
                body.Opacity = i;
                footer.Opacity = i;
                await Task.Delay(10);
            }
            head.Opacity = 0;
            body.Opacity = 0;
            footer.Opacity = 0;
            for (int i = 150; i < 500; i+=5)
            {
                AbsoluteLayout.SetLayoutBounds(bucket, new Rectangle(150, i, bucket.Width, bucket.Height));
                await Task.Delay(1);
            }
        }

        private void RandomColor_Clicked(object sender, EventArgs e)
        {
            foreach (var item in new Slider[] { sr, sg, sb })
            {
                item.ValueChanged-=Item_ValueChanged;
            }
            head.BackgroundColor = Color.FromRgb(new Random().Next(256),new Random().Next(256), new Random().Next(256));
            body.BackgroundColor = Color.FromRgb(new Random().Next(256), new Random().Next(256), new Random().Next(256));
            footer.BackgroundColor = Color.FromRgb(new Random().Next(256), new Random().Next(256), new Random().Next(256));
            foreach (var item in new Slider[] { sr, sg, sb })
            {
                item.ValueChanged+=Item_ValueChanged;
            }
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