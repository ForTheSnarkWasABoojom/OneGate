using AutoMapper;
using OneGate.Backend.Core.Assets.Api.Contracts.Asset;
using OneGate.Backend.Core.Assets.Api.Contracts.Exchange;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Core.Users.Api.Contracts.Order;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Limit;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Market;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Stop;
using OneGate.Backend.Core.Users.Api.Contracts.Portfolio;
using OneGate.Backend.Gateway.User.Api.Contracts.Account;
using OneGate.Backend.Gateway.User.Api.Contracts.Asset;
using OneGate.Backend.Gateway.User.Api.Contracts.Exchange;
using OneGate.Backend.Gateway.User.Api.Contracts.Order;
using OneGate.Backend.Gateway.User.Api.Contracts.Order.Limit;
using OneGate.Backend.Gateway.User.Api.Contracts.Order.Market;
using OneGate.Backend.Gateway.User.Api.Contracts.Order.Stop;
using OneGate.Backend.Gateway.User.Api.Contracts.Portfolio;

namespace OneGate.Backend.Gateway.User.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMapForAccount();

            CreateMapForOrder();
            
            CreateMapForPortfolio();

            CreateMapForAsset();
            
            CreateMapForExchange();
        }

        private void CreateMapForAccount()
        {
            CreateMap<CreateAccountRequest, CreateAccountDto>();
            CreateMap<AccountDto, AccountModel>();
        }
        
        private void CreateMapForOrder()
        {
            CreateMap<CreateOrderRequest, OrderDto>()
                .IncludeAllDerived();

            CreateMap<CreateMarketOrderRequest, MarketOrderDto>();
            CreateMap<CreateLimitOrderRequest, LimitOrderDto>();
            CreateMap<CreateStopOrderRequest, StopOrderDto>();

            CreateMap<OrderDto, OrderModel>()
                .IncludeAllDerived();

            CreateMap<MarketOrderDto, MarketOrderModel>();
            CreateMap<LimitOrderDto, LimitOrderModel>();
            CreateMap<StopOrderDto, StopOrderModel>();
            
            CreateMap<FilterOrdersRequest, FilterOrdersDto>();
        }
        
        private void CreateMapForPortfolio()
        {
            CreateMap<CreatePortfolioRequest, PortfolioDto>();
            CreateMap<PortfolioDto, PortfolioModel>();

            CreateMap<FilterPortfoliosRequest, FilterPortfoliosDto>();
        }

        private void CreateMapForExchange()
        {
            CreateMap<ExchangeDto, ExchangeModel>();
            
            CreateMap<FilterExchangesRequest, FilterExchangesDto>();
        }

        private void CreateMapForAsset()
        {
            CreateMap<AssetDto, AssetModel>();
            
            CreateMap<FilterAssetsRequest, FilterAssetsDto>();
        }
    }
}