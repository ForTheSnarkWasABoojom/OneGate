using System;
using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Series
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(OhlcSeriesModel), SeriesType.OHLC)]
    [JsonSubtypes.KnownSubType(typeof(PointSeriesModel), SeriesType.POINT)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(OhlcSeriesModel), DiscriminatorValue = nameof(SeriesType.OHLC))]
    [SwaggerSubType(typeof(PointSeriesModel), DiscriminatorValue = nameof(SeriesType.POINT))]
    public abstract class SeriesModel
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