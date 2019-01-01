using System;
using KomByd.Interfaces;
using KomByd.Navigation;
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

        public CheckUpdatesPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IPrepareStopsList prepareStopsList,
            IConnectivity connectivity)
            : base(navigationService, pageDialogService)
        {
            _connectivity = connectivity;
            _prepareStopsList = prepareStopsList;
        }

        public async void OnAppearing()
        {
            IsBusy = true;

            try
            {
                if (!_connectivity.IsConnected)
                {
                    return;
                }

                //if (false)
                //{
                    bool didInsert = await _prepareStopsList.AddStopsToDatabase();
                    if (!didInsert)
                    {
                        await ShowAlert("Ups!", "Coś poszło nie tak");
                    }
                //}
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