using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Order.Stop
{
    public class CreateStopOrderRequest : CreateOrderRequest
    {
        public override OrderType? Type => OrderType.STOP;

        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}