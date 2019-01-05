using KomByd.Settings.Interfaces;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;

namespace KomByd.ViewModels
{
    public class SettingsPageViewModel: BaseViewModel, IPageLifecycleAware
    {
        private readonly IUserSettings _userSettings;

        public SettingsPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialogService,
            IUserSettings userSettings)
            : base(navigationService, pageDialogService)
        {
            _userSettings = userSettings;
        }

        private string _databaseVersion;

        public string DatabaseVersion
        {
            get => _databaseVersion;
            set => SetProperty(ref _databaseVersion, value);
        }

        public void OnAppearing()
        {
            DatabaseVersion = _userSettings.CurrentDatabaseVersion;
        }

        public void OnDisappearing()
        {
        }
    }
}