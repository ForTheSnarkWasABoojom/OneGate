using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Exchange;

namespace OneGate.Backend.Transport.Contracts.Exchange
{
    [EntityName("request.exchange.create")]
    public class CreateExchange
    {
        public CreateExchangeDto Exchange { get; set; }
    }
}