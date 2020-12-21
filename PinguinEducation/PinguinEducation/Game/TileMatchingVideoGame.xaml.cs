using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PinguinEducation.Game
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TileMatchingVideoGame : ContentPage
    {
        TMVGStorage strongbox;
        Button[,] buttons = new Button[5,5];
        Point fPosition = new Point(170173, 170173);
        int cells_amount;
        bool fTurn = false;
        Alphabet.AlphabetStorage alphabetS;
        bool mute = false;
        public TileMatchingVideoGame(Alphabet.AlphabetStorage alphabetStorage)
        {
            InitializeComponent();

            alphabetS = alphabetStorage;

            strongbox = new TMVGStorage();
            strongbox.score = 0;

            strongbox.turn = 1;

            cells_amount = 5;
            for (int x = 0; x < cells_amount; x++)
            {
                for (int y = 0; y < cells_amount; y++)
                {
                    Button btn = new Button
                    {
                        BackgroundColor = Color.Wheat,
                        BorderColor = Color.Black,
                        BorderWidth = 2,
                        WidthRequest = 50,
                        HeightRequest = 50,
                        CornerRadius = 4,
                    };

                    btn.Opacity = 0.1;

                    buttons[x, y] = btn;

                    bool on = true;
                    Random rnd = new Random();
                    while (on)
                    {
                        btn.BackgroundColor = strongbox.color[rnd.Next(0, strongbox.color.Length)];

                        int currentY = y;
                        int currentX = x;

                        int leftY = currentY;
                        int leftX = currentX;

                        int topY = currentY;
                        int topX = currentX;

                        if (currentX != 0)
                        {
                            leftY = currentY;
                            leftX = Grid.GetColumn(buttons[currentX - 1, currentY]);
                        }

                        if (currentY != 0)
                        {
                            topY = Grid.GetRow(buttons[currentX, currentY - 1]);
                            topX = currentX;
                        }

                        if (buttons[currentX, currentY].BackgroundColor != buttons[leftX, leftY].BackgroundColor)
                        {
                            on = false;
                        }
                        if (buttons[currentX, currentY].BackgroundColor != buttons[topX, topY].BackgroundColor)
                        {
                            on = false;
                        }
                        if (currentX == leftX && leftX == topX && currentY == leftY && leftY == topY)
                        {
                            on = false;
                        }
                    }


                    btn.Clicked += Btn_Clicked;
                    grid.Children.Add(btn, x, y);
                }
            }
            OnStartAppearing();
        }

        private async void ObjectAppearing(Button btn)
        {
            for (double tick = 0.1; tick < 1.1; tick+=0.1)
            {
                btn.Opacity = tick;
                await Task.Delay(100);
            }
        }

        private async void OnStartAppearing()
        {
            foreach (var el in buttons)
            {
                await Task.Delay(30);
                el.Opacity = 0.5;
            }
            foreach (var el in buttons)
            {
                await Task.Delay(30);
                el.Opacity = 1;
            }
        }

        private void DrawTable()
        {
            bool check = false;
            int value = 0;

            grid.Children.Clear();
            for (int x = 0; x < cells_amount; x++)
            {
                for (int y = 0; y < cells_amount; y++)
                {
                    grid.Children.Add(buttons[x, y], x, y);
                }
            }

            for (int x = 0; x < cells_amount; x++)
            {
                for (int y = 0; y < cells_amount; y++)
                {
                    int currentY = y;
                    int currentX = x;

                    int leftY = 170173;
                    int leftX = 170173;

                    int topY = 170173;
                    int topX = 170173;

                    int rightY = 170173;
                    int rightX = 170173;

                    int bottomY = 170173;
                    int bottomX = 170173;

                    if (currentX != 0)
                    {
                        leftY = currentY;
                        leftX = Grid.GetColumn(buttons[currentX - 1, currentY]);
                    }

                    if (currentY != 0)
                    {
                        topY = Grid.GetRow(buttons[currentX, currentY - 1]);
                        topX = currentX;
                    }

                    if (currentX != (cells_amount - 1))
                    {
                        rightY = currentY;
                        rightX = Grid.GetColumn(buttons[currentX + 1, currentY]);
                    }

                    if (currentY != (cells_amount - 1))
                    {
                        bottomY = Grid.GetRow(buttons[currentX, currentY + 1]);
                        bottomX = currentX;
                    }

                    Random rnd = new Random();

                    try
                    {
                        if (buttons[currentX, currentY].BackgroundColor == buttons[leftX, leftY].BackgroundColor && buttons[leftX, leftY].BackgroundColor == buttons[rightX, rightY].BackgroundColor)
                        {
                            buttons[currentX, currentY].BackgroundColor = strongbox.color[rnd.Next(0, strongbox.color.Length)];
                            ObjectAppearing(buttons[currentX, currentY]);
                            buttons[leftX, leftY].BackgroundColor = strongbox.color[rnd.Next(0, strongbox.color.Length)];
                            ObjectAppearing(buttons[leftX, leftY]);
                            buttons[rightX, rightY].BackgroundColor = strongbox.color[rnd.Next(0, strongbox.color.Length)];
                            ObjectAppearing(buttons[rightX, rightY]);

                            value += 100;
                            check = true;
                        }

                        if (buttons[currentX, currentY].BackgroundColor == buttons[bottomX, bottomY].BackgroundColor && buttons[bottomX, bottomY].BackgroundColor == buttons[topX, topY].BackgroundColor)
                        {
                            buttons[currentX, currentY].BackgroundColor = strongbox.color[rnd.Next(0, strongbox.color.Length)];
                            ObjectAppearing(buttons[currentX, currentY]);
                            buttons[bottomX, bottomY].BackgroundColor = strongbox.color[rnd.Next(0, strongbox.color.Length)];
                            ObjectAppearing(buttons[bottomX, bottomY]);
                            buttons[topX, topY].BackgroundColor = strongbox.color[rnd.Next(0, strongbox.color.Length)];
                            ObjectAppearing(buttons[topX, topY]);

                            value += 100;
                            check = true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (check)
            {
                IncrementScoreByParams(value);
            }
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (fTurn)
            {
                fTurn = false;

                Point sPosition = new Point(Grid.GetColumn(btn), Grid.GetRow(btn));

                if (Math.Abs(sPosition.X - fPosition.X) == 1 || Math.Abs(sPosition.Y - fPosition.Y) == 1)
                {
                    Button sBtn = buttons[(int)fPosition.X, (int)fPosition.Y];
                    buttons[(int)fPosition.X, (int)fPosition.Y] = buttons[(int)sPosition.X, (int)sPosition.Y];
                    buttons[(int)sPosition.X, (int)sPosition.Y] = sBtn;

                    buttons[(int)sPosition.X, (int)sPosition.Y].Opacity = 1.0;
                }
                else
                {
                    buttons[(int)fPosition.X, (int)fPosition.Y].Opacity = 1.0;
                }

                DrawTable();

                fPosition = new Point(170173, 170173);
            }
            else
            {
                fTurn = true;
                fPosition = new Point(Grid.GetColumn(btn), Grid.GetRow(btn));
                buttons[(int)fPosition.X, (int)fPosition.Y].Opacity = 0.5;
            }
        }

        private async void IncrementScoreByParams(int value)
        {
            scoreLbl.Text = "Score: " + strongbox.score.ToString();
            lbl.Text = "    +";
            await Task.Delay(150);
            lbl.Text += value;
            strongbox.score += value;
            await Task.Delay(150);
            lbl.Text = "";
            scoreLbl.Text = "Score: " + strongbox.score.ToString();

            if (strongbox.score >= alphabetS.goal)
            {
                List<string> tempList = new List<string>();
                for (int tick = 0; tick < alphabetS.ALPHABET_unlocked.Length + 1; tick++)
                {
                    tempList.Add(alphabetS.ALPHABET_alphabet[tick]);
                }
                alphabetS.ALPHABET_unlocked = tempList.ToArray();
                alphabetS.goal = Math.Round(alphabetS.goal * 1.25);
                await Navigation.PopAsync();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            mute = !mute;
            DependencyService.Get<IBridge>().MuteMusic(mute);
        }
    }
}