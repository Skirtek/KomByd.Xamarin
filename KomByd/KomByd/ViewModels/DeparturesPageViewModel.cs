using System;
using System.Collections.ObjectModel;
using KomByd.Interfaces;
using KomByd.Models;
using KomByd.Navigation;
using Plugin.Connectivity.Abstractions;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class DeparturesPageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly IGetDeparture _getDeparture;
        private readonly IConnectivity _connectivity;

        public DeparturesPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IConnectivity connectivity,
            IGetDeparture getDeparture)
            : base(navigationService, pageDialogService)
        {
            _connectivity = connectivity;
            _getDeparture = getDeparture;
        }

        private StopDetails _passedStop;
        public StopDetails PassedStop
        {
            get => _passedStop;
            set => SetProperty(ref _passedStop, value);
        }

        private ObservableCollection<DepartureDetails> _departuresDetails;
        public ObservableCollection<DepartureDetails> DeparturesDetails
        {
            get => _departuresDetails;
            set => SetProperty(ref _departuresDetails, value);
        }

        private string _departuresStopName;
        public string DeparturesStopName
        {
            get => _departuresStopName;
            set => SetProperty(ref _departuresStopName, value);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                if (!_connectivity.IsConnected)
                {
                    await ShowAlert("Brak połączenia z Internetem",
                        "Aby sprawdzić odjazdy konieczne jest połączenie z Internetem.");
                    await NavigationService.GoBackAsync();
                    return;
                }

                bool getStop = parameters.TryGetValue(NavParams.DepartureStop, out StopDetails departureStop);
                if (!getStop)
                {
                    await ShowAlert("Ups!", "Coś poszło nie tak");
                    await NavigationService.GoBackAsync();
                    return;
                }

                PassedStop = departureStop;
                DeparturesStopName = departureStop.StopName;

                GetDepartures();
            }
            catch (Exception)
            {
                await ShowAlert("Ups!", "Coś poszło nie tak");
                await NavigationService.GoBackAsync();
            }
        }

        private async void GetDepartures()
        {
            var result = await _getDeparture.GetDeparturesForStopNumber(PassedStop.StopNumber);

            if (!result.IsSuccess)
            {
                await ShowAlert("Ups!", "Coś poszło nie tak");
                await NavigationService.GoBackAsync();
                return;
            }

            DeparturesDetails = new ObservableCollection<DepartureDetails>(result.DepartureList);
        }
    }
}