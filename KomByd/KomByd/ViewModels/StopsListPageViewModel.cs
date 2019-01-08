using System.Collections.ObjectModel;
using System.Globalization;
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

        private ObservableCollection<StopsListSource> _stopsList;
        public ObservableCollection<StopsListSource> StopsList
        {
            get => _stopsList;
            set => SetProperty(ref _stopsList, value);
        }

        private bool _noStopsVisibility;

        public bool NoStopsVisibility
        {
            get => _noStopsVisibility;
            set => SetProperty(ref _noStopsVisibility, value);
        }

        private StopsListSource RecentlyUsedStops { get; set; }

        private StopsListSource AllStops { get; set; }

        public DelegateCommand<object> GoToStopCommand => GetBusyDependedCommand<object>(async selectedStop =>
        {
            var navParams = new NavigationParameters { { NavParams.ChosenStop, (StopRepo)selectedStop } };
            await NavigationService.NavigateAsync(NavSettings.ChooseStopPage, navParams);
        });

        public DelegateCommand<object> FilterListView => new DelegateCommand<object>(searchBarValue =>
        {
            string enteredText = searchBarValue.ToString().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(enteredText))
            {
                StopsList = new ObservableCollection<StopsListSource> { RecentlyUsedStops, AllStops };
                return;
            }

            StopsList = new ObservableCollection<StopsListSource>();

            var compareInfo = CultureInfo.InvariantCulture.CompareInfo;

            var filteredRecentStops = RecentlyUsedStops.Where(
                x => compareInfo.IndexOf(x.StopName.ToLower(), enteredText, CompareOptions.IgnoreNonSpace) > -1).ToList();

            var filteredAllStops = AllStops.Where(
                x => compareInfo.IndexOf(x.StopName.ToLower(), enteredText, CompareOptions.IgnoreNonSpace) > -1).ToList();


            if (filteredRecentStops.Count > 0)
            {
                StopsList.Add(new StopsListSource(filteredRecentStops) { GroupName = "Ostatnio używane przystanki" });
            }

            if (filteredAllStops.Count > 0)
            {
                NoStopsVisibility = false;
                StopsList.Add(new StopsListSource(filteredAllStops) { GroupName = "Wszystkie przystanki" });
                return;
            }

            if (StopsList.Count == 0)
            {
                NoStopsVisibility = true;
            }
        });

        public async void OnAppearing()
        {
            IsBusy = true;
            var result = await _stopsRepository.GetStopsAsync();
            var stopRepos = result.ToList();

            RecentlyUsedStops = new StopsListSource(stopRepos.GetRange(0, 3)) { GroupName = "Ostatnio używane przystanki" };

            AllStops = new StopsListSource(stopRepos) { GroupName = "Wszystkie przystanki" };

            StopsList = new ObservableCollection<StopsListSource> { RecentlyUsedStops, AllStops };

            IsBusy = false;
        }

        public void OnDisappearing()
        {
        }
    }
}