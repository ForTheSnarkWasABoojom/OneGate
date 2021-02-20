using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Order.Limit
{
    public class CreateLimitOrderRequest : CreateOrderRequest
    {
        public override OrderType? Type => OrderType.LIMIT;
        
        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}