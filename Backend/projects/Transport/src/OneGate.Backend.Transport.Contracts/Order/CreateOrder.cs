using MassTransit.Topology;
using OneGate.Common.Models.Order;

namespace OneGate.Backend.Transport.Contracts.Order
{
    [EntityName("request.order.create")]
    public class CreateOrder
    {
        public CreateOrderDto Order { get; set; }
        public int OwnerId { get; set; }
    }
}