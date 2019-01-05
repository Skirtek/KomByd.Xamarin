using System;
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
    public class PrepareStopsList : IPrepareStopsList
    {
        private readonly IGetData _getData;
        private readonly IMaps _maps;
        private readonly IStopsRepository _stopsRepository;
        private int LastId { get; set; }

        public PrepareStopsList(IGetData getData, IStopsRepository stopsRepository, IMaps maps)
        {
            _getData = getData;
            _stopsRepository = stopsRepository;
            _maps = maps;
        }

        public async Task<bool> AddStopsToDatabase()
        {
            try
            {
                string response = await _getData.GetJsonString(AppSettings.StopsListJson);
                if (string.IsNullOrEmpty(response))
                {
                    return false;
                }

                JArray linesArray = JArray.Parse(response);

                LastId = await _stopsRepository.GetLastId();

                if (LastId != 0)
                {
                    await _stopsRepository.DeleteAll();
                }

                foreach (var stop in linesArray)
                {
                    await _stopsRepository.Create(new StopRepo
                    {
                        Id = LastId + 1,
                        StopName = _maps.MapUnicodeCharsToPolishChars(stop.Value<string>("nazwa")),
                        StopNumbers = stop.Value<string>("numery"),
                        VehiclesList = _maps.MapUnicodeCharsToPolishChars(stop.Value<string>("kierunki"))
                    });
                    LastId++;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}