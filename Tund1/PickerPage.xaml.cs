using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPage : ContentPage
    {
        Picker picker;
        WebView webView;
        SwipeGestureRecognizer swipe,swipe1;
        Frame frame;
        string[] lehed = new string[] { "https://github.com/", "https://moodle.edu.ee/", "https://www.youtube.com/" };
        public PickerPage()
        {
            Title = "Picker Page";
            picker = new Picker
            {
                Title = "Veebilehed",
                Items = { "github", "moodle", "youtube" },
            };
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url="https://et.wikipedia.org/wiki/A" },
                WidthRequest = 200,
                HeightRequest = 700,
            };
            swipe = new SwipeGestureRecognizer { Direction = SwipeDirection.Right};
            swipe1 = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            swipe.Swiped += (sender, e) =>
            {
                webView.GoBack();
                picker.Item
            };
            swipe1.Swiped += (sender, e) =>
            {
                webView.GoForward();
                picker.ItemDisplayBinding = new Binding("Name");
            };
            frame = new Frame
            {
                HeightRequest = 100,
                WidthRequest = 100,
                BackgroundColor = Color.Gray,
                GestureRecognizers = {swipe},
            };
            picker.SelectedIndexChanged+= (sender,e) => webView.Source = new UrlWebViewSource { Url = lehed[picker.SelectedIndex] };
            Content = new StackLayout
            {
                Children = { picker, webView,frame },
            };
        }
    }
}