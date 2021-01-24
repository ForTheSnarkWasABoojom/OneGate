using System;
using System.Linq;
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
    public class Converter : IConverter
    {
        public AccountFilterDto ToDto(AccountFilterModel src)
        {
            return new AccountFilterDto
            {
                Count = src.Count,
                Email = src.Email,
                FirstName = src.FirstName,
                Id = src.Id,
                IsAdmin = src.IsAdmin,
                LastName = src.LastName,
                Shift = src.Shift
            };
        }

        public AccountModel FromDto(AccountDto src)
        {
            return new AccountModel
            {
                Email = src.Email,
                FirstName = src.FirstName,
                Id = src.Id,
                IsAdmin = src.IsAdmin,
                LastName = src.LastName
            };
        }

        public CreateAssetDto ToDto(CreateAssetModel src)
        {
            return src.Type switch
            {
                AssetTypeModel.INDEX => new CreateIndexAssetDto
                {
                    ExchangeId = src.ExchangeId, Ticker = src.Ticker, Description = src.Description
                },
                AssetTypeModel.STOCK => new CreateStockAssetDto
                {
                    ExchangeId = src.ExchangeId, Ticker = src.Ticker, Description = src.Description
                },
                _ => null
            };
        }

        public CreateExchangeDto ToDto(CreateExchangeModel src)
        {
            Enum.TryParse(src.EngineType.ToString(), out EngineTypeDto type);
            return new CreateExchangeDto
            {
                Description = src.Description,
                EngineType = type,
                Title = src.Title,
                Website = src.Website
            };
        }

        public CreateLayoutDto ToDto(CreateLayoutModel src)
        {
            return new CreateLayoutDto
            {
                Description = src.Description,
                Name = src.Name
            };
        }

        public LayoutFilterDto ToDto(LayoutFilterModel src)
        {
            return new LayoutFilterDto
            {
                Id = src.Id,
                Name = src.Name
            };
        }

        public LayoutModel FromDto(LayoutDto src)
        {
            return new LayoutModel
            {
                Description = src.Description,
                Id = src.Id,
                Name = src.Name
            };
        }

        public OhlcSeriesDto ToDto(OhlcSeriesModel src)
        {
            Enum.TryParse(src.Interval.ToString(), out IntervalDto interval);
            return new OhlcSeriesDto
            {
                AssetId = src.AssetId,
                Interval = interval,
                Range = src.Range.Select(ToDto).ToList()
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

        public PointSeriesDto ToDto(PointSeriesModel src)
        {
            return new PointSeriesDto
            {
                LayoutId = src.LayoutId,
                AssetId = src.AssetId,
                Range = src.Range.Select(ToDto).ToList()
            };
        }

        public PointSeriesFilterDto ToDto(PointSeriesFilterModel src)
        {
            return new PointSeriesFilterDto
            {
                Id=src.Id,
                LayoutId = src.LayoutId,
                Name = src.Name,
                AssetId = src.AssetId,
                Count =src.Count,
                Shift = src.Shift,
                StartTimestamp = src.StartTimestamp,
                EndTimestamp = src.EndTimestamp
            };
        }

        private PointDto ToDto(PointModel src)
        {
            return new PointDto
            {
                Value = src.Value,
                Timestamp = src.Timestamp
            };
        }

        private OhlcDto ToDto(OhlcModel src)
        {
            return new OhlcDto
            {
                Low = src.Low,
                Close = src.Close,
                High = src.High,
                Open = src.Open,
                Timestamp = src.Timestamp
            };
        }
    }
}