using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Exchange;

namespace OneGate.Backend.Transport.Contracts.Exchange
{
    [EntityName("request.exchange.create")]
    public class CreateExchange
    {
        public CreateExchangeDto Exchange { get; set; }
    }
}