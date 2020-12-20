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
        public TileMatchingVideoGame()
        {
            InitializeComponent();

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
        }

        private void DrawTable()
        {
            grid.Children.Clear();
            for (int x = 0; x < cells_amount; x++)
            {
                for (int y = 0; y < cells_amount; y++)
                {
                    grid.Children.Add(buttons[x, y], x, y);
                }
            }
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (fTurn)
            {
                fTurn = false;

                Point sPosition = new Point(Grid.GetColumn(btn), Grid.GetRow(btn));

                Button sBtn = buttons[(int)fPosition.X, (int)fPosition.Y];
                buttons[(int)fPosition.X, (int)fPosition.Y] = buttons[(int)sPosition.X, (int)sPosition.Y];
                buttons[(int)sPosition.X, (int)sPosition.Y] = sBtn;

                DrawTable();

                fPosition = new Point(170173, 170173);
            }
            else
            {
                fTurn = true;
                fPosition = new Point(Grid.GetColumn(btn), Grid.GetRow(btn));
            }

            //int value = 100;
            //IncrementScoreByParams(value);
        }

        private async void IncrementScoreByParams(int value)
        {
            scoreLbl.Text = "Score: " + strongbox.score.ToString();
            lbl.Text = "    +";
            await Task.Delay(250);
            lbl.Text += value;
            strongbox.score += value;
            await Task.Delay(750);
            lbl.Text = "";
            scoreLbl.Text = "Score: " + strongbox.score.ToString();
        }
    }
}