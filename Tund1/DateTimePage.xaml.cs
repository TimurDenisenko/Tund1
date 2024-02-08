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
    public partial class DateTimePage : ContentPage
    {
        DatePicker dp;
        TimePicker tp;
        Label lbl;
        public DateTimePage()
        {
            Title = "DateTime leht";
            dp = new DatePicker
            {
                Format = "D",
                MinimumDate = DateTime.Now.AddDays(-10),
                MaximumDate = DateTime.Now.AddDays(10),
                TextColor= Color.Red,
            };
            dp.DateSelected+=Dp_DateSelected; ;
            tp = new TimePicker
            {
                Format = "",
                Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute,DateTime.Now.Second),
            };
            tp.PropertyChanged+=Tp_PropertyChanged;
            lbl = new Label
            {
                BackgroundColor = Color.Orange,
            };
            AbsoluteLayout al = new AbsoluteLayout
            {
                Children= { dp, tp, lbl }
            };
            AbsoluteLayout.SetLayoutBounds(dp, new Rectangle(100,100, 200, 50));
            AbsoluteLayout.SetLayoutBounds(tp, new Rectangle(100, 200, 200, 50));
            AbsoluteLayout.SetLayoutBounds(lbl, new Rectangle(100, 300, 200, 50));
            Content = al;   
        }

        private void Dp_DateSelected(object sender, DateChangedEventArgs e)
        {
            lbl.Text = e.NewDate.ToString("D");
        }

        private void Tp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            lbl.Text = "Aeg: "+tp.Time.ToString();
        }
    }
}