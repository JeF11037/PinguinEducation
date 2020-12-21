
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Media;

namespace PinguinEducation.Droid
{
    [Activity(Label = "PinguinEducation", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MediaPlayer mediaPlayer { get; private set; }
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            mediaPlayer = MediaPlayer.Create(Instance, Resource.Raw.song);
            mediaPlayer.Looping = true;
            mediaPlayer.Start();

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(Instance, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(Instance, savedInstanceState);
            LoadApplication(new App());
        }

        public void Mute(bool mute)
        {
            if (mute)
            {
                mediaPlayer.Stop();
            }
            else
            {
                mediaPlayer = MediaPlayer.Create(Instance, Resource.Raw.song);
                mediaPlayer.Looping = true;
                mediaPlayer.Start();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}