using System.Collections.Generic;
using OneGate.Shared.Models.Ohlc;

namespace OneGate.Backend.Rpc.Contracts.Ohlc.CreateOhlcs
{
    public class CreateOhlcsRequest
    {
        public CreateOhlcRangeDto OhlcRange { get; set; }
    }
}