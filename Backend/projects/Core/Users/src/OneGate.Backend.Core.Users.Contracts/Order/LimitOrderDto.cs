using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Order
{
    public class LimitOrderDto: OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.LIMIT;
        
        [JsonProperty("price")] 
         
        public float Price { get; set; }
    }
}