using System;
using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Shared.ApiModels.User.Timeseries
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(OhlcSeriesModel), SeriesTypeModel.OHLC)]
    [JsonSubtypes.KnownSubType(typeof(PointSeriesModel), SeriesTypeModel.POINT)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(OhlcSeriesModel), DiscriminatorValue = nameof(SeriesTypeModel.OHLC))]
    [SwaggerSubType(typeof(PointSeriesModel), DiscriminatorValue = nameof(SeriesTypeModel.POINT))]
    public abstract class SeriesModel
    {
        [Required]
        [JsonProperty("type")]
        public abstract SeriesTypeModel? Type { get; }
        
        [JsonProperty("layout_id")]
        public int LayoutId { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}