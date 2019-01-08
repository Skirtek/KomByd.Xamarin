using System.ComponentModel.DataAnnotations.Schema;

namespace KomByd.Migrations.Models
{
    [Table("Lines")]
    public class Line
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string LineNumber { get; set; }

        public string DirectionsList { get; set; }
    }
}