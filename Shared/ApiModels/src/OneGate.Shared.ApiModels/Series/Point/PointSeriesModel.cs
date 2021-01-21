using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Series.Point
{
    public class PointSeriesModel
    {
        [Required] 
        [JsonProperty("layout_id")] 
        public int LayoutId { get; set; }

        [Required]
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
        
        [Required]
        [JsonProperty("range")]
        public List<PointModel> Range { get; set; }
    }
}