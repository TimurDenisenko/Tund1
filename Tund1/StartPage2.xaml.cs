﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage2 : ContentPage
    {
        StackLayout st;
        List<ContentPage> pages = new List<ContentPage>() { new EntryPage(), new BoxViewPage(), new TimePage(), new DateTimePage(), 
            new StepperSliderPage(), new ColorSlider(), new LumememmPage(), new FramePage(), new TripsTrapsTrull(), new PickerPage() };
        Button btn;
        public StartPage2()
        {
            st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.LightGray,
            };
            foreach (ContentPage item in pages)
            {
                btn = new Button
                {
                    Text = "Ava "+item.Title
                };
                btn.Clicked+=async (s, e) => await Navigation.PushAsync(item);
                st.Children.Add(btn);
            }
            Content = new ScrollView { Content = st };
        }
    }
}