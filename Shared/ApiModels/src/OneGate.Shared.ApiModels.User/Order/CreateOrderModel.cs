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
    [JsonSubtypes.KnownSubType(typeof(CreateMarketOrderModel), OrderTypeModel.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(CreateLimitOrderModel), OrderTypeModel.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(CreateStopOrderModel), OrderTypeModel.STOP)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(MarketOrderModel), DiscriminatorValue = nameof(OrderTypeModel.MARKET))]
    [SwaggerSubType(typeof(LimitOrderModel), DiscriminatorValue = nameof(OrderTypeModel.LIMIT))]
    [SwaggerSubType(typeof(StopOrderModel), DiscriminatorValue = nameof(OrderTypeModel.STOP))]
    public abstract class CreateOrderModel
    {
        [JsonProperty("type")]
        [Required] 
        public abstract OrderTypeModel? Type { get; }

        [JsonProperty("asset_id")]
        [Required] 
        public int AssetId { get; set; }

        [JsonProperty("side")]
        [Required]
        public OrderSideModel? Side { get; set; }

        [JsonProperty("quantity")] 
        [Required] 
        public float Quantity { get; set; }
    }
}