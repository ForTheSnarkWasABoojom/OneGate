using MassTransit.Topology;

namespace OneGate.Backend.Transport.Contracts.Exchange
{
    [EntityName("request.exchange.delete")]
    public class DeleteExchange
    {
        public int Id { get; set; }
    }
}