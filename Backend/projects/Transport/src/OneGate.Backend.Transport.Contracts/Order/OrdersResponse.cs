using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Order;

namespace OneGate.Backend.Transport.Contracts.Order
{
    [EntityName("response.orders")]
    public class OrdersResponse
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}