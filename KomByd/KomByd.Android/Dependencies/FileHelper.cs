using System.IO;
using KomByd.Droid.Dependencies;
using KomByd.Repository.Abstract;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(FileHelper))]
namespace KomByd.Droid.Dependencies
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
        }
    }
}