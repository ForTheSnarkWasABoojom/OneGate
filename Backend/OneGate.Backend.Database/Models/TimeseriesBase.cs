using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    public abstract class TimeseriesBase
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("timestamp")] 
        public DateTime Timestamp { get; set; }

        [Required] 
        [Column("asset_id")]
        public int AssetId { get; set; }
        
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey("AssetId")]
        [Required] 
        public AssetBase Asset { get; set; }
    }
}