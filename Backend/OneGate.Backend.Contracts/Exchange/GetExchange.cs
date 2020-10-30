using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Exchange
{
    [EntityName("exchange.get")]
    public class GetExchange
    {
        public int Id { get; set; }
    }
}