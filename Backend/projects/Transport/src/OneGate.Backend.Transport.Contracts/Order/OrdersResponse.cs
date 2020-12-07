using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Common.Models.Order;

namespace OneGate.Backend.Transport.Contracts.Order
{
    [EntityName("response.orders")]
    public class OrdersResponse
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}