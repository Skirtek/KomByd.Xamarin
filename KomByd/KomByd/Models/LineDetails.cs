using System.Collections.Generic;

namespace KomByd.Models
{
    public class LineDetails
    {
        public string Direction { get; set; }

        public List<LineStop> Stops { get; set; }
    }
}
