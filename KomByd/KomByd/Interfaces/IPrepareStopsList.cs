using System.Threading.Tasks;

namespace KomByd.Interfaces
{
    public interface IPrepareStopsList
    {
        Task<bool> AddStopsToDatabase();
    }
}