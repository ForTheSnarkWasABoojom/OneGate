using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Analytics.Database.Models
{
    [Table("job")]
    public class Job
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("provider")]
        public Guid Provider { get; set; }
        
        [Required]
        [Column("metric")]
        public string Metric { get; set; }
        
        [Required]
        [Column("asset_id")]
        public int AssetId { get; set; }

        [Required]
        [Column("options")]
        public string Options { get; set; }
    }
}