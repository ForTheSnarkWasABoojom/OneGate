using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiModels.User.Asset
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AssetTypeModel
    {
        STOCK,
        INDEX
    }
}