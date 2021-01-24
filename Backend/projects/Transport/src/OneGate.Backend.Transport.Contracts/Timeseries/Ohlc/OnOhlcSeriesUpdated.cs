using System;
using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Series.Ohlc;

namespace OneGate.Backend.Transport.Contracts.Timeseries.Ohlc
{
    [EntityName("event.ohlc_series.update")]
    public class OnOhlcSeriesUpdated
    {
        public DateTime LastUpdate { get; set; }
        public int AssetId { get; set; }
        public Dictionary<IntervalDto, OhlcDto> Data { get; set; }
    }
}