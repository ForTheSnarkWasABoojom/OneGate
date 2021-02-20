using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Order.Limit
{
    public class LimitOrder : Order
    {
        public override OrderType? Type => OrderType.LIMIT;
        
        [JsonProperty("price")] 
        [Required] 
        public float Price { get; set; }
    }
}