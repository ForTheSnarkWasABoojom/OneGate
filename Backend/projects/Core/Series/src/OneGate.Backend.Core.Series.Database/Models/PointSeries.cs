using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Series.Database.Models
{
    [Table("point_series")]
    public class PointSeries : SeriesBase
    {
        [Required] 
        [MaxLength(100)]
        [Column("layout_id")]
        public int LayoutId { get; set; }
        
        [Required] 
        [Column("value")]
        public float Value { get; set; }
    }
}