using System.ComponentModel.DataAnnotations;
using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Shared.Models.Asset
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(CreateStockAssetDto), AssetTypeDto.STOCK)]
    [JsonSubtypes.KnownSubType(typeof(CreateIndexAssetDto), AssetTypeDto.INDEX)]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(CreateStockAssetDto), DiscriminatorValue = nameof(AssetTypeDto.STOCK))]
    [SwaggerSubType(typeof(CreateIndexAssetDto), DiscriminatorValue = nameof(AssetTypeDto.INDEX))]
    public abstract class CreateAssetBaseDto
    {
        [Required]
        [JsonProperty("type")]
        public abstract AssetTypeDto Type { get; }

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