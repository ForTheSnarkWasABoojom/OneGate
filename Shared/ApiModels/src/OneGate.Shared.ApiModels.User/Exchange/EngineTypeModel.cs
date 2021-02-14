using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiModels.User.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EngineTypeModel
    {
        FAKE
    }
}