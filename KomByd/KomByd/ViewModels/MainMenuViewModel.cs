using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
        }

        public DelegateCommand AboutApp =>
            new DelegateCommand(async () =>
            {
                await ShowAlert("KomByd 1.0.91", "Aplikację stworzył");
            });
    }
}
