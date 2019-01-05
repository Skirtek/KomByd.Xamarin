using Newtonsoft.Json;

namespace KomByd.Models
{
    public class DatabaseVersion
    {
        [JsonProperty("wersja")]
        public string Version { get; set; }

        [JsonProperty("aktualizacja")]
        public string Range { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }
}