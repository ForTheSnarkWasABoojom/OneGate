using MassTransit.Topology;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Contracts.Exchange
{
    [EntityName("request.exchange.get")]
    public class GetExchanges
    {
        public ExchangeFilterDto Filter { get; set; }
    }
}