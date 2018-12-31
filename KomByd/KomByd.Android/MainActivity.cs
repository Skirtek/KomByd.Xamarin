using Android.App;
using Android.Content.PM;
using Android.OS;
using KomByd.Droid.Dependencies;
using KomByd.Repository.Abstract;
using Prism;
using Prism.Ioc;

namespace KomByd.Droid
{
    [Activity(Label = "KomByd", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static readonly FileHelper FileHelperImplementation = new FileHelper();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            App komByd = new App(new AndroidInitialzier());
            LoadApplication(komByd);
        }

        public class AndroidInitialzier : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterInstance<IFileHelper>(FileHelperImplementation);
            }
        }
    }
}