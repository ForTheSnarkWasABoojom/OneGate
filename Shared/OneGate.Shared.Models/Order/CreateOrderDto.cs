using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Shared.Models.Order
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(CreateMarketOrderDto), OrderTypeDto.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(CreateLimitOrderDto), OrderTypeDto.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(CreateStopOrderDto), OrderTypeDto.STOP)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(MarketOrderDto), DiscriminatorValue = nameof(OrderTypeDto.MARKET))]
    [SwaggerSubType(typeof(LimitOrderDto), DiscriminatorValue = nameof(OrderTypeDto.LIMIT))]
    [SwaggerSubType(typeof(StopOrderDto), DiscriminatorValue = nameof(OrderTypeDto.STOP))]
    public abstract class CreateOrderDto
    {
        [JsonProperty("type")]
        [Required] 
        public abstract OrderTypeDto? Type { get; }

        [JsonProperty("asset_id")]
        [Required] 
        public int AssetId { get; set; }

        [JsonProperty("side")]
        [Required]
        public OrderSideDto? Side { get; set; }

        [JsonProperty("quantity")] 
        [Required] 
        public float Quantity { get; set; }
    }
}