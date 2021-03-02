using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Timeseries.Database.Models
{
    [Table("layer")]
    public abstract class Layer
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [Column("asset_id")]
        public int AssetId { get; set; }

        [Required]
        [Column("is_master")]
        public bool IsMaster { get; set; }
        
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }
    }
}