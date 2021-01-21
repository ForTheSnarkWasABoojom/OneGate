using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Series.Point
{
    public class PointSeriesFilterDto : SeriesFilterDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("layout_id")]
        public int LayoutId { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}