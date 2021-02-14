using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Assets.Database.Models
{
    [Table("asset")]
    public abstract class Asset
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        
        [Column("type")]
        [Required]
        public string Type { get; set; }

        [Column("exchange_id")]
        [Required]
        public int ExchangeId { get; set; }
        
        [MaxLength(50)]
        [Column("ticker")]
        [Required]
        public string Ticker { get; set; }
        
        [MaxLength(500)]
        [Column("description")]
        public string Description { get; set; }
        
        [ForeignKey(nameof(ExchangeId))]
        public Exchange Exchange { get; set; }
    }
}