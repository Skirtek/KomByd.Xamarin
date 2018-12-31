using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KomByd.Models;
using KomByd.Navigation;
using KomByd.Repository.Abstract;
using KomByd.Repository.Models;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class StopsListPageViewModel : BaseViewModel, IPageLifecycleAware
    {
        private readonly IStopsRepository _stopsRepository;

        public StopsListPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IStopsRepository stopsRepository)
            : base(navigationService, pageDialogService)
        {
            _stopsRepository = stopsRepository;
        }

        private ObservableCollection<StopRepo> _stopsList;

        public ObservableCollection<StopRepo> StopsList
        {
            get => _stopsList;
            set => SetProperty(ref _stopsList, value);
        }

        public DelegateCommand<object> GoToStopCommand => GetBusyDependedCommand<object>(async selectedStop =>
        {
            var navParams = new NavigationParameters { { NavParams.ChosenStop, (StopRepo)selectedStop } };
            await NavigationService.NavigateAsync(NavSettings.ChooseStopPage, navParams);
        });

        public async void OnAppearing()
        {
            var result = await _stopsRepository.GetStopsAsync();
            StopsList = new ObservableCollection<StopRepo>(result);
        }

        public void OnDisappearing()
        {
        }
    }
}