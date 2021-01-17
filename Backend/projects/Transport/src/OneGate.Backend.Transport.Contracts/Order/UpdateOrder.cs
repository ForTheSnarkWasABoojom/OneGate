using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Order;

namespace OneGate.Backend.Transport.Contracts.Order
{
    [EntityName("request.order.update")]
    public class UpdateOrder
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public OrderStateDto StateDto { get; set; }
    }
}