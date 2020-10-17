using System.Collections.Generic;
using OneGate.Backend.Rpc.Contracts.Base;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Rpc.Contracts.Timeseries
{
    public class OnOhlcTimeseriesChanged : EventBase
    {
        public int AssetId { get; set; }
        public Dictionary<OhlcIntervalDto, OhlcTimeseriesDto> OhlcByInterval { get; set; }
    }
}