using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Timeseries
{
    public class PointSeries : Series
    {
        public override SeriesType? Type => SeriesType.POINT;
        
        [JsonProperty("value")]
        public float Value { get; set; }
    }
}