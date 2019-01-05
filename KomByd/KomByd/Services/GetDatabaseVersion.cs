using System;
using System.Threading.Tasks;
using KomByd.Api.Interfaces;
using KomByd.Interfaces;
using KomByd.Models;
using KomByd.Settings;
using Newtonsoft.Json.Linq;

namespace KomByd.Services
{
    public class GetDatabaseVersion : IGetDatabaseVersion
    {
        private readonly IGetData _getData;

        public GetDatabaseVersion(IGetData getData)
        {
            _getData = getData;
        }


        public async Task<DatabaseVersion> GetVersionFromJson()
        {
            try
            {
                string response = await _getData.GetJsonString(AppSettings.DatabaseVersionJson);

                if (string.IsNullOrEmpty(response))
                {
                    return new DatabaseVersion
                    {
                        IsSuccess = false
                    };
                }

                JObject responseHandler = JObject.Parse(response);

                var objectToReturn = responseHandler.ToObject<DatabaseVersion>();

                objectToReturn.IsSuccess = true;

                return objectToReturn;
            }
            catch (Exception ex)
            {
                return new DatabaseVersion
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }
    }
}