using System.ComponentModel;
using Newtonsoft.Json;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Layer
{
    public class FilterLayersDto : FilterDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("asset_id")]
        public int? AssetId { get; set; }

        [JsonProperty("is_master")]
        [DefaultValue(true)]
        public bool? IsMaster { get; set; } = true;
    }
}