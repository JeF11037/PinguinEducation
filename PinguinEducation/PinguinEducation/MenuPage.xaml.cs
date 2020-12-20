using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PinguinEducation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Text)
            {
                case "Alphabet":
                    await Navigation.PushAsync(new PinguinEducation.Alphabet.AlphabetPage());
                    break;
                case "Game":
                    await Navigation.PushAsync(new PinguinEducation.Game.TileMatchingVideoGame());
                    break;
                default:
                    break;
            }
        }
    }
}