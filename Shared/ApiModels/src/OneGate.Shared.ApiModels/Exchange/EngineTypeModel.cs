using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiModels.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EngineTypeModel
    {
        FAKE
    }
}