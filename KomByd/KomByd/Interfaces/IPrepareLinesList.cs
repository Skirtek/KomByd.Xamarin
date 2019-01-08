using System.Threading.Tasks;

namespace KomByd.Interfaces
{
    public interface IPrepareLinesList
    {
        Task<bool> AddLinesToDatabase();
    }
}