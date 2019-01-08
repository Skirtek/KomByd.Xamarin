using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KomByd.Api.Interfaces;
using KomByd.Interfaces;
using KomByd.Repository.Abstract;
using KomByd.Repository.Models;
using KomByd.Utils.Interfaces;

namespace KomByd.Services
{
    public class PrepareLinesList : IPrepareLinesList
    {
        private readonly IGetData _getData;
        private readonly IMaps _maps;
        private readonly ILinesRepository _linesRepository;

        private int LastId { get; set; }

        public PrepareLinesList(IGetData getData, ILinesRepository linesRepository, IMaps maps)
        {
            _getData = getData;
            _linesRepository = linesRepository;
            _maps = maps;
        }

        public async Task<bool> AddLinesToDatabase()
        {
            LastId = await _linesRepository.GetLastId();
            await _linesRepository.Create(new Line {Id = LastId + 1, LineNumber = (LastId + 1).ToString()});
            return true;
        }
    }
}