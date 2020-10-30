using MassTransit.Topology;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Contracts.Order
{
    [EntityName("order.get_range")]
    public class GetOrdersRange
    {
        public OrderBaseFilterDto Filter { get; set; }
        public int OwnerId { get; set; }
    }
}