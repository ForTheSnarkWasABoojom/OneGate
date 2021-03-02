using AutoMapper;
using OneGate.Backend.Core.Assets.Api.Contracts.Asset;
using OneGate.Backend.Core.Assets.Api.Contracts.Exchange;
using OneGate.Backend.Core.Assets.Database.Models;

namespace OneGate.Backend.Core.Assets.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMapForAsset();

            CreateMapForExchange();
        }

        private void CreateMapForExchange()
        {
            CreateMap<Exchange, ExchangeDto>();
        }

        private void CreateMapForAsset()
        {
            CreateMap<Asset, AssetDto>()
                .IncludeAllDerived();
            
            CreateMap<IndexAsset, IndexAssetDto>();
            CreateMap<StockAsset, StockAssetDto>();
        }
    }
}