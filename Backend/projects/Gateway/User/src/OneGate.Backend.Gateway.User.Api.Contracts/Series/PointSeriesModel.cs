using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Series
{
    public class PointSeriesModel : SeriesModel
    {
        public override SeriesType? Type => SeriesType.POINT;
        
        [JsonProperty("value")]
        public float Value { get; set; }
    }
}