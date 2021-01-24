using System;
using System.Linq;
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
using OneGate.Shared.ApiModels.Series.Ohlc;
using OneGate.Shared.ApiModels.Series.Point;

namespace OneGate.Backend.Gateway.UserApi.Converters
{
    public class Converter : IConverter
    {
        public CreateAccountDto ToDto(CreateAccountModel src)
        {
            return new CreateAccountDto
            {
                Email = src.Email,
                Password = src.Password,
                FirstName = src.FirstName,
                LastName = src.LastName,
                ClientFingerprint = src.ClientFingerprint
            };
        }

        public AccountModel FromDto(AccountDto src)
        {
            return new AccountModel
            {
                Id = src.Id,
                Email = src.Email,
                FirstName = src.FirstName,
                IsAdmin = src.IsAdmin,
                LastName = src.LastName
            };
        }

        public AssetFilterDto ToDto(AssetFilterModel src)
        {
            Enum.TryParse(src.Type.ToString(), out AssetTypeDto type);
            return new AssetFilterDto
            {
                Count = src.Count,
                Exchange = ToDto(src.Exchange),
                Id = src.Id,
                Shift = src.Shift,
                Ticker = src.Ticker,
                Type = type
            };
        }

        public AssetModel FromDto(AssetDto src)
        {
            return src.Type switch
            {
                AssetTypeDto.INDEX => new IndexAssetModel
                {
                    Id = src.Id, ExchangeId = src.ExchangeId, Ticker = src.Ticker, Description = src.Description
                },
                AssetTypeDto.STOCK => new StockAssetModel
                {
                    Id = src.Id, ExchangeId = src.ExchangeId, Ticker = src.Ticker, Description = src.Description
                },
                _ => null
            };
        }

        public ExchangeFilterDto ToDto(ExchangeFilterModel src)
        {
            Enum.TryParse(src.EngineType.ToString(), out EngineTypeDto type);
            return new ExchangeFilterDto
            {
                Id = src.Id,
                Title = src.Title,
                EngineType = type,
                Count = src.Count,
                Shift = src.Shift
            };
        }


        public ExchangeModel FromDto(ExchangeDto src)
        {
            Enum.TryParse(src.EngineType.ToString(), out EngineTypeModel type);
            return new ExchangeModel
            {
                Id = src.Id,
                Description = src.Description,
                EngineType = type,
                Title = src.Title,
                Website = src.Website
            };
        }


        public OhlcSeriesFilterDto ToDto(OhlcSeriesFilterModel src)
        {
            Enum.TryParse(src.Interval.ToString(), out IntervalDto interval);
            return new OhlcSeriesFilterDto
            {
                AssetId = src.AssetId,
                Count = src.Count,
                EndTimestamp = src.EndTimestamp,
                Id = src.Id,
                Interval = interval,
                Shift = src.Shift,
                StartTimestamp = src.StartTimestamp
            };
        }

        public OhlcSeriesModel ToDto(OhlcSeriesDto src)
        {
            Enum.TryParse(src.Interval.ToString(), out IntervalModel interval);
            return new OhlcSeriesModel
            {
                AssetId = src.AssetId,
                Interval = interval,
                Range = src.Range.Select(ToDto).ToList()
            };
        }

        public CreateOrderDto ToDto(CreateOrderModel src)
        {
            Enum.TryParse(src.Side.ToString(), out OrderSideDto side);
            return src.Type switch
            {
                OrderTypeModel.MARKET => new CreateLimitOrderDto
                {
                    AssetId = src.AssetId, Quantity = src.Quantity, Side = side
                },
                OrderTypeModel.STOP => new CreateStopOrderDto
                {
                    AssetId = src.AssetId, Quantity = src.Quantity, Side = side
                },
                OrderTypeModel.LIMIT => new CreateLimitOrderDto
                {
                    AssetId = src.AssetId, Quantity = src.Quantity, Side = side
                },
                _ => null
            };
        }

        public OrderModel FromDto(OrderDto src)
        {
            Enum.TryParse(src.State.ToString(), out OrderStateModel state);
            Enum.TryParse(src.Side.ToString(), out OrderSideModel side);
            return src.Type switch
            {
                OrderTypeDto.MARKET => new MarketOrderModel
                {
                    Id = src.Id,
                    AssetId = src.AssetId,
                    Quantity = src.Quantity,
                    Side = side,
                    State = state
                },
                OrderTypeDto.STOP => new StopOrderModel
                {
                    Id = src.Id,
                    AssetId = src.AssetId,
                    Quantity = src.Quantity,
                    Side = side,
                    State = state
                },
                OrderTypeDto.LIMIT => new LimitOrderModel
                {
                    Id = src.Id,
                    AssetId = src.AssetId,
                    Quantity = src.Quantity,
                    Side = side,
                    State = state
                },
                _ => null
            };
        }

        public OrderFilterDto ToDto(OrderFilterRequest src)
        {
            Enum.TryParse(src.State.ToString(), out OrderStateDto state);
            Enum.TryParse(src.Side.ToString(), out OrderSideDto side);
            Enum.TryParse(src.Type.ToString(), out OrderTypeDto type);
            return new OrderFilterDto
            {
                Id = src.Id,
                AssetId = src.AssetId,
                Count = src.Count,
                Shift = src.Shift,
                Side = side,
                State = state,
                Type = type
            };
        }

        public PointSeriesFilterDto ToDto(PointSeriesFilterModel src)
        {
            return new PointSeriesFilterDto
            {
                Id = src.Id,
                LayoutId = src.LayoutId,
                Name = src.Name,
                AssetId = src.AssetId,
                Count = src.Count,
                Shift = src.Shift,
                StartTimestamp = src.StartTimestamp,
                EndTimestamp = src.EndTimestamp
            };
        }

        public PointSeriesModel ToDto(PointSeriesDto src)
        {
            return new PointSeriesModel
            {
                LayoutId = src.LayoutId,
                AssetId = src.AssetId,
                Range = src.Range.Select(ToDto).ToList()
            };
        }

        public CreatePortfolioDto ToDto(CreatePortfolioModel src)
        {
            return new CreatePortfolioDto
            {
                Name = src.Name,
                Description = src.Description
            };
        }

        public PortfolioModel FromDto(PortfolioDto src)
        {
            return new PortfolioModel
            {
                Description = src.Description,
                Id = src.Id,
                Name = src.Name,
                OwnerId = src.OwnerId
            };
        }
        

        private OhlcModel ToDto(OhlcDto src)
        {
            return new OhlcModel
            {
                Low = src.Low,
                Close = src.Close,
                High = src.High,
                Open = src.Open,
                Timestamp = src.Timestamp
            };
        }

        private PointModel ToDto(PointDto src)
        {
            return new PointModel
            {
                Value = src.Value,
                Timestamp = src.Timestamp
            };
        }
    }
}