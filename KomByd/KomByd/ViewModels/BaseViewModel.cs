using System;
using System.Threading.Tasks;
using KomByd.Resources;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public IPageDialogService PageDialogService { get; }
        public INavigationService NavigationService { get; }

        protected BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
        } 

        protected DelegateCommand<T> GetBusyDependedCommand<T>(Action<T> executeMethod)
            => new DelegateCommand<T>(executeMethod, (T arg) => !IsBusy).ObservesProperty(() => IsBusy);

        protected DelegateCommand GetBusyDependedCommand(Action executeMethod)
            => new DelegateCommand(executeMethod, () => !IsBusy).ObservesProperty(() => IsBusy);

        protected async Task ShowAlert(string title, string message)
            => await PageDialogService.DisplayAlertAsync(title, message, AppResources.Common_OK);

        protected async Task<bool> ShowAlert(string title, string message, string confirmButton)
            => await PageDialogService.DisplayAlertAsync(
                title,
                message,
                confirmButton,
                AppResources.Common_Cancel);
    }
}