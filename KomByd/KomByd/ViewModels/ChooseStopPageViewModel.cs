using System.Collections.ObjectModel;
using KomByd.Models;
using KomByd.Navigation;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class ChooseStopPageViewModel : BaseViewModel, INavigatedAware
    {
        public ChooseStopPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
        }

        private string _chosenStopName;
        public string ChosenStopName
        {
            get => _chosenStopName;
            set => SetProperty(ref _chosenStopName, value);
        }

        private ObservableCollection<StopDetails> _stopsDetails;
        public ObservableCollection<StopDetails> StopDetails
        {
            get => _stopsDetails;
            set => SetProperty(ref _stopsDetails, value);
        }

        public DelegateCommand<object> GoToDeparturesCommand =>
            GetBusyDependedCommand<object>(async stop =>
            {
                var navParams = new NavigationParameters { { NavParams.DepartureStop, (StopDetails)stop } };
                await NavigationService.NavigateAsync(NavSettings.DeparturesPage, navParams);
            });

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                return;
            }

            bool getStop = parameters.TryGetValue(NavParams.ChosenStop, out Stop chosenStop);
            if (!getStop)
            {
                await ShowAlert("Ups!", "Coś poszło nie tak");
                await NavigationService.GoBackAsync();
                return;
            }

            ChosenStopName = chosenStop.StopName;

            StopDetails = new ObservableCollection<StopDetails>
            {
                new StopDetails
                {
                    StopName = chosenStop.StopName,
                    StopNumber = "8036",
                    Buses = "69 -> Tatrzańskie 83 -> Tatrzańskie 89 -> Tatrzańskie 32N -> Tatrzańskie"
                },
                new StopDetails
                {
                    StopName = chosenStop.StopName,
                    StopNumber = "8037",
                    Buses = "69 -> Błonie 83 -> Czyżkówko 89 -> Błonie 32N -> Dworzec Błonie"
                }
            };

        }
    }
}