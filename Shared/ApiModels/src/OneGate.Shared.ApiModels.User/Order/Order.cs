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
    [JsonSubtypes.KnownSubType(typeof(MarketOrder), OrderType.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(LimitOrder), OrderType.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(StopOrder), OrderType.STOP)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(MarketOrder), DiscriminatorValue = nameof(OrderType.MARKET))]
    [SwaggerSubType(typeof(LimitOrder), DiscriminatorValue = nameof(OrderType.LIMIT))]
    [SwaggerSubType(typeof(StopOrder), DiscriminatorValue = nameof(OrderType.STOP))]
    public abstract class Order
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