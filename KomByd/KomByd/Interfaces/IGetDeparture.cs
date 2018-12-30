using System.Threading.Tasks;
using KomByd.Enums;
using KomByd.Models;

namespace KomByd.Interfaces
{
    public interface IGetDeparture
    {
        Task<DeparturesList> GetDeparturesForStopNumber(string number);

        DeparturesList SetupFailedList(ErrorType errorType);

        VehicleType DetermineVehicleType(string lineNumber);
    }
}