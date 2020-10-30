using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Exchange
{
    [EntityName("exchange.delete")]
    public class DeleteExchange
    {
        public int Id { get; set; }
    }
}