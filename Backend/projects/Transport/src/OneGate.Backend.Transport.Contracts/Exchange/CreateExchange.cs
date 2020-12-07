using MassTransit.Topology;
using OneGate.Common.Models.Exchange;

namespace OneGate.Backend.Transport.Contracts.Exchange
{
    [EntityName("request.exchange.create")]
    public class CreateExchange
    {
        public CreateExchangeDto Exchange { get; set; }
    }
}