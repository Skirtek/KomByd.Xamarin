using System;
using KomByd.Navigation;
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

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

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

                var x = lineNumber;
            }
            catch (Exception)
            {
                await ShowAlert("Ups!", "Coś poszło nie tak");
                await NavigationService.GoBackAsync();
            }
        }
    }
}