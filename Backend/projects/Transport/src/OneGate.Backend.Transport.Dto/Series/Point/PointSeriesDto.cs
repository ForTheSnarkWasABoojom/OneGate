using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Series.Point
{
    public class PointSeriesDto
    {
        [JsonProperty("layout_id")] 
        public int LayoutId { get; set; }
        
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
        
        [JsonProperty("range")]
        public List<PointDto> Range { get; set; }
    }
}