using System;
using System.Net.Http;
using System.Threading.Tasks;
using KomByd.Api.Interfaces;

namespace KomByd.Api
{
    public class GetData : IGetData
    {
        public async Task<string> GetJsonString(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {                    
                    return await client.GetStringAsync(url);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
