using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Order;

namespace OneGate.Backend.Transport.Contracts.Order
{
    [EntityName("request.order.get")]
    public class GetOrders
    {
        public OrderFilterDto Filter { get; set; }
        public int OwnerId { get; set; }
    }
}