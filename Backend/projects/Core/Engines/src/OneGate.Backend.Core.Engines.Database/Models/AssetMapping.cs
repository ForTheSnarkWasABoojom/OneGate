using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Engines.Database.Models
{
    [Table("asset_mapping")]
    public class AssetMapping
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        
        [Column("engine_id")]
        [Required]
        public int EngineId { get; set; }

        [Column("asset_id")]
        [Required]
        public int AssetId { get; set; }
        
        [Column("original_symbol")]
        [Required]
        public string OriginalSymbol { get; set; }
    }
}