using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Series.Ohlc
{
    public class OhlcSeriesDto
    {
        [Required]
        [JsonProperty("interval")]
        public IntervalDto Interval { get; set; }

        [Required]
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
        
        [Required]
        [JsonProperty("range")]
        public List<OhlcDto> Range { get; set; }
    }
}