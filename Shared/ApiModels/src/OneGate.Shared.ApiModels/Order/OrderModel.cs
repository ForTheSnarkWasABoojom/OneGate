using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Shared.ApiModels.Order
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(MarketOrderModel), OrderTypeModel.MARKET)]
    [JsonSubtypes.KnownSubType(typeof(LimitOrderModel), OrderTypeModel.LIMIT)]
    [JsonSubtypes.KnownSubType(typeof(StopOrderModel), OrderTypeModel.STOP)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(MarketOrderModel), DiscriminatorValue = nameof(OrderTypeModel.MARKET))]
    [SwaggerSubType(typeof(LimitOrderModel), DiscriminatorValue = nameof(OrderTypeModel.LIMIT))]
    [SwaggerSubType(typeof(StopOrderModel), DiscriminatorValue = nameof(OrderTypeModel.STOP))]
    public abstract class OrderModel
    {
        [JsonProperty("id")] 
        [Required] 
        public int Id { get; set; }

        [JsonProperty("type")]
        [Required] 
        public abstract OrderTypeModel? Type { get; }

        [JsonProperty("asset_id")]
        [Required] 
        public int AssetId { get; set; }

        [JsonProperty("state")] 
        [Required] 
        public OrderStateModel State { get; set; }
        
        [JsonProperty("side")]
        [Required]
        public OrderSideModel Side { get; set; }

        [JsonProperty("quantity")] 
        [Required] 
        public float Quantity { get; set; }
    }
}