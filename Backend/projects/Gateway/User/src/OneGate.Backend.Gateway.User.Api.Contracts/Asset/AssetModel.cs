using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Asset
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(StockAssetModel), AssetType.STOCK)]
    [JsonSubtypes.KnownSubType(typeof(IndexAssetModel), AssetType.INDEX)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(StockAssetModel), DiscriminatorValue = nameof(AssetType.STOCK))]
    [SwaggerSubType(typeof(IndexAssetModel), DiscriminatorValue = nameof(AssetType.INDEX))]
    public abstract class AssetModel
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [Required]
        [JsonProperty("type")]
        public abstract AssetType? Type { get; }
        
        [Required]
        [JsonProperty("exchange_id")]
        public int ExchangeId { get; set; }
        
        [Required]
        [MaxLength(50)]
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
        
        [Required]
        [MaxLength(500)]
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}