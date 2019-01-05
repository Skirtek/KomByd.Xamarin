using System.Threading.Tasks;
using KomByd.Models;

namespace KomByd.Interfaces
{
    public interface IGetDatabaseVersion
    {
        Task<DatabaseVersion> GetVersionFromJson();
    }
}