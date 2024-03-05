using Plugin.Messaging;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tund1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablePage : ContentPage
    {
        readonly TableView tableView;
        readonly SwitchCell sc;
        readonly ImageCell ic;
        readonly TableSection fotosection;
        readonly ImageButton helista, sms, email;
        public TablePage()
        {
            Title = "Table page";
            sc = new SwitchCell { Text = "Näita veel" };
            sc.OnChanged+=Sc_OnChanged;
            ic = new ImageCell
            {
                ImageSource =  ImageSource.FromStream(() => new MemoryStream(Properties.Resources.home)),
                Text = "Foto nimetus",
                Detail = "Foto kirjeldus",
            };
            fotosection = new TableSection();
            helista = new ImageButton
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Source =  ImageSource.FromStream(() => new MemoryStream(Properties.Resources.tel)),
                BackgroundColor = Color.Transparent,
                Padding = 25
            };
            helista.Clicked+=Helista_Clicked;
            sms = new ImageButton
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Source =  ImageSource.FromStream(() => new MemoryStream(Properties.Resources.sms)),
                BackgroundColor = Color.Transparent,
                Padding = 25
            };
            email = new ImageButton
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Source =  ImageSource.FromStream(() => new MemoryStream(Properties.Resources.email)),
                BackgroundColor = Color.Transparent,
                Padding = 25
            };
            tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("Andmete sisetamine")
                {
                    new TableSection("Põhiandmed:")
                    {
                        new EntryCell
                        {
                            Label="Nimi: ",
                            Placeholder="Sisesta oma sõbra nimi"
                        }
                    },
                    new TableSection("Kontaktandmed:")
                    {

                        new EntryCell
                        {
                            Label="Telefon: ",
                            Placeholder="Sisesta tel.number",
                            Keyboard = Keyboard.Telephone
                        },
                        new EntryCell
                        {
                            Label="Email: ",
                            Placeholder="Sisesta email",
                            Keyboard = Keyboard.Email
                        },
                        sc
                    },
                    fotosection,
                    new TableSection
                    {
                        new ViewCell
                        {
                            View = helista
                        },
                        new ViewCell
                        {
                            View = sms
                        },
                        new ViewCell
                        {
                            View = email
                        },
                    }
                }
            };
            Content = tableView;
        }

        private void Helista_Clicked(object sender, EventArgs e)
        {
            IPhoneCallTask phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(tableView.Root[1][0].ToString());
        }

        private void Sc_OnChanged(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                fotosection.Title = "Foto: ";
                fotosection.Add(ic);
                sc.Text = "Peida";
            }
            else
            {
                fotosection.Title = string.Empty;
                fotosection.Remove(ic);
                sc.Text = "Näita veel";
            }
        }
    }
}