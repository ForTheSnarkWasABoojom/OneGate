using MassTransit.Topology;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Contracts.Exchange
{
    [EntityName("request.exchange.create")]
    public class CreateExchange
    {
        public CreateExchangeDto Exchange { get; set; }
    }
}