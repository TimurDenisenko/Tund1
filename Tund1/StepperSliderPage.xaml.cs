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
    public partial class StepperSliderPage : ContentPage
    {
        Label lbl;
        Slider sld;
        Stepper stp;
        public StepperSliderPage()
        {
            Title = "StepperSlider leht";
            lbl = new Label
            {
                Text="...",
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions= LayoutOptions.CenterAndExpand,
            };
            sld = new Slider
            {
                Minimum= 0,
                Maximum= 100,
                Value= 30,
                MinimumTrackColor= Color.White,
                MaximumTrackColor= Color.Black,
                ThumbColor= Color.Red,
            };
            sld.ValueChanged+=ValueChanged;
            stp = new Stepper
            {
                Minimum= 0,
                Maximum = 100,
                Increment = 1,
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions= LayoutOptions.CenterAndExpand,
            };
            stp.ValueChanged+=ValueChanged;
            Content = new StackLayout
            { 
                Children= {sld,lbl,stp}
            };
        }

        private void ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lbl.Text= string.Format("Valitud {0:F1}", e.NewValue);
            lbl.FontSize = e.NewValue;
            lbl.Rotation = e.NewValue;
        }
    }
}