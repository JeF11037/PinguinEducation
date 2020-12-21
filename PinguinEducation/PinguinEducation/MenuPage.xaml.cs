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
            Start();
        }

        private async void Start()
        {
            await Navigation.PushAsync(new PinguinEducation.Alphabet.AlphabetPage());
        }
    }
}