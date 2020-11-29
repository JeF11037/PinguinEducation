using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PinguinEducation.Alphabet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlphabetPage : ContentPage
    {
        Strongbox strongbox = new Strongbox();
        public AlphabetPage()
        {
            InitializeComponent();

            int letter = 0;
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    try
                    {
                        Button btn = new Button
                        {
                            BackgroundColor = Color.Red,
                            BorderColor = Color.Black,
                            BorderWidth = 2,
                            TextColor = Color.Black,
                            FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                            CornerRadius = 10,
                            Text = strongbox.ALPHABET_alphabet[letter],
                        };
                        btn.Clicked += Btn_Clicked;
                        grid.Children.Add(btn, x, y);
                    }
                    catch (System.Exception)
                    {
                    }
                    letter++;
                }
            }
        }

        private async void Btn_Clicked(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;

            strongbox.ALPHABET_letter = btn.Text;

            Label lbl = new Label
            {
                Text = btn.Text,
                FontSize = Device.GetNamedSize(NamedSize.Header, typeof(Label)),
                TextColor = Color.Black
            };

            Frame dec_lbl = new Frame
            {
                Content = lbl,
                CornerRadius = 50,
                BackgroundColor = Color.Wheat,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = 10,
                BorderColor = Color.Black,
            };

            Button tts = new Button
            {
                ImageSource = "play.png",
                BackgroundColor = Color.Wheat,
                WidthRequest = 200,
                HeightRequest = 200
            };

            Frame dec_btn = new Frame
            {
                Content = tts,
                BackgroundColor = Color.Wheat,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = 10,
                BorderColor = Color.Black
            };

            tts.Clicked += Tts_Clicked;

            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 0,
            };

            stack.Children.Add(dec_lbl);
            stack.Children.Add(dec_btn);

            Frame frm = new Frame
            {
                Content = stack,
                CornerRadius = 10,
                BorderColor = Color.Black,
                BackgroundColor = Color.Wheat,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            await Navigation.PushAsync(new ContentPage
            {
                Content = frm,
                BackgroundColor = Color.BlanchedAlmond
            });
        }

        private async void Tts_Clicked(object sender, System.EventArgs e)
        {
            await Xamarin.Essentials.TextToSpeech.SpeakAsync(strongbox.ALPHABET_letter, new Xamarin.Essentials.SpeechOptions()
            {
                Volume = 1,
                Pitch = 1,
            });
        }
    }
}