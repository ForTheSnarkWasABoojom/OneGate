using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Order.Stop
{
    public class StopOrderModel : OrderModel
    {
        public override OrderType? Type { get; } = OrderType.STOP;

        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}