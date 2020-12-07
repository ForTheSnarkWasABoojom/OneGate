using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Common.Models.Order
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderSideDto
    {
        BUY,
        SELL
    }
}