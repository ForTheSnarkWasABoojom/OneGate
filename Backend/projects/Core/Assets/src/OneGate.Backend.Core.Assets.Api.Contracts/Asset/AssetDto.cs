using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Assets.Api.Contracts.Asset
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
        
        [MaxLength(50)]
        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        [MaxLength(500)]
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}