using MassTransit.Topology;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Contracts.Order
{
    [EntityName("request.order.get")]
    public class GetOrders
    {
        public OrderFilterDto Filter { get; set; }
        public int OwnerId { get; set; }
    }
}