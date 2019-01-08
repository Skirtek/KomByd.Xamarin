using System.Collections.Generic;
using System.Threading.Tasks;
using KomByd.Repository.Models;

namespace KomByd.Repository.Abstract
{
    public interface ILinesRepository : IRepository<Line>
    {
        Task<IEnumerable<Line>> GetLinesAsync();

        Task<Line> GetByLineNumber(string number);
    }
}