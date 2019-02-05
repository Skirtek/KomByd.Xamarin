using System;
using System.Collections.Generic;
using KomByd.Models;
using KomByd.Navigation;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class LineDetailsPageViewModel : BaseViewModel, INavigatedAware
    {
        public LineDetailsPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
        }

        private LineDetails _directionsFirst;

        public LineDetails DirectionFirst
        {
            get => _directionsFirst;
            set => SetProperty(ref _directionsFirst, value);
        }

        private LineDetails _directionsSecond;

        public LineDetails DirectionSecond
        {
            get => _directionsSecond;
            set => SetProperty(ref _directionsSecond, value);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public DelegateCommand<object> GoToStopCommand => GetBusyDependedCommand<object>(async selectedStop =>
        {
            var x = (string) selectedStop;
            //var navParams = new NavigationParameters { { NavParams.ChosenStop, (string)selectedStop } };
            //await NavigationService.NavigateAsync(NavSettings.ChooseStopPage, navParams);
        });

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                bool hasLineNumber = parameters.TryGetValue(NavParams.LineNumber, out string lineNumber);
                if (!hasLineNumber)
                {
                    await ShowAlert("Ups!", "Coś poszło nie tak");
                    await NavigationService.GoBackAsync();
                    return;
                }

                var list = new List<LineStop>();
                for (int i = 0; i < 20; i++)
                {
                    list.Add(new LineStop
                    {
                        StopName = $"Przystanek nr {i}.",
                        StopNumber = $"{i}"
                    });
                }

                DirectionFirst = new LineDetails {Direction = "Błonie", Stops = list};
                DirectionSecond = new LineDetails { Direction = "Tatrzańskie", Stops = list };
            }
            catch (Exception)
            {
                await ShowAlert("Ups!", "Coś poszło nie tak");
                await NavigationService.GoBackAsync();
            }
        }
    }
}