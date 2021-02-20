using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using OneGate.Shared.ApiModels.User.Order.Limit;
using OneGate.Shared.ApiModels.User.Order.Market;
using OneGate.Shared.ApiModels.User.Order.Stop;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Shared.ApiModels.User.Order
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(CreateMarketOrderRequest), OrderType.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(CreateLimitOrderRequest), OrderType.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(CreateStopOrderRequest), OrderType.STOP)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(MarketOrder), DiscriminatorValue = nameof(OrderType.MARKET))]
    [SwaggerSubType(typeof(LimitOrder), DiscriminatorValue = nameof(OrderType.LIMIT))]
    [SwaggerSubType(typeof(StopOrder), DiscriminatorValue = nameof(OrderType.STOP))]
    public abstract class CreateOrderRequest
    {
        [JsonProperty("type")]
        [Required] 
        public abstract OrderType? Type { get; }

        [JsonProperty("asset_id")]
        [Required] 
        public int AssetId { get; set; }

        [JsonProperty("side")]
        [Required]
        public OrderSide? Side { get; set; }

        [JsonProperty("quantity")] 
        [Required] 
        public float Quantity { get; set; }
    }
}