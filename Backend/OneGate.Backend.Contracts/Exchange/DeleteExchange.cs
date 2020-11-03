using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Exchange
{
    [EntityName("request.exchange.delete")]
    public class DeleteExchange
    {
        public int Id { get; set; }
    }
}