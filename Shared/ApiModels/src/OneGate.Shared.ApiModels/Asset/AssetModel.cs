using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using OneGate.Shared.ApiModels.Asset.Index;
using OneGate.Shared.ApiModels.Asset.Stock;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Shared.ApiModels.Asset
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(StockAssetModel), AssetTypeModel.STOCK)]
    [JsonSubtypes.KnownSubType(typeof(IndexAssetModel), AssetTypeModel.INDEX)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(StockAssetModel), DiscriminatorValue = nameof(AssetTypeModel.STOCK))]
    [SwaggerSubType(typeof(IndexAssetModel), DiscriminatorValue = nameof(AssetTypeModel.INDEX))]
    public abstract class AssetModel
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [Required]
        [JsonProperty("type")]
        public abstract AssetTypeModel? Type { get; }
        
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