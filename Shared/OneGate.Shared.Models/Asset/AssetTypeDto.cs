using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.Models.Asset
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AssetTypeDto
    {
        STOCK,
        INDEX
    }
}