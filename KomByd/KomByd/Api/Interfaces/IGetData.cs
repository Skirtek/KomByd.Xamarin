using System.Threading.Tasks;

namespace KomByd.Api.Interfaces
{
    public interface IGetData
    {
        Task<string> GetJsonString(string url);
    }
}