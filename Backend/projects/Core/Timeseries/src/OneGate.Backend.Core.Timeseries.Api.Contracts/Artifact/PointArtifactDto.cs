namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Artifact
{
    public class PointArtifactDto : ArtifactDto
    {
        public override ArtifactTypeDto? Type => ArtifactTypeDto.POINT;
    }
}