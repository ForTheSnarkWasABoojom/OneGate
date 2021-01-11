using AutoMapper;
using OneGate.Backend.Core.Records.Database.Models;
using OneGate.Common.Models.Asset;
using OneGate.Common.Models.Exchange;
using OneGate.Common.Models.Layout;

namespace OneGate.Backend.Core.Records.Node
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