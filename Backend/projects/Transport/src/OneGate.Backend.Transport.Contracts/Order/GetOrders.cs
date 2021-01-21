using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Order;

namespace OneGate.Backend.Transport.Contracts.Order
{
    [EntityName("request.order.get")]
    public class GetOrders
    {
        public OrderFilterDto Filter { get; set; }
        public int OwnerId { get; set; }
    }
}