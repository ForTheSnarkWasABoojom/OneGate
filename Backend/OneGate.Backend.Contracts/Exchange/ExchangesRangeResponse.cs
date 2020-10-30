using System.Collections.Generic;
using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Contracts.Exchange
{
    public class ExchangesRangeResponse : SuccessResponse
    {
        public IEnumerable<ExchangeDto> Exchanges { get; set; }
    }
}