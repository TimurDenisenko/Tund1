using System;
using System.IO;
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
        ImageButton home, back, forward;
        StackLayout st;
        public PickerPage()
        {
            Title = "Picker Page";
            picker = new Picker
            {
                Title = "Ajalugu",
            };
            picker.SelectedIndexChanged+=Picker_SelectedIndexChanged;
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url="https://www.tthk.ee" },
                WidthRequest = 200,
                HeightRequest = 700,
            };
            webView.Navigated+=(sender,e)=> 
            {
                picker.Items.Add(e.Url.Replace("https://", ""));
                picker.SelectedItem = e.Url.Replace("https://", "");
                search.TextColor = Color.Gray;
                search.Text = "Otsing";
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
            home = new ImageButton
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Source =  ImageSource.FromStream(() => new MemoryStream(Properties.Resources.home)),
                BackgroundColor = Color.Transparent
            };
            home.Clicked += (sender,e) => 
            {
                webView.Source = new UrlWebViewSource { Url = "https://www.tthk.ee" };
            };
            back = new ImageButton
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Source =  ImageSource.FromStream(() => new MemoryStream(Properties.Resources.back)),
                BackgroundColor = Color.Transparent
            };
            back.Clicked += (sender, e) => webView.GoBack();
            forward = new ImageButton
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Source =  ImageSource.FromStream(() => new MemoryStream(Properties.Resources.forward)),
                BackgroundColor = Color.Transparent
            };
            forward.Clicked += (sender, e) => webView.GoForward();
            st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {home,back, forward},
            };
            Content = new StackLayout
            {
                Children = { picker,search,st, webView },
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
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = picker.SelectedItem as string;
            if (!picker.Items.Contains(url))
                picker.Items.Add(url.Replace("https://",""));
            webView.Source = new UrlWebViewSource { Url = "https://"+url };
        }
    }
}