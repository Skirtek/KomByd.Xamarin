using System;
using System.Collections.ObjectModel;
using System.Linq;
using KomByd.Models;
using KomByd.Navigation;
using KomByd.Repository.Abstract;
using KomByd.Repository.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class ChooseStopPageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly IStopsRepository _stopsRepository;

        public ChooseStopPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IStopsRepository stopsRepository)
            : base(navigationService, pageDialogService)
        {
            _stopsRepository = stopsRepository;
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

            bool getStop = parameters.TryGetValue(NavParams.ChosenStop, out StopRepo chosenStop);
            if (!getStop)
            {
                await ShowAlert("Ups!", "Coś poszło nie tak");
                await NavigationService.GoBackAsync();
                return;
            }

            ChosenStopName = chosenStop.StopName;
            StopDetails = new ObservableCollection<StopDetails>();

            var vehiclesLists = chosenStop.VehiclesList.Split('#');
            var stopNumbers = chosenStop.StopNumbers.Split(' ');

            foreach (var stop in stopNumbers.Zip(vehiclesLists, Tuple.Create))
            {
                StopDetails.Add(new StopDetails
                {
                    StopName = ChosenStopName,
                    StopNumber = stop.Item1,
                    VehiclesList = stop.Item2
                });
            }
        }
    }
}