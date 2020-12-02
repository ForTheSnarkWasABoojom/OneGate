using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Shared.Models.Common;

namespace OneGate.Shared.Models.PortfolioAssetLink
{
    public class PortfolioAssetLinkFilterDto:FilterBaseDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "portfolio_id")]
        [JsonProperty("portfolio_id")]
        public int? PortfolioId { get; set; }
        
        [FromQuery(Name = "asset_id")]
        [JsonProperty("asset_id")]
        public int? AssetId { get; set; }
    }
}