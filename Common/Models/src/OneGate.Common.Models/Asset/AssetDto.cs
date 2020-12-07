using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Common.Models.Asset
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(StockAssetDto), AssetTypeDto.STOCK)]
    [JsonSubtypes.KnownSubType(typeof(IndexAssetDto), AssetTypeDto.INDEX)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(StockAssetDto), DiscriminatorValue = nameof(AssetTypeDto.STOCK))]
    [SwaggerSubType(typeof(IndexAssetDto), DiscriminatorValue = nameof(AssetTypeDto.INDEX))]
    public abstract class AssetDto
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [Required]
        [JsonProperty("type")]
        public abstract AssetTypeDto? Type { get; }
        
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