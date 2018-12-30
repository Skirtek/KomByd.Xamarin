using KomByd.Enums;
using Newtonsoft.Json;

namespace KomByd.Models
{
    public class DepartureDetails
    {
        public VehicleType VehicleType { get; set; }

        [JsonProperty("Linia")]
        public string LineNumber { get; set; }

        [JsonProperty("Kierunek")]
        public string Direction { get; set; }

        [JsonProperty("Odjazd")]
        public string Time { get; set; }
    }
}