using KomByd.Api;
using KomByd.Api.Interfaces;
using KomByd.Interfaces;
using KomByd.Navigation;
using KomByd.Repository;
using KomByd.Repository.Abstract;
using KomByd.Repository.Implementation;
using KomByd.Services;
using KomByd.Settings;
using KomByd.Settings.Interfaces;
using KomByd.Utils;
using KomByd.Utils.Interfaces;
using KomByd.ViewModels;
using KomByd.Views;
using Microsoft.EntityFrameworkCore;
using Plugin.Connectivity;
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
            string dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath(AppSettings.DbFileName);
            using (var db = new AppDbContext(dbPath))
            {
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }

            await NavigationService.NavigateAsync(NavSettings.CheckUpdatesPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterPagesWithViewModels(containerRegistry);
            RegisterServicesAndUtils(containerRegistry);
            RegisterAddOns(containerRegistry);
        }

        private void RegisterPagesWithViewModels(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CheckUpdatesPage, CheckUpdatesPageViewModel>(NavSettings.CheckUpdatesPage);
            containerRegistry.RegisterForNavigation<MainMenu, MainMenuViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>(NavSettings.MenuPage);
            containerRegistry.RegisterForNavigation<FavouritesPage, FavouritesPageViewModel>(NavSettings.FavouritesPage);
            containerRegistry.RegisterForNavigation<StopsListPage, StopsListPageViewModel>(NavSettings.StopsListPage);
            containerRegistry.RegisterForNavigation<ChooseStopPage, ChooseStopPageViewModel>(NavSettings.ChooseStopPage);
            containerRegistry.RegisterForNavigation<DeparturesPage, DeparturesPageViewModel>(NavSettings.DeparturesPage);
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>(NavSettings.SettingsPage);
        }

        private void RegisterAddOns(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(CrossConnectivity.Current);
            containerRegistry.RegisterInstance(UserSettings.Instance);
        }

        private static void RegisterServicesAndUtils(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGetDeparture, GetDeparture>();
            containerRegistry.Register<IGetData, GetData>();
            containerRegistry.Register<IMaps, Maps>();
            containerRegistry.Register<IStopsRepository, StopsRepository>();
            containerRegistry.Register<IPrepareStopsList, PrepareStopsList>();
            containerRegistry.Register<IGetDatabaseVersion, GetDatabaseVersion>();
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
