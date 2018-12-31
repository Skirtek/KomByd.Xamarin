using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KomByd.Repository.Abstract
{
    public interface IRepository<T>
    {
        Task<int> Create(T entity);

        Task<int> GetLastId();

        Task<bool> Update(T entity);

        IEnumerable<T> GetAllByCondition(Func<T, bool> predicate);

        Task<T> GetByName(string name);

        Task<bool> Delete(T entity);
    }
}