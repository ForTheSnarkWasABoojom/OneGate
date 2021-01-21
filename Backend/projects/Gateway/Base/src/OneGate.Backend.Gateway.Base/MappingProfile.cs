using System.Collections.Generic;
using AutoMapper;
using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Transport.Dto.Asset;
using OneGate.Backend.Transport.Dto.Exchange;
using OneGate.Backend.Transport.Dto.Layout;
using OneGate.Backend.Transport.Dto.Order;
using OneGate.Backend.Transport.Dto.Portfolio;
using OneGate.Backend.Transport.Dto.Series.Ohlc;
using OneGate.Backend.Transport.Dto.Series.Point;
using OneGate.Shared.ApiModels.Account;
using OneGate.Shared.ApiModels.Asset;
using OneGate.Shared.ApiModels.Exchange;
using OneGate.Shared.ApiModels.Layout;
using OneGate.Shared.ApiModels.Order;
using OneGate.Shared.ApiModels.Portfolio;
using OneGate.Shared.ApiModels.Series.Ohlc;
using OneGate.Shared.ApiModels.Series.Point;

namespace OneGate.Backend.Gateway.Base
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Account

            CreateMap<AccountFilterModel, AccountFilterDto>();
            CreateMap<IEnumerable<AccountDto>, IEnumerable<AccountModel>>();
            CreateMap<AccountDto, AccountModel>();
            CreateMap<CreateAccountModel, CreateAccountDto>();

            CreateMap<AccountDto, AccountModel>();
            CreateMap<AccountModel, AccountDto>();

            CreateMap<CreateAccountDto, AccountModel>();
            CreateMap<AccountModel, CreateAccountDto>();

            #endregion

            #region Assets

            CreateMap<CreateAssetModel, CreateAssetDto>();
            CreateMap<AssetFilterModel, AssetFilterDto>();
            CreateMap<IEnumerable<AssetDto>, IEnumerable<AssetModel>>();
            CreateMap<AssetDto, AssetModel>();

            CreateMap<CreateAssetDto, AssetModel>().IncludeAllDerived();
            CreateMap<CreateIndexAssetDto, IndexAssetModel>();
            CreateMap<CreateStockAssetDto, StockAssetModel>();

            CreateMap<AssetModel, AssetDto>().IncludeAllDerived();
            CreateMap<IndexAssetModel, IndexAssetDto>();
            CreateMap<StockAssetModel, StockAssetDto>();

            #endregion

            #region Exchange

            CreateMap<CreateExchangeModel, CreateExchangeDto>();
            CreateMap<ExchangeFilterModel, ExchangeFilterDto>();
            CreateMap<IEnumerable<ExchangeDto>, IEnumerable<ExchangeModel>>();

            CreateMap<CreateExchangeDto, ExchangeModel>();
            CreateMap<ExchangeModel, ExchangeDto>();
            CreateMap<ExchangeDto, ExchangeModel>();

            #endregion

            #region Layouts

            CreateMap<LayoutFilterModel, LayoutFilterDto>();
            CreateMap<IEnumerable<LayoutDto>, IEnumerable<LayoutModel>>();

            CreateMap<CreateLayoutDto, LayoutModel>();
            CreateMap<LayoutDto, LayoutModel>();
            CreateMap<LayoutModel, LayoutDto>();

            #endregion

            #region OhlcSeries

            CreateMap<OhlcSeriesModel, OhlcSeriesDto>();
            CreateMap<OhlcSeriesFilterModel, OhlcSeriesFilterDto>();
            CreateMap<OhlcSeriesDto, OhlcSeriesModel>();

            #endregion

            #region Orders

            CreateMap<CreateOrderModel, CreateOrderDto>();
            CreateMap<OrderDto, OrderModel>();
            CreateMap<OrderFilterRequest, OrderFilterDto>();
            CreateMap<IEnumerable<OrderDto>, IEnumerable<OrderModel>>();

            CreateMap<CreateOrderDto, OrderModel>().IncludeAllDerived();
            CreateMap<CreateLimitOrderDto, LimitOrderModel>();
            CreateMap<CreateMarketOrderDto, MarketOrderModel>();

            CreateMap<OrderModel, OrderDto>().IncludeAllDerived();
            CreateMap<LimitOrderModel, LimitOrderDto>();
            CreateMap<MarketOrderModel, MarketOrderDto>();

            #endregion

            #region PointSeries

            CreateMap<PointSeriesModel, PointSeriesDto>();
            CreateMap<PointSeriesFilterModel, PointSeriesFilterDto>();
            CreateMap<PointSeriesDto, PointSeriesModel>();

            #endregion

            #region Portfolios

            CreateMap<IEnumerable<PortfolioDto>, IEnumerable<PortfolioModel>>();
            CreateMap<CreatePortfolioModel, CreatePortfolioDto>();

            CreateMap<PortfolioDto, PortfolioModel>();
            CreateMap<PortfolioModel, PortfolioDto>();

            CreateMap<CreatePortfolioDto, PortfolioModel>();

            #endregion
        }
    }
}