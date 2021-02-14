using System.Collections.Generic;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Order
{
    [EntityName("response.orders")]
    public class OrdersResponse
    {
        [JsonProperty("orders")]
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}