using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using OneGate.Backend.Gateway.User.Api.Contracts.Order.Limit;
using OneGate.Backend.Gateway.User.Api.Contracts.Order.Market;
using OneGate.Backend.Gateway.User.Api.Contracts.Order.Stop;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Order
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(CreateMarketOrderRequest), OrderType.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(CreateLimitOrderRequest), OrderType.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(CreateStopOrderRequest), OrderType.STOP)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(MarketOrderModel), DiscriminatorValue = nameof(OrderType.MARKET))]
    [SwaggerSubType(typeof(LimitOrderModel), DiscriminatorValue = nameof(OrderType.LIMIT))]
    [SwaggerSubType(typeof(StopOrderModel), DiscriminatorValue = nameof(OrderType.STOP))]
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