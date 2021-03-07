using Newtonsoft.Json;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Layer
{
    public class LayerDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
        
        [JsonProperty("asset_id")]
        public int AssetId { get; set; }
        
        [JsonProperty("interval")]
        public string Interval { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}