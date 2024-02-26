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
        bool x = true, withBot = false;
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
                Text = "Uus mäng",
            };
            newGame.Clicked += NewGame_Clicked;
            changeBackground = new Button
            {
                WidthRequest = 100,
                HeightRequest = 50,
                Text = "Muutke taustavärvi",
            };
            changeBackground.Clicked += ChangeBackground_Clicked;
            changePlayer1 = new Button
            {
                WidthRequest = 100,
                HeightRequest = 50,
                Text = "Muuda X värvi",
            };
            changePlayer1.Clicked += ChangePlayer1_Clicked;
            changePlayer2 = new Button
            {
                WidthRequest = 100,
                HeightRequest = 50,
                Text = "Muuda O värvi",
            };
            changePlayer2.Clicked += ChangePlayer2_Clicked;
            gameMode = new Button
            {
                WidthRequest = 100,
                HeightRequest = 50,
                Text = "Muuda mängurežiimi",
            };
            gameMode.Clicked+=GameMode_Clicked;
            grid.Children.Add(new Frame { BackgroundColor = Color.Transparent }, 0, 6);
            grid.Children.Add(newGame, 0, 10);
            grid.Children.Add(gameMode, 1, 10);
            grid.Children.Add(changeBackground, 0, 11);
            grid.Children.Add(changePlayer1, 1, 11);
            grid.Children.Add(changePlayer2, 2, 11);
            StartGame();
            Content = grid;
        }

        private async void GameMode_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Mängurežiimi", "Tühista", null, "Vaikimisi", "Bot");
            switch (action)
            {
                case "Vaikimisi":
                    withBot = false;
                    break;
                case "Bot":
                    withBot = true;
                    break;
            }
            StartGame();
        }

        private void ChangePlayer2_Clicked(object sender, EventArgs e)
        {
            ChangeColor2();
            StartGame();
        }

        private void ChangePlayer1_Clicked(object sender, EventArgs e)
        {
            ChangeColor1();
            StartGame();
        }

        private async void ChangeColor1()
        {
            try
            {
                string result = await DisplayPromptAsync("Muuda X värvi", "Värv");
                if ((Color)converter.ConvertFromInvariantString(result) == p2)
                {
                    Color temp = p1;
                    p1 = p2;
                    p2 = temp;
                }
                else
                {
                    if ((Color)converter.ConvertFromInvariantString(result)==Color.White)
                    {
                        return;
                    }
                    p1 = (Color)converter.ConvertFromInvariantString(result);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private async void ChangeColor2()
        {
            try
            {
                string result = await DisplayPromptAsync("Muuda O värvi", "Värv");
                if ((Color)converter.ConvertFromInvariantString(result) == p1)
                {
                    Color temp = p2;
                    p2 = p1;
                    p1 = temp;
                }
                else
                {
                    if ((Color)converter.ConvertFromInvariantString(result)==Color.White)
                    {
                        return;
                    }
                    p2 = (Color)converter.ConvertFromInvariantString(result);
                }
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
            xo = new int[3,3];
        }

        private async void ChangeBackground_Clicked(object sender, EventArgs e)
        {
            try
            {
                string result = await DisplayPromptAsync("Muutke taustavärvi", "Värv");
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
                    fr.BackgroundColor = p1;
                    xo[r, c] = 1;
                    x = !x;
                    if (withBot)
                    {
                        BotGame();
                    }
                    break;
                case false:
                    fr.BackgroundColor = p2;
                    xo[r, c] = 10;
                    x = !x;
                    break;
            }
        }

        private void BotGame()
        {
            for (int i = 0; i < 3; i++)
            {
                if ((xo[i, 0]+xo[i, 1]+xo[i, 2])==2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (xo[i, j]==0)
                        {
                            grid.Children.Add(new Frame() { BackgroundColor = p2 }, j, i);
                            x = !x;
                            xo[i, j] = 4;
                            CheckWin(12);
                            return;
                        }
                    }
                }
                if ((xo[0, i]+xo[1, i]+xo[2, i])==2)
                {
                    for (int o = 0; o < 3; o++)
                    {
                        if (xo[o,i]==0)
                        {
                            grid.Children.Add(new Frame() { BackgroundColor = p2 }, i,o);
                            x = !x;
                            xo[o,i] = 4;
                            CheckWin(12);
                            return;
                        }
                    }
                }
            }
            if ((xo[0, 0]+xo[1, 1]+xo[2, 2])==2)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (xo[j, j]==0)
                    {
                        grid.Children.Add(new Frame() { BackgroundColor = p2 }, j, j);
                        x = !x;
                        xo[j, j] = 4;
                        CheckWin(12);
                        return;
                    }
                }
            }
            if ((xo[0, 2]+xo[1, 1]+xo[2, 0])==2)
            {
                for (int o = 2; o > 1; o--)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (xo[j, o]==0)
                        {
                            grid.Children.Add(new Frame() { BackgroundColor = p2 }, o, j);
                            x = !x;
                            xo[j, o] = 4;
                            CheckWin(12);
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < 9; i++)
            {
                int rand1 = new Random().Next(3);
                int rand2 = new Random().Next(3); 
                if (xo[rand1,rand2]==0)
                {
                    grid.Children.Add(new Frame() { BackgroundColor = p2 }, rand2, rand1);
                    x = !x;
                    xo[rand1,rand2] = 4;
                    CheckWin(12);
                    return;
                }
            }
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
            if ((xo[0,0]+xo[1, 1]+xo[2, 2])==res)
            {
                EndGame(res);
                return;
            }
            if ((xo[0,2]+xo[1, 1]+xo[2,0])==res)
            {
                EndGame(res);
                return;
            }
            if (xo[0,0]!=0 && xo[0, 1]!=0 && xo[0, 2]!=0 && xo[1, 0]!=0 && xo[1, 1]!=0 && xo[1, 2]!=0
                &&xo[2, 0]!=0&&xo[2, 1]!=0&&xo[2, 2]!=0)
            {
                EndGame(1);
                return; 
            }
        }
        private async void EndGame(int res)
        {
            switch (res)
            {
                case 1:
                    await DisplayAlert("Lõpp", "Võitjaid pole", "OK");
                    break;
                case 3:
                    await DisplayAlert("Lõpp", "P1 võit", "OK");
                    break;
                case 12:
                    await DisplayAlert("Lõpp", "P1 võit", "OK");
                    break;
            }
            StartGame();
        }
    }
}