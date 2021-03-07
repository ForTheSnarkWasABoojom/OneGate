using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Artifact
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ArtifactTypeDto
    {
        POINT,
        ADVICE
    }
}