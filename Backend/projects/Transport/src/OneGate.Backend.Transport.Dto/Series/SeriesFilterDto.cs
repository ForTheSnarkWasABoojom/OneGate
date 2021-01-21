using System;
using Newtonsoft.Json;
using OneGate.Backend.Transport.Dto.Common;

namespace OneGate.Backend.Transport.Dto.Series
{
    public class SeriesFilterDto : FilterDto
    {
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
        
        [JsonProperty("end_timestamp")]
        public DateTime? EndTimestamp { get; set; }
        
        [JsonProperty("start_timestamp")]
        public DateTime? StartTimestamp { get; set; }
    }
}