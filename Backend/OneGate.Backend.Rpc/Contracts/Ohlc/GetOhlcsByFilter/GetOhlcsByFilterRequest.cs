using OneGate.Shared.Models.Ohlc;

namespace OneGate.Backend.Rpc.Contracts.Ohlc.GetOhlcsByFilter
{
    public class GetOhlcsByFilterRequest
    {
        public OhlcFilterDto Filter { get; set; }
    }
}