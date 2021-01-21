using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Exchange;

namespace OneGate.Backend.Transport.Contracts.Exchange
{
    [EntityName("request.exchange.get")]
    public class GetExchanges
    {
        public ExchangeFilterDto Filter { get; set; }
    }
}