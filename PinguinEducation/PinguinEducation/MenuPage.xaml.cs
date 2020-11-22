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
            await Navigation.PushAsync(new PinguinEducation.Alphabet.AlphabetPage());
        }
    }
}