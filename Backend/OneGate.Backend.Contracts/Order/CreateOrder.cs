using MassTransit.Topology;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Contracts.Order
{
    [EntityName("request.order.create")]
    public class CreateOrder
    {
        public CreateOrderBaseDto Order { get; set; }
        public int OwnerId { get; set; }
    }
}