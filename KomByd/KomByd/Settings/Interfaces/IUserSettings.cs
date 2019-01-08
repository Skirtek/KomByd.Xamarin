namespace KomByd.Settings.Interfaces
{
    public interface IUserSettings
    {
        string CurrentDatabaseVersion { get; set; }

        string Language { get; set; }
    }
}