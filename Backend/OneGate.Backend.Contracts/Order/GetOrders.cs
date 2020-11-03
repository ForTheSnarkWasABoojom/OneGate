using MassTransit.Topology;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Contracts.Order
{
    [EntityName("request.order.get")]
    public class GetOrders
    {
        public OrderBaseFilterDto Filter { get; set; }
        public int OwnerId { get; set; }
    }
}