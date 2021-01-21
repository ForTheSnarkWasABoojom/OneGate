using Newtonsoft.Json;
using OneGate.Backend.Transport.Dto.Common;

namespace OneGate.Backend.Transport.Dto.PortfolioAssetLink
{
    public class PortfolioAssetLinkFilterDto:FilterDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("portfolio_id")]
        public int? PortfolioId { get; set; }
        
        [JsonProperty("asset_id")]
        public int? AssetId { get; set; }
    }
}