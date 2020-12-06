using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Exchange;
using OneGate.Shared.Models.Layout;
using OneGate.Shared.Models.Series.Ohlc;
using OneGate.Shared.Models.Series.Point;
using Xunit;

namespace OneGate.Frontend.ClientLibrary.Tests
{
    public class OneGateApiIntegrationTests : IClassFixture<OneGateApiFixture>
    {
        private readonly OneGateApiFixture _fixture;

        public OneGateApiIntegrationTests(OneGateApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task StockAssetFullPipelineAsync()
        {
            var createdExchange = await _fixture.AdminApi.CreateExchangeAsync(new CreateExchangeDto
            {
                Title = Guid.NewGuid().ToString(),
                Description = "testExchangeForStockAsset",
                EngineType = EngineTypeDto.FAKE
            });
            
            var createdStockAsset = await _fixture.AdminApi.CreateAssetAsync(new CreateStockAssetDto
            {
                Ticker = Guid.NewGuid().ToString(),
                ExchangeId = createdExchange.Id,
                Description = "testStockAsset"
            });
            
            await _fixture.AdminApi.CreateOhlcSeriesAsync(new OhlcSeriesDto
            {
                AssetId = createdStockAsset.Id,
                Interval = IntervalDto.m1,
                Range = new List<OhlcDto>
                {
                    new OhlcDto
                    {
                        High = 100,
                        Low = 0,
                        Open = 50,
                        Close = 75,
                        Timestamp = _fixture.GetRandomDay()
                    },
                    new OhlcDto
                    {
                        High = 100,
                        Low = 0,
                        Open = 75,
                        Close = 25,
                        Timestamp = _fixture.GetRandomDay()
                    }
                }
            });

            await _fixture.AdminApi.DeleteOhlcSeriesAsync(new OhlcSeriesFilterDto
            {
                AssetId = createdStockAsset.Id,
                Interval = IntervalDto.m1,
                Count = 1000
            });

            var createdLayout = await _fixture.AdminApi.CreateLayoutAsync(new CreateLayoutDto
            {
                Name = "testLayout",
                Description = "layoutDescription"
            });

            var pointSeriesName = Guid.NewGuid().ToString();
            await _fixture.AdminApi.CreatePointSeriesAsync(new PointSeriesDto
            {
                AssetId = createdStockAsset.Id,
                LayoutId = createdLayout.Id,
                Range = new List<PointDto>
                {
                    new PointDto
                    {
                        Value = 10,
                        Timestamp = _fixture.GetRandomDay()
                    },
                    new PointDto
                    {
                        Value = 20,
                        Timestamp = _fixture.GetRandomDay()
                    },
                    new PointDto
                    {
                        Value = 30,
                        Timestamp = _fixture.GetRandomDay()
                    }
                }
            });

            await _fixture.AdminApi.DeletePointSeriesAsync(new PointSeriesFilterDto
            {
                AssetId = createdStockAsset.Id,
                Name = pointSeriesName,
                Count = 1000
            });

            await _fixture.AdminApi.DeleteAssetAsync(createdStockAsset.Id);

            await _fixture.AdminApi.DeleteExchangeAsync(createdExchange.Id);
        }
    }
}