using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.PortfolioAssetLink
{
    public class CreatePortfolioAssetLinkDto
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("portfolio_id")]
        public int PortfolioId { get; set; }

        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
    }
}