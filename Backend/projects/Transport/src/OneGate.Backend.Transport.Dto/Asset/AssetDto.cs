using JsonSubTypes;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Asset
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(StockAssetDto), AssetTypeDto.STOCK)]
    [JsonSubtypes.KnownSubType(typeof(IndexAssetDto), AssetTypeDto.INDEX)]
    public abstract class AssetDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
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