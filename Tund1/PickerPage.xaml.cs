using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPage : ContentPage
    {
        Picker picker;
        WebView webView;
        Entry search;
        Button home, back, forward, history;
        public PickerPage()
        {
            Title = "Picker Page";
            picker = new Picker
            {
                Title = "Ajalugu",
                HorizontalTextAlignment = TextAlignment.Center,
            };
            picker.SelectedIndexChanged+=Picker_SelectedIndexChanged;
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url="https://tthk.ee" },
                WidthRequest = 200,
                HeightRequest = 700,
            };
            search = new Entry
            {
                Text = "Otsing",
                TextColor = Color.Gray, 
                MaxLength = 20,
                WidthRequest = 200,
                FontSize = 16,
            };
            search.Focused += (sender, e) =>
            {
                search.Text = string.Empty;
                search.TextColor = Color.White;
            };
            search.Unfocused+=Search_Unfocused;
            Content = new StackLayout
            {
                Children = { picker,search, webView },
            };
        }

        private void Search_Unfocused(object sender, FocusEventArgs e)
        {
            if (search.Text == string.Empty)
                return;
            string[] list = search.Text.Split('.');
            if (list.Length==1 || list[1].Length < 1)
                return;
            try
            {
                webView.Source = new UrlWebViewSource { Url = "https://"+search.Text };
                picker.Items.Add(search.Text.Replace("https://", ""));
                search.TextColor = Color.Gray;
                search.Text = "Otsing";
            }
            catch (Exception)
            {
                return;
            }
        }


        //private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        //{
        //    picker.SelectedItem = e.Url;
        //}

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = picker.SelectedItem as string;
            if (!picker.Items.Contains(url))
                picker.Items.Add(url.Replace("https://",""));
            webView.Source = new UrlWebViewSource { Url = "https://"+url };
        }
    }
}