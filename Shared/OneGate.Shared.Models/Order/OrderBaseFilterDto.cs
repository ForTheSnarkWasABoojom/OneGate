using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Shared.Models.Common;

namespace OneGate.Shared.Models.Order
{
    public class OrderBaseFilterDto : FilterBaseDto
    {
        [FromQuery(Name = "type")]
        [JsonProperty("type")]
        public OrderTypeDto? Type { get; set; }

        [FromQuery(Name = "asset_id")]
        [JsonProperty("asset_id")]
        public int? AssetId { get; set; }

        [FromQuery(Name = "state")]
        [JsonProperty("state")]
        public OrderStateDto? State { get; set; }
        
        [FromQuery(Name = "side")]
        [JsonProperty("side")]
        public OrderSideDto? Side { get; set; }
    }
}