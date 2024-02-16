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
    public partial class TripsTrapsTrull : ContentPage
    {
        Grid grid;
        Frame fr;
        bool x = true, withBot = true;
        int[,] xo;
        Button newGame;
        Button changeBackground;
        Button changePlayer1;
        Button changePlayer2;
        Button gameMode;
        TapGestureRecognizer tap;
        ColorTypeConverter converter = new ColorTypeConverter();
        Color p1 = Color.Red, p2 = Color.Blue;
        public TripsTrapsTrull()
        {
            Title = "Trips Traps Trull Page";
            grid = new Grid
            {
                WidthRequest = 200,
                HeightRequest = 200,
            };
            tap = new TapGestureRecognizer();
            tap.Tapped+=Tap_Tapped;
            newGame = new Button
            {
                WidthRequest = 100,
                HeightRequest = 50,
                Text = "New game",
            };
            newGame.Clicked += NewGame_Clicked;
            changeBackground = new Button
            {
                WidthRequest = 100,
                HeightRequest = 50,
                Text = "Change background color",
            };
            changeBackground.Clicked += ChangeBackground_Clicked;
            changePlayer1 = new Button
            {
                WidthRequest = 100,
                HeightRequest = 50,
                Text = "Change X color",
            };
            changePlayer1.Clicked += ChangePlayer1_Clicked;
            changePlayer2 = new Button
            {
                WidthRequest = 100,
                HeightRequest = 50,
                Text = "Change O color",
            };
            changePlayer2.Clicked += ChangePlayer2_Clicked;
            grid.Children.Add(new Frame { BackgroundColor = Color.Transparent }, 0, 6);
            grid.Children.Add(newGame, 0, 3);
            grid.Children.Add(changeBackground, 0, 4);
            grid.Children.Add(changePlayer1, 1, 4);
            grid.Children.Add(changePlayer2, 2, 4);
            StartGame();
            Content = grid;
        }

        private void ChangePlayer2_Clicked(object sender, EventArgs e)
        {
            ChangeColor("O", p2, p1);
        }

        private void ChangePlayer1_Clicked(object sender, EventArgs e)
        {
            ChangeColor("X", p1, p2);
        }

        private async void ChangeColor(string name, Color primary, Color secondary)
        {
            try
            {
                string result = await DisplayPromptAsync("Change "+name+"color", "Color");
                if ((Color)converter.ConvertFromInvariantString(result) == secondary)
                {
                    Color temp = primary;
                    primary = secondary;
                    secondary = temp;
                }
                else
                    primary = (Color)converter.ConvertFromInvariantString(result);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void StartGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid.Children.Add(
                    fr = new Frame { BackgroundColor = Color.White }, i, j
                    );
                    fr.GestureRecognizers.Add(tap);
                }
            }
            xo = new int[3, 3];
        }

        private async void ChangeBackground_Clicked(object sender, EventArgs e)
        {
            try
            {
                string result = await DisplayPromptAsync("Change background color", "Color");
                ColorTypeConverter converter = new ColorTypeConverter();
                BackgroundColor = (Color)converter.ConvertFromInvariantString(result);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void NewGame_Clicked(object sender, EventArgs e)
        {
            EndGame(0);
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            Frame fr = sender as Frame;
            if (fr.BackgroundColor!=Color.White)
                return;
            int r = Grid.GetRow(fr);
            int c = Grid.GetColumn(fr);
            switch (x)
            {
                case true:
                    fr.BackgroundColor = Color.Red;
                    xo[r, c] = 1;
                    CheckWin(3);
                    x = !x;
                    if (withBot)
                    {
                        BotGame();
                    }
                    return;
                case false:
                    fr.BackgroundColor = Color.Blue;
                    xo[r, c] = 4;
                    CheckWin(12);
                    x = !x;
                    return;
            }
        }

        private void BotGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (xo[i,j]==0)
                    {
                        grid.Children.Add(new Frame() { BackgroundColor = Color.Blue}, i,j);
                    }
                }
            }
            CheckWin(12);
            x = !x;
        }

        private void CheckWin(int res)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((xo[i,0]+xo[i,1]+xo[i,2])==res)
                {
                    EndGame(res);
                    return;
                }
                if ((xo[0, i]+xo[1, i]+xo[2, i])==res)
                {
                    EndGame(res);
                    return;
                }
            }
            if ((xo[0, 0]+xo[1, 1]+xo[2, 2])==res)
            {
                EndGame(res);
                return;
            }
            if ((xo[0,2]+xo[1, 1]+xo[2,0])==res)
            {
                EndGame(res);
                return;
            }
        }
        private async void EndGame(int res)
        {
            switch (res)
            {
                case 3:
                    await DisplayAlert("End", p1+" win", "OK");
                    break;
                case 12:
                    await DisplayAlert("End", p2+" win", "OK");
                    break;
            }
            StartGame();
        }
    }
}