using MassTransit.Topology;

namespace OneGate.Backend.Transport.Contracts.Order
{
    [EntityName("request.order.delete")]
    public class DeleteOrder
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
    }
}