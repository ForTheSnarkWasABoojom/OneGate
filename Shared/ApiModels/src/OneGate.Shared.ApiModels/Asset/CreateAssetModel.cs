using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Shared.ApiModels.Asset
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(CreateStockAssetModel), AssetTypeModel.STOCK)]
    [JsonSubtypes.KnownSubType(typeof(CreateIndexAssetModel), AssetTypeModel.INDEX)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(CreateStockAssetModel), DiscriminatorValue = nameof(AssetTypeModel.STOCK))]
    [SwaggerSubType(typeof(CreateIndexAssetModel), DiscriminatorValue = nameof(AssetTypeModel.INDEX))]
    public abstract class CreateAssetModel
    {
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

        [MaxLength(500)]
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}