using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Shared.Models.Timeseries
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OhlcIntervalDto
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