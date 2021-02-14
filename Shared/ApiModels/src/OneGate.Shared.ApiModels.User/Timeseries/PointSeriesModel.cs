using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Timeseries
{
    public class PointSeriesModel : SeriesModel
    {
        public override SeriesTypeModel? Type => SeriesTypeModel.POINT;
        
        [JsonProperty("value")]
        public float Value { get; set; }
    }
}