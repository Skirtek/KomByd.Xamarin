using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KomByd.Repository.Abstract;
using KomByd.Repository.Models;
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
    }
}