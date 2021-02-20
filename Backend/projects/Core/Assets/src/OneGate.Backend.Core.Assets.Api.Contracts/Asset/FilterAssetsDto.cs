using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Assets.Api.Contracts.Asset
{
    public class FilterAssetsDto : FilterDto
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "ticker")]
        public string Ticker { get; set; }
        
        [FromQuery(Name = "exchange_id")]
        public int? ExchangeId { get; set; }
    }
}