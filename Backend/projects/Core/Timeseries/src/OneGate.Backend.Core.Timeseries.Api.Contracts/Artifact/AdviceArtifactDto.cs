namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Artifact
{
    public class AdviceArtifactDto : ArtifactDto
    {
        public override ArtifactTypeDto? Type => ArtifactTypeDto.ADVICE;
    }
}