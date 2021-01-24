using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Timeseries.Ohlc
{
    public class OhlcSeriesModel
    {
        [Required]
        [JsonProperty("interval")]
        public IntervalModel Interval { get; set; }

        [Required]
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
        
        [Required]
        [JsonProperty("range")]
        public List<OhlcModel> Range { get; set; }
    }
}