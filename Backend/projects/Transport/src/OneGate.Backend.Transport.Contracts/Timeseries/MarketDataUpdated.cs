using System.Collections.Generic;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts.Timeseries
{
    [EntityName("event.market_data_updated")]
    public class MarketDataUpdated : BaseEvent
    {
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
        
        [JsonProperty("ohlc")]
        public List<OhlcSeriesDto> Ohlc { get; set; }
    }
}