using JsonSubTypes;
using Newtonsoft.Json;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Limit;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Market;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Stop;

namespace OneGate.Backend.Core.Users.Api.Contracts.Order
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

        [JsonProperty("state")]
        public OrderStateDto State { get; set; }

        [JsonProperty("side")]
        public OrderSideDto Side { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }
    }
}