using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Shared.ApiContracts.Common;

namespace OneGate.Shared.ApiContracts.Order
{
    public class OrderFilterDto : FilterDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
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