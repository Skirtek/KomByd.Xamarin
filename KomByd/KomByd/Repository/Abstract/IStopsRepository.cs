using System.Collections.Generic;
using System.Threading.Tasks;
using KomByd.Repository.Models;

namespace KomByd.Repository.Abstract
{
    public interface IStopsRepository
    {
        Task<IEnumerable<StopRepo>> GetStopsAsync();
    }
}