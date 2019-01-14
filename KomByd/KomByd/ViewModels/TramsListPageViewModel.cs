using System.Collections.ObjectModel;
using KomByd.Repository.Abstract;
using KomByd.Repository.Models;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class TramsListPageViewModel : BaseViewModel, IPageLifecycleAware
    {
        private readonly ILinesRepository _linesRepository;

        public TramsListPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            ILinesRepository linesRepository)
            : base(navigationService, pageDialogService)
        {
            _linesRepository = linesRepository;
        }

        private ObservableCollection<Line> _tramsList;

        public ObservableCollection<Line> TramsList
        {
            get => _tramsList;
            set => SetProperty(ref _tramsList, value);
        }

        public void OnAppearing()
        {
            TramsList = new ObservableCollection<Line>(_linesRepository.GetAllByCondition(x => x.Type == "T"));
        }

        public DelegateCommand<object> GoToLineDetailsCommand => GetBusyDependedCommand<object>(
            async lineNumber =>
        {
            //var x = lineNumber;
        });
        public void OnDisappearing()
        {
        }
    }
}