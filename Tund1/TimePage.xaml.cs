using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePage : ContentPage
    {
        bool t = false;
        int r = 0;
        int g = 25;
        int b = 50; 
        public TimePage()
        {
            InitializeComponent();
            Title = "Time Leht";
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            BackgroundColor = Color.FromRgb(new Random().Next(256), new Random().Next(256), new Random().Next(256));
            lbl_click.Text= "Vajutajad";
        }

        private async void Timer()
        {
            while (t)
            {
                btn_Timer.Text = int.TryParse(btn_Timer.Text, out int result) ? (result +1).ToString() : "0";
                if (r==255 && g==255 && b==255)
                {
                    r=new Random().Next(256);
                    g=new Random().Next(256);
                    b=new Random().Next(256);
                }
                BackgroundColor = Color.FromRgb(r==255 ? r : ++r, b==255 ? b : ++b, g==255 ? g : ++g);
                await Task.Delay(10);
            }
            while (!t)
            {
                btn_Timer.Text = DateTime.Now.ToString();
                await Task.Delay(1000);
            }
        }
        private void Timer_Clicked(object sender, EventArgs e)
        {
            t=!t;
            Timer();
        }
    }
}