using System;
using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Contracts.OhlcTimeseries
{
    [EntityName("event.ohlc_timeseries.update")]
    public class OnOhlcTimeseriesUpdated
    {
        public DateTime LastUpdate { get; set; }
        public int AssetId { get; set; }
        public Dictionary<OhlcIntervalDto, OhlcTimeseriesDto> Ohlcs { get; set; }
    }
}