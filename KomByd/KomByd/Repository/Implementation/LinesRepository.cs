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
    public class LinesRepository : ILinesRepository
    {

        private readonly IFileHelper _fileHelper;
        private readonly AppDbContext _databaseContext;

        public LinesRepository(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _databaseContext = new AppDbContext(_fileHelper.GetLocalFilePath(AppSettings.DbFileName));
        }

        public async Task<int> Create(Line entity)
        {
            _databaseContext.Add(entity);
            await _databaseContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> GetLastId()
        {
            if (!_databaseContext.LinesList.Any())
            {
                return 0;
            }

            var result = await _databaseContext.LinesList.LastAsync();
            return result.Id;
        }

        public async Task<bool> Update(Line entity)
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

        public IEnumerable<Line> GetAllByCondition(Func<Line, bool> predicate)
        {
            return _databaseContext.LinesList.Where(predicate).ToList();
        }

        public async Task<Line> GetByLineNumber(string number)
        {
            return await _databaseContext.LinesList.FirstOrDefaultAsync(x => x.LineNumber == number);
        }

        public async Task<bool> Delete(Line entity)
        {
            try
            {
                _databaseContext.LinesList.Remove(entity);
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
                foreach (var entity in _databaseContext.LinesList)
                {
                    _databaseContext.LinesList.Remove(entity);
                }
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Line>> GetLinesAsync()
        {
            try
            {
                return await _databaseContext.LinesList.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}