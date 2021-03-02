using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EngineType
    {
        FAKE
    }
}