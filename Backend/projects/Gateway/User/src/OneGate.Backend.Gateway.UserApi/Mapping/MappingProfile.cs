using AutoMapper;
using OneGate.Backend.Core.Assets.Contracts.Asset;
using OneGate.Backend.Core.Assets.Contracts.Exchange;
using OneGate.Backend.Core.Assets.Contracts.Layer;
using OneGate.Backend.Core.Timeseries.Contracts;
using OneGate.Backend.Core.Timeseries.Contracts.Series;
using OneGate.Backend.Core.Users.Contracts.Account;
using OneGate.Backend.Core.Users.Contracts.Order;
using OneGate.Backend.Core.Users.Contracts.Portfolio;
using OneGate.Shared.ApiModels.User.Account;
using OneGate.Shared.ApiModels.User.Asset;
using OneGate.Shared.ApiModels.User.Exchange;
using OneGate.Shared.ApiModels.User.Layout;
using OneGate.Shared.ApiModels.User.Order;
using OneGate.Shared.ApiModels.User.Order.Limit;
using OneGate.Shared.ApiModels.User.Order.Market;
using OneGate.Shared.ApiModels.User.Order.Stop;
using OneGate.Shared.ApiModels.User.Portfolio;
using OneGate.Shared.ApiModels.User.Timeseries;

namespace OneGate.Backend.Gateway.UserApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAccountModel, AccountDto>();
            CreateMap<AccountDto, AccountModel>();

            CreateMap<CreateOrderModel, OrderDto>()
                .IncludeAllDerived();

            CreateMap<CreateMarketOrderModel, MarketOrderDto>();
            CreateMap<CreateLimitOrderModel, LimitOrderDto>();
            CreateMap<CreateStopOrderModel, StopOrderDto>();

            CreateMap<OrderDto, OrderModel>()
                .IncludeAllDerived();

            CreateMap<MarketOrderDto, MarketOrderModel>();
            CreateMap<LimitOrderDto, LimitOrderModel>();
            CreateMap<StopOrderDto, StopOrderModel>();
                
            CreateMap<CreatePortfolioModel, PortfolioDto>();
            CreateMap<PortfolioDto, PortfolioModel>();

            CreateMap<AssetDto, AssetModel>();
            CreateMap<ExchangeDto, ExchangeModel>();
            CreateMap<LayersDto, LayoutModel>();

            CreateMap<SeriesDto, SeriesModel>()
                .IncludeAllDerived();
            
            CreateMap<PointSeriesDto, PointSeriesModel>();
            CreateMap<OhlcSeriesDto, OhlcSeriesModel>();
        }
    }
}