using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace PinguinEducation.Alphabet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlphabetPage : ContentPage
    {
        AlphabetStorage strongbox = new AlphabetStorage();
        bool mute = false;
        public AlphabetPage()
        {
            InitializeComponent();

            List<string> tempList = new List<string>();
            tempList.Add(strongbox.ALPHABET_alphabet[0]);
            strongbox.ALPHABET_unlocked = tempList.ToArray();

            strongbox.goal = 100;

            CreateGrid();
        }

        public void CreateGrid()
        {
            grid.Children.Clear();

            int letter = 0;
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    try
                    {
                        Button btn = new Button
                        {
                            BorderColor = Color.Black,
                            BorderWidth = 2,
                            TextColor = Color.Black,
                            FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                            CornerRadius = 10,
                            Text = strongbox.ALPHABET_alphabet[letter],
                            Margin = 4
                        };
                        if (!strongbox.ALPHABET_unlocked.Contains(btn.Text))
                        {
                            btn.BackgroundColor = Color.Gray;
                            btn.Clicked += Btn_Clicked1;
                        }
                        else
                        {
                            btn.BackgroundColor = Color.LightGray;
                            btn.Clicked += Btn_Clicked;
                        }
                        grid.Children.Add(btn, x, y);
                    }
                    catch (System.Exception)
                    {
                    }
                    letter++;
                }
            }

            Button btnS = new Button
            {
                Text = "Mute",
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                BorderColor = Color.Black,
                BorderWidth = 2,
            };
            btnS.Clicked += Btn_Clicked2;
            grid.Children.Add(btnS, 3, 6);
        }

        private void Btn_Clicked2(object sender, System.EventArgs e)
        {
            mute = !mute;
            DependencyService.Get<IBridge>().MuteMusic(mute);
        }

        protected override void OnAppearing()
        {
            CreateGrid();
        }

        private async void Btn_Clicked1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PinguinEducation.Game.TileMatchingVideoGame(strongbox));
        }

        private async void Btn_Clicked(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;

            strongbox.ALPHABET_letter = btn.Text;

            Label lbl = new Label
            {
                Text = btn.Text,
                FontSize = Device.GetNamedSize(NamedSize.Header, typeof(Label)),
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center
            };

            Button tts = new Button
            {
                ImageSource = "play.png",
                BackgroundColor = Color.Wheat,
                WidthRequest = 100,
                HeightRequest = 100
            };

            Button mut = new Button
            {
                Text = "Mute",
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                BorderColor = Color.Black,
                BorderWidth = 2,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.End
            };
            mut.Clicked += Btn_Clicked2;

            tts.Clicked += Tts_Clicked;

            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 0,
            };

            StackLayout st = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 0,
            };

            stack.Children.Add(lbl);
            stack.Children.Add(tts);

            Frame frm = new Frame
            {
                Content = stack,
                CornerRadius = 10,
                BorderColor = Color.Black,
                BackgroundColor = Color.Wheat,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(100, 200, 100, 200),
            };

            st.Children.Add(frm);
            st.Children.Add(mut);

            await Navigation.PushAsync(new ContentPage
            {
                Content = st,
                BackgroundColor = Color.FromHex("#85AAC5")
            });
        }

        private async void Tts_Clicked(object sender, System.EventArgs e)
        {
            var locales = await Xamarin.Essentials.TextToSpeech.GetLocalesAsync();

            var locale = locales.FirstOrDefault();

            await Xamarin.Essentials.TextToSpeech.SpeakAsync(strongbox.ALPHABET_letter, new Xamarin.Essentials.SpeechOptions()
            {
                Volume = 1,
                Pitch = 1,
                Locale = locale
            });
        }
    }
}