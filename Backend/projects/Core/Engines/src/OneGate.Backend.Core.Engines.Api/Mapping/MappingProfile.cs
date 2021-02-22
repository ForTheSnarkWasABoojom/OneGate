using AutoMapper;
using OneGate.Backend.Core.Engines.Api.Contracts.AssetMapping;
using OneGate.Backend.Core.Engines.Database.Models;

namespace OneGate.Backend.Core.Engines.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAssetMappingDto, AssetMapping>();
            CreateMap<AssetMapping, AssetMappingDto>();
        }
    }
}