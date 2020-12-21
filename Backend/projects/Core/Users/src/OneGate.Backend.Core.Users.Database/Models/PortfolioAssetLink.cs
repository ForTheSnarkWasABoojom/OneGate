using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Users.Database.Models
{
    [Table("portfolio_asset_link")]
    public class PortfolioAssetLink
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        
        [Column("count")]
        [Required]
        public int Count { get; set; }
        
        [Column("portfolio_id")]
        [Required]
        public int PortfolioId { get; set; }
            
        [ForeignKey(nameof(PortfolioId))]
        public Portfolio Portfolio { get; set; }
        
        [Column("asset_id")]
        [Required]
        public int AssetId { get; set; }
    }
}