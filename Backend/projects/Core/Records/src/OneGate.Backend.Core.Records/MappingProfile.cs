using AutoMapper;
using OneGate.Backend.Core.Records.Database.Models;
using OneGate.Backend.Transport.Dto.Asset;
using OneGate.Backend.Transport.Dto.Exchange;
using OneGate.Backend.Transport.Dto.Layout;

namespace OneGate.Backend.Core.Records
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateExchangeDto, Exchange>();
            CreateMap<Exchange, ExchangeDto>();
            CreateMap<ExchangeDto, Exchange>();
            
            CreateMap<CreateLayoutDto, Layout>();
            CreateMap<LayoutDto, Layout>();
            CreateMap<Layout, LayoutDto>();

            CreateMap<CreateAssetDto, Asset>().IncludeAllDerived();
            CreateMap<CreateIndexAssetDto, IndexAsset>();
            CreateMap<CreateStockAssetDto, StockAsset>();

            CreateMap<Asset, AssetDto>().IncludeAllDerived();
            CreateMap<IndexAsset, IndexAssetDto>();
            CreateMap<StockAsset, StockAssetDto>();
        }
    }
}