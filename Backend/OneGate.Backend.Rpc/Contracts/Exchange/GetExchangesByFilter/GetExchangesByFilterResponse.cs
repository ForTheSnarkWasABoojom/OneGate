using System.Collections.Generic;
using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Rpc.Contracts.Exchange.GetExchangesByFilter
{
    public class GetExchangesByFilterResponse : SuccessResponse
    {
        public List<ExchangeDto> Exchanges { get; set; }
    }
}