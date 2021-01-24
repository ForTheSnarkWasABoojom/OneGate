using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Transport.Dto.Asset;
using OneGate.Backend.Transport.Dto.Exchange;
using OneGate.Backend.Transport.Dto.Layout;
using OneGate.Backend.Transport.Dto.Series.Ohlc;
using OneGate.Backend.Transport.Dto.Series.Point;
using OneGate.Shared.ApiModels.Account;
using OneGate.Shared.ApiModels.Asset;
using OneGate.Shared.ApiModels.Exchange;
using OneGate.Shared.ApiModels.Layout;
using OneGate.Shared.ApiModels.Series.Ohlc;
using OneGate.Shared.ApiModels.Series.Point;

namespace OneGate.Backend.Gateway.AdminApi.Converters
{
    public interface IConverter
    {
        public AccountFilterDto ToDto(AccountFilterModel src);
        public AccountModel FromDto(AccountDto src);
        
        public CreateAssetDto ToDto(CreateAssetModel src);
        
        public CreateExchangeDto ToDto(CreateExchangeModel src);
        public CreateLayoutDto ToDto(CreateLayoutModel src);

        public LayoutFilterDto ToDto(LayoutFilterModel src);
        public LayoutModel FromDto(LayoutDto src);
        
        public OhlcSeriesDto ToDto(OhlcSeriesModel src);
        public OhlcSeriesFilterDto ToDto(OhlcSeriesFilterModel src);
        
        public PointSeriesDto ToDto(PointSeriesModel src);
        public PointSeriesFilterDto ToDto(PointSeriesFilterModel src);
    }
}