using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Rpc.Contracts.Exchange.GetExchangesByFilter
{
    public class GetExchangesByFilterRequest
    {
        public ExchangeFilterDto Filter { get; set; }
    }
}