using JsonSubTypes;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Artifact
{
    [JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(PointArtifactDto), ArtifactTypeDto.POINT)]
    [JsonSubtypes.KnownSubType(typeof(AdviceArtifactDto), ArtifactTypeDto.ADVICE)]
    public abstract class ArtifactDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public abstract ArtifactTypeDto? Type { get; }
    }
}