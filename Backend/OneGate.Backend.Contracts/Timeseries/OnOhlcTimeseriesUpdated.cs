using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Backend.Contracts.Common;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.Timeseries
{
    [EntityName("ohlc_timeseries.event.update")]
    public class OnOhlcTimeseriesUpdated : EventBase
    {
        public int AssetId { get; set; }
        public Dictionary<OhlcIntervalDto, OhlcTimeseriesDto> Ohlcs { get; set; }
    }
}