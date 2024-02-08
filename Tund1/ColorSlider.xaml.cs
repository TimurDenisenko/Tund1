using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorSlider : ContentPage
    {
        Label r, g, b;
        Slider sr, sg, sb;
        BoxView bv;
        Stepper stp;
        public ColorSlider()
        {

            Title = "ColorSlider leht";
            r = new Label(); g = new Label(); b = new Label();
            foreach (var item in new Label[] {r,g,b})
            {
                item.Text="...";
                item.HorizontalOptions= LayoutOptions.Center;
                item.VerticalOptions= LayoutOptions.CenterAndExpand;
            }
            sr = new Slider(); sg = new Slider(); sb= new Slider();
            foreach (var item in new Slider[] { sr,sg,sb})
            {
                item.Minimum= 0;
                item.Maximum= 255;
                item.Value= 30;
                item.MinimumTrackColor= Color.White;
                item.MaximumTrackColor= Color.Black;
                item.ThumbColor= Color.Red;
            }
            sr.ValueChanged+=ValueChanged; 
            sg.ValueChanged+=ValueChanged;
            sb.ValueChanged+=ValueChanged;
            bv= new BoxView
            {
                WidthRequest = 300,
                HeightRequest= 400,
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions= LayoutOptions.CenterAndExpand,
            };
            stp = new Stepper
            {
                Minimum = 0,
                Maximum = 255,
                Increment = 10,
                Value= 255,
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions= LayoutOptions.CenterAndExpand,
            };
            stp.ValueChanged+=ValueChanged;
            Content = new StackLayout
            {
                Children= { bv,sr,r,sg,g,sb,b,stp }
            };
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped +=Tap_Tapped;
            bv.GestureRecognizers.Add(tap);
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            ToColor(sr,r, new Random().Next(256), "Red");
            ToColor(sg, g, new Random().Next(256), "Green");
            ToColor(sb, b, new Random().Next(256), "Blue");
        }

        private void ToColor(Slider slider, Label label, int ToColor, string color)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (slider.Value > ToColor)
                {
                    for (; slider.Value > ToColor; slider.Value--)
                    {
                        label.Text = string.Format("{1} = {0}", (int)slider.Value, color);
                        await Task.Delay(10);
                    }
                }
                else
                {
                    for (; slider.Value < ToColor; slider.Value++)
                    {
                        label.Text = string.Format("{1} = {0}", (int)slider.Value, color);
                        await Task.Delay(10);
                    }
                }
            });
        }


        private void ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender==sr)
            {
                r.Text = string.Format("Red = {0}", (int)e.NewValue);
            }
            else if (sender==sg)
            {
                g.Text = string.Format("Green = {0}", (int)e.NewValue);
            }
            else if (sender==sb)
            {
                b.Text = string.Format("Blue = {0}", (int)e.NewValue);
            }
            bv.Color = Color.FromRgba((int)sr.Value, (int)sg.Value, (int)sb.Value, (int)stp.Value);
        }
    }
}