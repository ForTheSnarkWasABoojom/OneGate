using AutoMapper;
using OneGate.Backend.Core.Assets.Api.Contracts.Asset;
using OneGate.Backend.Core.Assets.Api.Contracts.Exchange;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Series;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Core.Users.Api.Contracts.Order;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Limit;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Market;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Stop;
using OneGate.Backend.Core.Users.Api.Contracts.Portfolio;
using OneGate.Shared.ApiModels.User.Account;
using OneGate.Shared.ApiModels.User.Asset;
using OneGate.Shared.ApiModels.User.Exchange;
using OneGate.Shared.ApiModels.User.Order;
using OneGate.Shared.ApiModels.User.Order.Limit;
using OneGate.Shared.ApiModels.User.Order.Market;
using OneGate.Shared.ApiModels.User.Order.Stop;
using OneGate.Shared.ApiModels.User.Portfolio;
using OneGate.Shared.ApiModels.User.Timeseries;

namespace OneGate.Backend.Gateway.User.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMapForAccount();

            CreateMapForOrder();
            
            CreateMapForPortfolio();

            CreateMapForSeries();
            
            CreateMapForAsset();
            
            CreateMapForExchange();
        }

        private void CreateMapForSeries()
        {
            CreateMap<SeriesDto, Series>()
                .IncludeAllDerived();
            
            CreateMap<PointSeriesDto, PointSeries>();
            CreateMap<OhlcSeriesDto, OhlcSeries>();
            
            CreateMap<FilterSeriesRequest, FilterSeriesDto>();
        }

        private void CreateMapForAccount()
        {
            CreateMap<CreateAccountRequest, CreateAccountDto>();
            CreateMap<AccountDto, Account>();
        }
        
        private void CreateMapForOrder()
        {
            CreateMap<CreateOrderRequest, OrderDto>()
                .IncludeAllDerived();

            CreateMap<CreateMarketOrderRequest, MarketOrderDto>();
            CreateMap<CreateLimitOrderRequest, LimitOrderDto>();
            CreateMap<CreateStopOrderRequest, StopOrderDto>();

            CreateMap<OrderDto, Order>()
                .IncludeAllDerived();

            CreateMap<MarketOrderDto, MarketOrder>();
            CreateMap<LimitOrderDto, LimitOrder>();
            CreateMap<StopOrderDto, StopOrder>();
            
            CreateMap<FilterOrdersRequest, FilterOrdersDto>();
        }
        
        private void CreateMapForPortfolio()
        {
            CreateMap<CreatePortfolioRequest, PortfolioDto>();
            CreateMap<PortfolioDto, Portfolio>();

            CreateMap<FilterPortfoliosRequest, FilterPortfoliosDto>();
        }

        private void CreateMapForExchange()
        {
            CreateMap<ExchangeDto, Exchange>();
            
            CreateMap<FilterExchangesRequest, FilterExchangesDto>();
        }

        private void CreateMapForAsset()
        {
            CreateMap<AssetDto, Asset>();
            
            CreateMap<FilterAssetsRequest, FilterAssetsDto>();
        }
    }
}