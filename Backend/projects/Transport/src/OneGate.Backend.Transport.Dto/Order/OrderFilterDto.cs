using Newtonsoft.Json;
using OneGate.Backend.Transport.Dto.Common;

namespace OneGate.Backend.Transport.Dto.Order
{
    public class OrderFilterDto : FilterDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("type")]
        public OrderTypeDto? Type { get; set; }
        
        [JsonProperty("asset_id")]
        public int? AssetId { get; set; }
        
        [JsonProperty("state")]
        public OrderStateDto? State { get; set; }

        [JsonProperty("side")]
        public OrderSideDto? Side { get; set; }
    }
}