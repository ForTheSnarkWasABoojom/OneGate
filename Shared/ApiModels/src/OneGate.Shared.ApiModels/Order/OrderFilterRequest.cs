using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Shared.ApiModels.Common;

namespace OneGate.Shared.ApiModels.Order
{
    public class OrderFilterRequest : FilterModel
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "type")]
        [JsonProperty("type")]
        public OrderTypeModel? Type { get; set; }

        [FromQuery(Name = "asset_id")]
        [JsonProperty("asset_id")]
        public int? AssetId { get; set; }

        [FromQuery(Name = "state")]
        [JsonProperty("state")]
        public OrderStateModel? State { get; set; }
        
        [FromQuery(Name = "side")]
        [JsonProperty("side")]
        public OrderSideModel? Side { get; set; }
    }
}