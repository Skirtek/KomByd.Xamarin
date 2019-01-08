using System.Collections.Generic;
using System.Threading.Tasks;
using KomByd.Repository.Models;

namespace KomByd.Repository.Abstract
{
    public interface IStopsRepository : IRepository<StopRepo>
    {
        Task<IEnumerable<StopRepo>> GetStopsAsync();

        Task<StopRepo> GetByName(string name);
    }
}