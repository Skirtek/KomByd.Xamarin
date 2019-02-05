using System.Threading.Tasks;
using KomByd.Api.Interfaces;
using KomByd.Interfaces;
using KomByd.Repository.Abstract;
using KomByd.Repository.Models;
using KomByd.Settings;
using KomByd.Utils.Interfaces;
using Newtonsoft.Json.Linq;

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
            string response = await _getData.GetJsonString(AppSettings.LinesListJson);
            if (string.IsNullOrEmpty(response))
            {
                return false;
            }

            JArray linesArray = JArray.Parse(response);

            LastId = await _linesRepository.GetLastId();

            if (LastId != 0)
            {
                await _linesRepository.DeleteAll();
            }

            foreach (var lines in linesArray)
            {
                await _linesRepository.Create(new Line
                {
                    Id = LastId + 1,
                    Type = lines.Value<string>("typ"),
                    LineNumber = lines.Value<string>("numerlinii"),
                    DirectionsList = _maps.MapUnicodeCharsToPolishChars(lines["kierunki"].ToString())
                });
                LastId++;
            }
            return true;
        }
    }
}