using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.Models.Series.Ohlc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IntervalDto
    {
        m1,
        m5,
        m15,
        m30,
        H1,
        H4,
        D1,
        M1
    }
}