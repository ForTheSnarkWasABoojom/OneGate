using System.Collections.Generic;
using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Ohlc;

namespace OneGate.Backend.Rpc.Contracts.Ohlc.GetOhlcsByFilter
{
    public class GetOhlcsByFilterResponse : SuccessResponse
    {
        public OhlcRangeDto OhlcRange { get; set; }
    }
}