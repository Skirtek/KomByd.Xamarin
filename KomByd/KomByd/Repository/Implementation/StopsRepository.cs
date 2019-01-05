using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KomByd.Repository.Abstract;
using KomByd.Repository.Models;
using KomByd.Settings;
using Microsoft.EntityFrameworkCore;

namespace KomByd.Repository.Implementation
{
    public class StopsRepository : IStopsRepository
    {
        private readonly IFileHelper _fileHelper;
        private readonly AppDbContext _databaseContext;

        public StopsRepository(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _databaseContext = new AppDbContext(_fileHelper.GetLocalFilePath(AppSettings.DbFileName));
        }

        public async Task<IEnumerable<StopRepo>> GetStopsAsync()
        {
            try
            {
                return await _databaseContext.StopList.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> Create(StopRepo entity)
        {
            _databaseContext.Add(entity);
            await _databaseContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> GetLastId()
        {
            if (!_databaseContext.StopList.Any())
            {
                return 0;
            }

            var result = await _databaseContext.StopList.LastAsync();
            return result.Id;

        }

        public async Task<bool> Update(StopRepo entity)
        {
            try
            {
                _databaseContext.Update(entity);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<StopRepo> GetAllByCondition(Func<StopRepo, bool> predicate)
        {
            return _databaseContext.StopList.Where(predicate).ToList();
        }

        public async Task<StopRepo> GetByName(string name)
        {
            return await _databaseContext.StopList.FirstOrDefaultAsync(x => x.StopName == name);
        }

        public async Task<bool> Delete(StopRepo entity)
        {
            try
            {
                _databaseContext.StopList.Remove(entity);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAll()
        {
            try
            {
                foreach (var entity in _databaseContext.StopList)
                {
                    _databaseContext.StopList.Remove(entity);
                }
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}