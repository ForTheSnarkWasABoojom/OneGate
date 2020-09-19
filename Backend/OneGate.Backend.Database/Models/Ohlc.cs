using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    [Table("ohlc")]
    public class Ohlc
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("low")]
        public float Low { get; set; }
        
        [Required]
        [Column("high")]
        public float High { get; set; }
        
        [Required]
        [Column("open")]
        public float Open { get; set; }
        
        [Required]
        [Column("close")]
        public float Close { get; set; }
        
        [Required]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        
        [Required]
        [MaxLength(4)]
        [Column("interval")]
        public string Interval { get; set; }
        
        [Required]
        [Column("asset_id")]
        public int AssetId { get; set; }
        
        [ForeignKey("AssetId")]
        [Required]
        public AssetBase Asset { get; set; }
    }
}