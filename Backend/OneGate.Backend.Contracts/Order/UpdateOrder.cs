using MassTransit.Topology;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Contracts.Order
{
    [EntityName("order.update")]
    public class UpdateOrder
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public OrderStateDto StateDto { get; set; }
    }
}