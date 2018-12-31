using System;
using System.IO;
using KomByd.iOS.Dependencies;
using KomByd.Repository.Abstract;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace KomByd.iOS.Dependencies
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", filename);
        }
    }
}