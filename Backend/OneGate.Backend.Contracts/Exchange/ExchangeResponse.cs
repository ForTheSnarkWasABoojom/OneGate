using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Contracts.Exchange
{
    public class ExchangeResponse : SuccessResponse
    {
        public ExchangeDto Exchange { get; set; }
    }
}