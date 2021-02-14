using System;
using JsonSubTypes;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Timeseries.Contracts.Series
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(OhlcSeriesDto), SeriesTypeDto.OHLC)]
    [JsonSubtypes.KnownSubType(typeof(PointSeriesDto), SeriesTypeDto.POINT)]
    public abstract class SeriesDto
    {
        [JsonProperty("type")]
        public abstract SeriesTypeDto? Type { get; }
        
        [JsonProperty("layout_id")]
        public int LayoutId { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}