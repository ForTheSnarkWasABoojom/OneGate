using JsonSubTypes;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Order
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(CreateMarketOrderDto), OrderTypeDto.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(CreateLimitOrderDto), OrderTypeDto.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(CreateStopOrderDto), OrderTypeDto.STOP)]
    public abstract class CreateOrderDto
    {
        [JsonProperty("type")]
        public abstract OrderTypeDto? Type { get; }

        [JsonProperty("asset_id")]
        public int AssetId { get; set; }

        [JsonProperty("side")]
        public OrderSideDto? Side { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }
    }
}