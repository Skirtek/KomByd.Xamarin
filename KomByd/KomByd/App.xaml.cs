using KomByd.Navigation;
using KomByd.ViewModels;
using KomByd.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KomByd
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        public App() => InitializeComponent();

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"/{NavSettings.NavigationBase}/{NavSettings.MainMenu}?selectedTab=MenuPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterPagesWithViewModels(containerRegistry);
        }

        private void RegisterPagesWithViewModels(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainMenu, MainMenuViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>(NavSettings.MenuPage);
            containerRegistry.RegisterForNavigation<FavouritesPage, FavouritesPageViewModel>(NavSettings.FavouritesPage);
            containerRegistry.RegisterForNavigation<StopsListPage, StopsListPageViewModel>(NavSettings.StopsListPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
