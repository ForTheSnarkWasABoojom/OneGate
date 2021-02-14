using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Timeseries.Database.Models
{
    [Table("series")]
    public class PointSeries : Series
    {
        [Required] 
        [Column("value")]
        public float Value { get; set; }
    }
}