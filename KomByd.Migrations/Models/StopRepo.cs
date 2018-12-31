using System.ComponentModel.DataAnnotations.Schema;

namespace KomByd.Migrations.Models
{
    [Table("Stops")]
    public class StopRepo
    {
        public int Id { get; set; }

        public string StopName { get; set; }

        public string StopNumbers { get; set; }

        public string VehiclesList { get; set; }
    }
}