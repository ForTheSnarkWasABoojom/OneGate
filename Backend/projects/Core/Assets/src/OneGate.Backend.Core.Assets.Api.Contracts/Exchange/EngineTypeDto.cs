using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Core.Assets.Api.Contracts.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EngineTypeDto
    {
        FAKE
    }
}