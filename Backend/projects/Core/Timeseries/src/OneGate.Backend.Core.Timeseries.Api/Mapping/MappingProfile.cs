using AutoMapper;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Artifact;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Layer;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Ohlc;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMapForOhlc();
            CreateMapForLayer();
            CreateMapForArtifact();
        }

        private void CreateMapForLayer()
        {
            CreateMap<Layer, LayerDto>();
        }

        private void CreateMapForOhlc()
        {
            CreateMap<Ohlc, OhlcDto>();
        }
        
        private void CreateMapForArtifact()
        {
            CreateMap<Artifact, ArtifactDto>()
                .IncludeAllDerived();
            
            CreateMap<PointArtifact, PointArtifactDto>();
            CreateMap<AdviceArtifact, PointArtifactDto>();
        }
    }
}