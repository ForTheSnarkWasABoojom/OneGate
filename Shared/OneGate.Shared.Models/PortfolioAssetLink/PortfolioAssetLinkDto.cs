using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.Models.PortfolioAssetLink
{
    public class PortfolioAssetLinkDto
    {
        [JsonProperty("id")]
        [Required]
        public int Id { get; set; }
        
        [JsonProperty("count")]
        [Required]
        public int Count { get; set; }
        
        [JsonProperty("portfolio_id")]
        [Required]
        public int PortfolioId { get; set; }

        [JsonProperty("asset_id")]
        [Required]
        public int AssetId { get; set; }
    }
}