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
            await Xamarin.Essentials.TextToSpeech.SpeakAsync(btn.Text, new Xamarin.Essentials.SpeechOptions()
            {
                Volume = 1,
                Pitch = 1,
            });
        }
    }
}