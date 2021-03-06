﻿using System.Collections.ObjectModel;
using KomByd.Navigation;
using KomByd.Repository.Abstract;
using KomByd.Repository.Models;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    //Z - zwykła N - nocna S - specjalna M - międzygminna
    public class BusesListPageViewModel : BaseViewModel, IPageLifecycleAware
    {
        private readonly ILinesRepository _linesRepository;

        public BusesListPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            ILinesRepository linesRepository)
            : base(navigationService, pageDialogService)
        {
            _linesRepository = linesRepository;
        }

        private ObservableCollection<Line> _busesList;

        public ObservableCollection<Line> BusesList
        {
            get => _busesList;
            set => SetProperty(ref _busesList, value);
        }

        public void OnAppearing()
        {
            BusesList = new ObservableCollection<Line>(_linesRepository.GetAllByCondition(x => x.Type == "Z"));
        }

        public DelegateCommand<object> GoToLineDetailsCommand
            => GetBusyDependedCommand<object>(
                async lineNumber =>
                {
                    var navParams = new NavigationParameters { { NavParams.LineNumber, lineNumber.ToString() } };
                    await NavigationService.NavigateAsync(NavSettings.LineDetailsPage, navParams);
                });

        public void OnDisappearing()
        {
        }
    }
}