using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Api.Contracts.Order.Limit
{
    public class LimitOrderDto : OrderDto
    {
        public override OrderTypeDto? Type => OrderTypeDto.LIMIT;

        [JsonProperty("price")]

        public float Price { get; set; }
    }
}