using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Order.Stop
{
    public class StopOrder : Order
    {
        public override OrderType? Type { get; } = OrderType.STOP;

        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}