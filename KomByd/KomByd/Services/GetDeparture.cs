using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KomByd.Api.Interfaces;
using KomByd.Enums;
using KomByd.Interfaces;
using KomByd.Models;
using KomByd.Resources;
using KomByd.Utils.Interfaces;
using Newtonsoft.Json.Linq;

namespace KomByd.Services
{
    public class GetDeparture : IGetDeparture
    {
        private readonly IGetData _getData;
        private readonly IMaps _maps;

        public GetDeparture(IGetData getData, IMaps maps)
        {
            _getData = getData;
            _maps = maps;
        }

        public async Task<DeparturesList> GetDeparturesForStopNumber(string number)
        {
            try
            {
                List<DepartureDetails> departureList = new List<DepartureDetails>();
                string response = await _getData.GetJsonString(string.Format(AppResources.Url_GetDepartures, number));

                if (string.IsNullOrEmpty(response))
                {
                    return SetupFailedList(ErrorType.NoDepartures);
                }

                if (!(JObject.Parse(response)["odjazdy"] is JArray departuresArray))
                {
                    return SetupFailedList(ErrorType.NoDepartures);
                }

                foreach (var departure in departuresArray)
                {
                    var departureItem = departure.ToObject<DepartureDetails>();
                    departureItem.LineNumber = departureItem.LineNumber.Trim();
                    departureItem.Direction = _maps.MapUnicodeCharsToPolishChars(departureItem.Direction).Trim();
                    departureItem.Time = departureItem.Time.Trim().Replace(">>","teraz");
                    departureItem.VehicleType = DetermineVehicleType(departureItem.LineNumber);

                    departureList.Add(departureItem);
                }

                return new DeparturesList
                {
                    IsSuccess = true,
                    DepartureList = departureList
                };
            }
            catch (Exception ex)
            {
                return new DeparturesList
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public DeparturesList SetupFailedList(ErrorType errorType) => new DeparturesList
        {
            IsSuccess = false,
            Message = _maps.MapErrorTypeToMessage(errorType)
        };

        public VehicleType DetermineVehicleType(string lineNumber)
        {
            bool isValidNumber = int.TryParse(lineNumber, out int number);

            if (!isValidNumber || number > 10 || number < 1)
            {
                return VehicleType.Bus;
            }

            return VehicleType.Tram;
        }
    }
}