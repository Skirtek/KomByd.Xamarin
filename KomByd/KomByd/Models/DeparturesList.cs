using System.Collections.Generic;

namespace KomByd.Models
{
    public class DeparturesList
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public IEnumerable<DepartureDetails> DepartureList { get; set; }
    }
}