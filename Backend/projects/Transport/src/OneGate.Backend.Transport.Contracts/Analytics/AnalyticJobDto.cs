using System;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts.Analytics
{
    public class AnalyticJobDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("provider")]
        public Guid Provider { get; set; }
        
        [JsonProperty("metric")]
        public string Metric { get; set; }
        
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }

        [JsonProperty("options")]
        public string Options { get; set; }
    }
}