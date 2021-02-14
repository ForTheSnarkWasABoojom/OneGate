using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Order
{
    [EntityName("request.order.create")]
    public class CreateOrder
    {
        [JsonProperty("order")]
        public OrderDto Order { get; set; }
    }
}