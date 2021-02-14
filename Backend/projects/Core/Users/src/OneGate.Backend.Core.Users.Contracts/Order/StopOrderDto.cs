using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Order
{
    public class StopOrderDto: OrderDto
    {
        public override OrderTypeDto? Type { get; } = OrderTypeDto.STOP;

        [JsonProperty("price")] 
         
        public float Price { get; set; }
    }
}