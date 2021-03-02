using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Asset
{
    public class FilterAssetsRequest : FilterRequest
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "ticker")]
        public string Ticker { get; set; }
        
        [FromQuery(Name = "exchange_id")]
        public int? ExchangeId { get; set; }
    }
}