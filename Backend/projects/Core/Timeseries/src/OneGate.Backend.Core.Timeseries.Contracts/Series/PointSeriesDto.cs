using Newtonsoft.Json;

namespace OneGate.Backend.Core.Timeseries.Contracts.Series
{
    public class PointSeriesDto : SeriesDto
    {
        public override SeriesTypeDto? Type => SeriesTypeDto.POINT;
        
        [JsonProperty("value")]
        public float Value { get; set; }
    }
}