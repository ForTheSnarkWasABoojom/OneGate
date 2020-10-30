using MassTransit.Topology;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Contracts.Exchange
{
    [EntityName("exchange.get_range")]
    public class GetExchangesRange
    {
        public ExchangeFilterDto Filter { get; set; }
    }
}