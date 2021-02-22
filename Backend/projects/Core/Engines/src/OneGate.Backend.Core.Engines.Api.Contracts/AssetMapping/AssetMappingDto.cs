using Newtonsoft.Json;

namespace OneGate.Backend.Core.Engines.Api.Contracts.AssetMapping
{
    public class AssetMappingDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("engine_id")]
        public int EngineId { get; set; }

        [JsonProperty("asset_id")]
        public int AssetId { get; set; }

        [JsonProperty("original_symbol")]
        public string OriginalSymbol { get; set; }
    }
}