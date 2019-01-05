using System.Collections.Generic;
using KomByd.Repository.Models;

namespace KomByd.Models
{
    public class StopsListSource : List<StopRepo>
    {
        public string GroupName { get; set; }

        public StopsListSource(List<StopRepo> stopsList)
        {
            AddRange(stopsList);
        }
    }
}