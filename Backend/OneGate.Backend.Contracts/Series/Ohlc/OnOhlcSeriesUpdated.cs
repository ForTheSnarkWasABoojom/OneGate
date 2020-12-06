using System;
using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.Series.Ohlc;

namespace OneGate.Backend.Contracts.Series.Ohlc
{
    [EntityName("event.ohlc_series.update")]
    public class OnOhlcSeriesUpdated
    {
        public DateTime LastUpdate { get; set; }
        public int AssetId { get; set; }
        public Dictionary<IntervalDto, OhlcDto> Data { get; set; }
    }
}