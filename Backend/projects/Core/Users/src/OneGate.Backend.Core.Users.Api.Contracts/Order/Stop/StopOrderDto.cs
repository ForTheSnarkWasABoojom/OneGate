using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Api.Contracts.Order.Stop
{
    public class StopOrderDto : OrderDto
    {
        public override OrderTypeDto? Type { get; } = OrderTypeDto.STOP;

        [JsonProperty("price")]

        public float Price { get; set; }
    }
}