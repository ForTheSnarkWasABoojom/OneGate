using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Timeseries.Database.Models
{
    [Table("series")]
    public abstract class Series
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("type")]
        [Required]
        public string Type { get; set; }

        [Required]
        [Column("timestamp")] 
        public DateTime Timestamp { get; set; }

        [Required]
        [Column("layer_id")]
        public int LayerId { get; set; }
        
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}