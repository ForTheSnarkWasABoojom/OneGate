using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Timeseries.Database.Models
{
    [Table("ohlc")]
    public class Ohlc
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("asset_id")]
        public int AssetId { get; set; }
        
        [Required]
        [Column("interval")] 
        public string Interval { get; set; }

        [Required]
        [Column("low")]
        public double Low { get; set; }

        [Required] 
        [Column("high")]
        public double High { get; set; }

        [Required]
        [Column("open")]
        public double Open { get; set; }

        [Required]
        [Column("close")]
        public double Close { get; set; }
        
        [Required]
        [Column("volume")]
        public double Volume { get; set; }
        
        [Required]
        [Column("timestamp")] 
        public DateTime Timestamp { get; set; }
        
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}