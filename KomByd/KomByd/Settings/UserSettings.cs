using KomByd.Settings.Interfaces;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace KomByd.Settings
{
    public class UserSettings : IUserSettings
    {
        private readonly ISettings _settings;

        public UserSettings(ISettings settings)
        {
            _settings = settings;
        }

        private static IUserSettings _instance;
        public static IUserSettings Instance => _instance ?? (_instance = new UserSettings(CrossSettings.Current));

        public string CurrentDatabaseVersion
        {
            get => _settings.GetValueOrDefault(nameof(CurrentDatabaseVersion), "1");
            set => _settings.AddOrUpdateValue(nameof(CurrentDatabaseVersion), value);
        }

        public string Language
        {
            get => _settings.GetValueOrDefault(nameof(Language), "polski");
            set => _settings.AddOrUpdateValue(nameof(Language), value);
        }
    }
}