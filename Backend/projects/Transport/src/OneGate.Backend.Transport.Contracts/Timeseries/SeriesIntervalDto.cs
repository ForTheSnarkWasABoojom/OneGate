using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Transport.Contracts.Timeseries
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SeriesIntervalDto
    {
        m1 = 1
    }
}