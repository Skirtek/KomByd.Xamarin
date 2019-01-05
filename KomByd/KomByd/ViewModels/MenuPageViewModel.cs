using KomByd.Navigation;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public MenuPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
        }

        public DelegateCommand GoToStopsListCommand =>
            GetBusyDependedCommand(async () =>
        {
            IsBusy = true;
            await NavigationService.NavigateAsync(NavSettings.StopsListPage);
            IsBusy = false;
        });

        public DelegateCommand GoToSettingsCommand =>
            GetBusyDependedCommand(async () =>
            {
                IsBusy = true;
                await NavigationService.NavigateAsync(NavSettings.SettingsPage);
                IsBusy = false;
            });
    }
}
