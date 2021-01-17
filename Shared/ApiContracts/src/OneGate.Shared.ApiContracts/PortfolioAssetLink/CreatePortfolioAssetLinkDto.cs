using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiContracts.PortfolioAssetLink
{
    public class CreatePortfolioAssetLinkDto
    {
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