using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Exchange;
using OneGate.Shared.Models.Timeseries;
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
                Description = "testExchangeForStockAsset"
            });
            
            var createdStockAsset = await _fixture.AdminApi.CreateAssetAsync(new CreateStockAssetDto
            {
                Ticker = Guid.NewGuid().ToString(),
                ExchangeId = createdExchange.Id,
                Description = "testStockAsset"
            });
            
            await _fixture.AdminApi.CreateOhlcTimeseriesAsync(new CreateOhlcTimeseriesRangeDto
            {
                AssetId = createdStockAsset.Id,
                Interval = OhlcIntervalDto.m1,
                Range = new List<CreateOhlcTimeseriesDto>
                {
                    new CreateOhlcTimeseriesDto
                    {
                        High = 100,
                        Low = 0,
                        Open = 50,
                        Close = 75,
                        Timestamp = _fixture.GetRandomDay()
                    },
                    new CreateOhlcTimeseriesDto
                    {
                        High = 100,
                        Low = 0,
                        Open = 75,
                        Close = 25,
                        Timestamp = _fixture.GetRandomDay()
                    }
                }
            });

            await _fixture.AdminApi.DeleteOhlcTimeseriesAsync(new OhlcTimeseriesFilterDto
            {
                AssetId = createdStockAsset.Id,
                Interval = OhlcIntervalDto.m1,
                Count = 1000
            });

            var valueTimeseriesName = Guid.NewGuid().ToString();
            await _fixture.AdminApi.CreateValueTimeseriesAsync(new CreateValueTimeseriesRangeDto
            {
                AssetId = createdStockAsset.Id,
                Name = valueTimeseriesName,
                Range = new List<CreateValueTimeseriesDto>
                {
                    new CreateValueTimeseriesDto
                    {
                        Value = 10,
                        Timestamp = _fixture.GetRandomDay()
                    },
                    new CreateValueTimeseriesDto
                    {
                        Value = 20,
                        Timestamp = _fixture.GetRandomDay()
                    },
                    new CreateValueTimeseriesDto
                    {
                        Value = 30,
                        Timestamp = _fixture.GetRandomDay()
                    }
                }
            });

            await _fixture.AdminApi.DeleteValueTimeseriesAsync(new ValueTimeseriesFilterDto
            {
                AssetId = createdStockAsset.Id,
                Name = valueTimeseriesName,
                Count = 1000
            });

            await _fixture.AdminApi.DeleteAssetAsync(createdStockAsset.Id);

            await _fixture.AdminApi.DeleteExchangeAsync(createdExchange.Id);
        }
    }
}