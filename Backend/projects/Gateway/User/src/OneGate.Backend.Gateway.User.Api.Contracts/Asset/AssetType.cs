using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Asset
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AssetType
    {
        STOCK,
        INDEX
    }
}