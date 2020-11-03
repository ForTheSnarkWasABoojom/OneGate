using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Contracts.Order
{
    [EntityName("response.orders")]
    public class OrdersResponse
    {
        public IEnumerable<OrderBaseDto> Orders { get; set; }
    }
}