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
    [JsonSubtypes.KnownSubType(typeof(MarketOrderModel), OrderType.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(LimitOrderModel), OrderType.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(StopOrderModel), OrderType.STOP)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(MarketOrderModel), DiscriminatorValue = nameof(OrderType.MARKET))]
    [SwaggerSubType(typeof(LimitOrderModel), DiscriminatorValue = nameof(OrderType.LIMIT))]
    [SwaggerSubType(typeof(StopOrderModel), DiscriminatorValue = nameof(OrderType.STOP))]
    public abstract class OrderModel
    {
        [JsonProperty("id")] 
        [Required] 
        public int Id { get; set; }

        [JsonProperty("type")]
        [Required] 
        public abstract OrderType? Type { get; }

        [JsonProperty("asset_id")]
        [Required] 
        public int AssetId { get; set; }

        [JsonProperty("state")] 
        [Required] 
        public OrderState State { get; set; }
        
        [JsonProperty("side")]
        [Required]
        public OrderSide Side { get; set; }

        [JsonProperty("quantity")] 
        [Required] 
        public float Quantity { get; set; }
    }
}