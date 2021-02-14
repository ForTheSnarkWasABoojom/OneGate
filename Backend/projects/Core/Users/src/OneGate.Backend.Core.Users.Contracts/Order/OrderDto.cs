using JsonSubTypes;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Order
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(MarketOrderDto), OrderTypeDto.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(LimitOrderDto), OrderTypeDto.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(StopOrderDto), OrderTypeDto.STOP)]
    public abstract class OrderDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public abstract OrderTypeDto? Type { get; }

        [JsonProperty("asset_id")]
        public int AssetId { get; set; }

        [JsonProperty("state")]
        public OrderStateDto State { get; set; }

        [JsonProperty("side")]
        public OrderSideDto Side { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }
    }
}