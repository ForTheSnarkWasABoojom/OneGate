using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Order
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(MarketOrderDto), OrderTypeDto.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(LimitOrderDto), OrderTypeDto.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(StopOrderDto), OrderTypeDto.STOP)]
    public abstract class OrderDto
    {
        [JsonProperty("id")] 
        [Required] 
        public int Id { get; set; }

        [JsonProperty("type")]
        [Required] 
        public abstract OrderTypeDto? Type { get; }

        [JsonProperty("asset_id")]
        [Required] 
        public int AssetId { get; set; }

        [JsonProperty("state")] 
        [Required] 
        public OrderStateDto State { get; set; }
        
        [JsonProperty("side")]
        [Required]
        public OrderSideDto Side { get; set; }

        [JsonProperty("quantity")] 
        [Required] 
        public float Quantity { get; set; }
    }
}