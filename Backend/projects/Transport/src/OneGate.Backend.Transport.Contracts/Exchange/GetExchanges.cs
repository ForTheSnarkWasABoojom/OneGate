using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Exchange;

namespace OneGate.Backend.Transport.Contracts.Exchange
{
    [EntityName("request.exchange.get")]
    public class GetExchanges
    {
        public ExchangeFilterDto Filter { get; set; }
    }
}