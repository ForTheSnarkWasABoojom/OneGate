using OneGate.Shared.Models.Ohlc;

namespace OneGate.Backend.Rpc.Contracts.Ohlc.DeleteOhlcs
{
    public class DeleteOhlcsRequest
    {
        public OhlcFilterDto Filter { get; set; }
    }
}