using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Asset.Database.Models
{
    [Table("asset")]
    public abstract class AssetBase
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("type")]
        public string Type { get; set; }

        [Column("exchange_id")]
        [Required]
        public int ExchangeId { get; set; }
        
        [MaxLength(50)]
        [Column("ticker")]
        public string Ticker { get; set; }
        
        [MaxLength(500)]
        [Column("description")]
        public string Description { get; set; }
        
        [ForeignKey(nameof(ExchangeId))]
        public Exchange Exchange { get; set; }
    }
}