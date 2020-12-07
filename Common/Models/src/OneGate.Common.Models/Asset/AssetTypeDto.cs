using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Common.Models.Asset
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AssetTypeDto
    {
        STOCK,
        INDEX
    }
}