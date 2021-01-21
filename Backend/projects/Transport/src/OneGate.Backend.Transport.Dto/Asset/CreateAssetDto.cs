using JsonSubTypes;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Asset
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(CreateStockAssetDto), AssetTypeDto.STOCK)]
    [JsonSubtypes.KnownSubType(typeof(CreateIndexAssetDto), AssetTypeDto.INDEX)]
    public abstract class CreateAssetDto
    {
        [JsonProperty("type")]
        public abstract AssetTypeDto? Type { get; }
        
        [JsonProperty("exchange_id")]
        public int ExchangeId { get; set; }

        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}