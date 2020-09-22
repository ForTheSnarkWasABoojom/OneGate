using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Database.Models
{
    [Table("order")]
    public abstract class OrderBase
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        
        [Column("type")]
        [Required]
        public string Type { get; set; }
        
        [Column("asset_id")]
        [Required]
        public int AssetId { get; set; }
        
        [Column("account_id")]
        [Required]
        public int AccountId { get; set; }
        
        [Column("state")]
        [Required]
        public string State { get; set; }
        
        [MaxLength(10)]
        [Column("side")]
        [Required]
        public string Side { get; set; }
        
        [Column("quantity")]
        [Required]
        public float Quantity { get; set; }
        
        [ForeignKey("AssetId")]
        [Required]
        public AssetBase Asset { get; set; }

        [ForeignKey("AccountId")]
        [Required]
        public Account Account { get; set; }
    }
}