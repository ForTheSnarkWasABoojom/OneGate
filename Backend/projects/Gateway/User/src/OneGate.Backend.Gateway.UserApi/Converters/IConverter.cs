using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Transport.Dto.Asset;
using OneGate.Backend.Transport.Dto.Exchange;
using OneGate.Backend.Transport.Dto.Order;
using OneGate.Backend.Transport.Dto.Portfolio;
using OneGate.Backend.Transport.Dto.Series.Ohlc;
using OneGate.Backend.Transport.Dto.Series.Point;
using OneGate.Shared.ApiModels.Account;
using OneGate.Shared.ApiModels.Asset;
using OneGate.Shared.ApiModels.Exchange;
using OneGate.Shared.ApiModels.Order;
using OneGate.Shared.ApiModels.Portfolio;
using OneGate.Shared.ApiModels.Timeseries.Ohlc;
using OneGate.Shared.ApiModels.Timeseries.Point;

namespace OneGate.Backend.Gateway.UserApi.Converters
{
    public interface IConverter
    {
        public CreateAccountDto ToDto(CreateAccountModel src);
        public AccountModel FromDto(AccountDto src);
        
        public AssetFilterDto ToDto(AssetFilterModel src);
        public AssetModel FromDto(AssetDto src);
        
        public ExchangeFilterDto ToDto(ExchangeFilterModel src);
        public ExchangeModel FromDto(ExchangeDto src);
        
        public OhlcSeriesFilterDto ToDto(OhlcSeriesFilterModel src);
        public OhlcSeriesModel FromDto(OhlcSeriesDto src);
        
        public CreateOrderDto ToDto(CreateOrderModel src);
        public OrderModel FromDto(OrderDto src);
        public OrderFilterDto ToDto(OrderFilterModel src);
        
        public PointSeriesFilterDto ToDto(PointSeriesFilterModel src);
        public PointSeriesModel FromDto(PointSeriesDto src);
        
        public CreatePortfolioDto ToDto(CreatePortfolioModel src);
        public PortfolioModel FromDto(PortfolioDto src);
    }
}