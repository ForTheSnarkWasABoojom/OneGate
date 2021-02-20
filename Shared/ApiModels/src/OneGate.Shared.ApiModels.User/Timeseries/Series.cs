using System;
using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Shared.ApiModels.User.Timeseries
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(OhlcSeries), SeriesType.OHLC)]
    [JsonSubtypes.KnownSubType(typeof(PointSeries), SeriesType.POINT)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(OhlcSeries), DiscriminatorValue = nameof(SeriesType.OHLC))]
    [SwaggerSubType(typeof(PointSeries), DiscriminatorValue = nameof(SeriesType.POINT))]
    public abstract class Series
    {
        [Required]
        [JsonProperty("type")]
        public abstract SeriesType? Type { get; }
        
        [JsonProperty("layout_id")]
        public int LayoutId { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}