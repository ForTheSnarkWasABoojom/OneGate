using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.ApiContracts.Order
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderSideDto
    {
        BUY,
        SELL
    }
}