using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string _language;
        public string Language
        {
            get => _language;
            set => SetProperty(ref _language, value);
        }
        public ObservableCollection<int> ItemsSource2 { get; } = new ObservableCollection<int>();
        public ObservableCollection<int> SelectedItems2 { get; } = new ObservableCollection<int>();
        private ObservableCollection<string> _languageList;
        public ObservableCollection<string> LanguageList
        {
            get => _languageList;
            set => SetProperty(ref _languageList, value);
        }

        public void OnAppearing()
        {
            DatabaseVersion = _userSettings.CurrentDatabaseVersion;
            Language = _userSettings.Language;
            LanguageList = new ObservableCollection<string>{"polski", "angielski", "ukraiński"};
            ItemsSource2.Add(1);
            ItemsSource2.Add(2);
            SelectedItems2.Add(1);
        }

        public void OnDisappearing()
        {
        }
    }
}