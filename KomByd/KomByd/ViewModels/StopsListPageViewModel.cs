using System.Collections.Generic;
using System.Collections.ObjectModel;
using KomByd.Models;
using KomByd.Navigation;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class StopsListPageViewModel : BaseViewModel, IPageLifecycleAware
    {
        public StopsListPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
        }

        private ObservableCollection<Stop> _stopsList;

        public ObservableCollection<Stop> StopsList
        {
            get => _stopsList;
            set => SetProperty(ref _stopsList, value);
        }

        public DelegateCommand<object> GoToStopCommand => GetBusyDependedCommand<object>(async selectedStop =>
        {
            var navParams = new NavigationParameters { { NavParams.ChosenStop, (Stop)selectedStop } };
            await NavigationService.NavigateAsync(NavSettings.ChooseStopPage, navParams);
        });

        public void OnAppearing()
        {
            StopsList = new ObservableCollection<Stop>();
            StopsList.Add(new Stop { StopName = "Twardzickiego / Kleina", StopNumbers = new List<string> { "8036", "8037" } });
            for (int i = 0; i < 20; i++)
            {
                StopsList.Add(new Stop { StopName = $"Przystanek {i}" });
            }
        }

        public void OnDisappearing()
        {
        }
    }
}