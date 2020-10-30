using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Order
{
    [EntityName("order.get")]
    public class GetOrder
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
    }
}