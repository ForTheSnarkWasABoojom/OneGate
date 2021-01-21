using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.PortfolioAssetLink
{
    public class PortfolioAssetLinkDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("portfolio_id")]
        public int PortfolioId { get; set; }

        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
    }
}