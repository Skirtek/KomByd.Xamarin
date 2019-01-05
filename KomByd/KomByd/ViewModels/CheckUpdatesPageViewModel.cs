using System;
using KomByd.Interfaces;
using KomByd.Navigation;
using KomByd.Settings.Interfaces;
using Plugin.Connectivity.Abstractions;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class CheckUpdatesPageViewModel : BaseViewModel, IPageLifecycleAware
    {
        private readonly IConnectivity _connectivity;
        private readonly IPrepareStopsList _prepareStopsList;
        private readonly IGetDatabaseVersion _getDatabaseVersion;
        private readonly IUserSettings _userSettings;

        public CheckUpdatesPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IPrepareStopsList prepareStopsList,
            IGetDatabaseVersion getDatabaseVersion,
            IUserSettings userSettings,
            IConnectivity connectivity)
            : base(navigationService, pageDialogService)
        {
            _connectivity = connectivity;
            _prepareStopsList = prepareStopsList;
            _getDatabaseVersion = getDatabaseVersion;
            _userSettings = userSettings;
        }

        public async void OnAppearing()
        {
            IsBusy = true;

            try
            {
                if (!_connectivity.IsConnected)
                {
                    await ShowAlert("Brak połączenia z Internetem", "Bez dostępnego połączenia z Internetem nie jest możliwe sprawdzenie bieżących odjazdów");
                    return;
                }

                var databaseCheck = await _getDatabaseVersion.GetVersionFromJson();

                string version = databaseCheck.Version;

                if (version.Equals(_userSettings.CurrentDatabaseVersion))
                {
                    return;
                }

                bool didInsert = await _prepareStopsList.AddStopsToDatabase();
                if (!didInsert)
                {
                    await ShowAlert("Ups!", "Coś poszło nie tak");
                    return;
                }

                _userSettings.CurrentDatabaseVersion = version;
            }
            catch (Exception ex)
            {
                await ShowAlert("Ups!", "Nie udało się sprawdzić aktualizacji");
            }
            finally
            {
                await NavigationService.NavigateAsync($"/{NavSettings.NavigationBase}/{NavSettings.MainMenu}?selectedTab=MenuPage");
                IsBusy = false;
            }
        }

        public void OnDisappearing()
        {
        }
    }
}