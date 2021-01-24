using System;
using OneGate.Backend.Core.Records.Database.Models;
using OneGate.Backend.Transport.Dto.Asset;
using OneGate.Backend.Transport.Dto.Exchange;
using OneGate.Backend.Transport.Dto.Layout;

namespace OneGate.Backend.Core.Records.Converters
{
    public class Converter : IConverter
    {
        public Asset FromDto(CreateAssetDto src)
        {
            return src.Type switch
            {
                AssetTypeDto.INDEX => new IndexAsset
                {
                    ExchangeId = src.ExchangeId, Ticker = src.Ticker, Description = src.Description
                },
                AssetTypeDto.STOCK => new StockAsset
                {
                    ExchangeId = src.ExchangeId, Ticker = src.Ticker, Description = src.Description
                },
                _ => null
            };
        }

        public AssetDto ToDto(Asset src)
        {
            Enum.TryParse(src.Type, out AssetTypeDto type);
            return type switch
            {
                AssetTypeDto.INDEX => new IndexAssetDto
                {
                    Id = src.Id, ExchangeId = src.ExchangeId, Ticker = src.Ticker, Description = src.Description
                },
                AssetTypeDto.STOCK => new StockAssetDto
                {
                    Id = src.Id, ExchangeId = src.ExchangeId, Ticker = src.Ticker, Description = src.Description
                },
                _ => null
            };
        }

        public Exchange FromDto(CreateExchangeDto src)
        {
            return new Exchange
            {
                Description = src.Description,
                EngineType = src.EngineType.ToString(),
                Website = src.Website,
                Title = src.Title
            };
        }

        public ExchangeDto ToDto(Exchange src)
        {
            Enum.TryParse(src.EngineType, out EngineTypeDto type);
            return new ExchangeDto
            {
                Description = src.Description,
                EngineType = type,
                Id = src.Id,
                Title = src.Title,
                Website = src.Website
            };
        }

        public Layout FromDto(CreateLayoutDto src)
        {
            return new Layout
            {
                Name = src.Name,
                Description = src.Description,
            };
        }

        public LayoutDto ToDto(Layout src)
        {
            return new LayoutDto
            {
                Name = src.Name,
                Description = src.Description,
                Id = src.Id
            };
        }
    }
}