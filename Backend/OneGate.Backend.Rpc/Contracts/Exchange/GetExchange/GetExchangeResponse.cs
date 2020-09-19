using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Rpc.Contracts.Exchange.GetExchange
{
    public class GetExchangeResponse : SuccessResponse
    {
        public ExchangeDto Exchange { get; set; }
    }
}