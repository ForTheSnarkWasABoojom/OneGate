using AutoMapper;
using OneGate.Backend.Core.Assets.Contracts.Asset;
using OneGate.Backend.Core.Assets.Contracts.Exchange;
using OneGate.Backend.Core.Assets.Contracts.Layer;
using OneGate.Backend.Core.Assets.Database.Models;

namespace OneGate.Backend.Core.Assets.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AssetDto, Asset>()
                .IncludeAllDerived();
            
            CreateMap<IndexAssetDto, IndexAsset>();
            CreateMap<StockAssetDto, StockAsset>();

            CreateMap<Asset, AssetDto>()
                .IncludeAllDerived();
            
            CreateMap<IndexAsset, IndexAssetDto>();
            CreateMap<StockAsset, StockAssetDto>();
            
            CreateMap<ExchangeDto, Exchange>();
            CreateMap<Exchange, ExchangeDto>();
            
            CreateMap<LayersDto, Layer>();
            CreateMap<Layer, LayersDto>();
        }
    }
}