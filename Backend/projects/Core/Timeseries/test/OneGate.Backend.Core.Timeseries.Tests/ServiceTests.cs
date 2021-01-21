using System;
using System.Collections.Generic;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Backend.Transport.Contracts.Series.Point;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Core.Timeseries.Tests
{
    public class ServiceTests
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;

        private readonly List<PointSeries> _pointSeriesRepository;
        private readonly List<OhlcSeries> _ohlcSeriesRepository;

        private readonly IService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();

            var pointSeries = A.Fake<IPointSeriesRepository>();
            _pointSeriesRepository = ConfigureFakeRepository(pointSeries);

            var ohlcSeries = A.Fake<IOhlcSeriesRepository>();
            _ohlcSeriesRepository = ConfigureFakeRepository(ohlcSeries);

            _mapper = A.Fake<IMapper>();
            _service = new Service(ohlcSeries, pointSeries, _mapper);
        }

        private static List<PointSeries> ConfigureFakeRepository(IPointSeriesRepository pointSeries)
        {
            var fakeRepository = new List<PointSeries>();

            A.CallTo(() => pointSeries.AddAsync(A<IEnumerable<PointSeries>>.Ignored))
                .Invokes((IEnumerable<PointSeries> x) =>
                {
                    fakeRepository.AddRange(x);
                });

            A.CallTo(() => pointSeries.RemoveAsync(A<int>.Ignored, A<int>.Ignored
                    , A<DateTime?>.Ignored, A<DateTime?>.Ignored, A<int>.Ignored, A<int>.Ignored))
                .Invokes((int layoutId, int assetId, DateTime? endTimestamp, DateTime? startTimestamp, int shift,
                    int count) =>
                {
                    fakeRepository.RemoveAll(series => series.LayoutId == layoutId);
                });

            return fakeRepository;
        }

        private static List<OhlcSeries> ConfigureFakeRepository(IOhlcSeriesRepository ohlcSeries)
        {
            var fakeRepository = new List<OhlcSeries>();

            A.CallTo(() => ohlcSeries.AddAsync(A<IEnumerable<OhlcSeries>>.Ignored))
                .Invokes((IEnumerable<OhlcSeries> x) =>
                {
                    fakeRepository.AddRange(x);
                });

            A.CallTo(() => ohlcSeries.RemoveAsync(A<string>.Ignored, A<int>.Ignored
                    , A<DateTime?>.Ignored, A<DateTime?>.Ignored, A<int>.Ignored, A<int>.Ignored))
                .Invokes((string interval, int assetId, DateTime? endTimestamp,
                    DateTime? startTimestamp, int shift, int count) =>
                {
                    fakeRepository.RemoveAll(series => series.AssetId == assetId);
                });

            return fakeRepository;
        }

        [Fact]
        public async void CreateOhlcSeries_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreateOhlcSeries>();
            
            // Act.
            await _service.CreateOhlcSeriesAsync(request);

            // Assert.
            _ohlcSeriesRepository.Should().NotBeEmpty().And.Should().Equals(request.Series);
        }

        [Fact]
        public async void DeleteOhlcSeries_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteOhlcSeries>();

            _ohlcSeriesRepository.Add(_fixture.Create<OhlcSeries>());
            _ohlcSeriesRepository.Add(_fixture.Build<OhlcSeries>()
                .With(x => x.AssetId, request.Filter.AssetId)
                .Create());

            // Act.
            await _service.DeleteOhlcSeriesAsync(request);

            // Assert.
            _ohlcSeriesRepository.Should().NotContain(x => x.AssetId == request.Filter.AssetId);
        }


        [Fact]
        public async void CreatePointSeries_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreatePointSeries>();
            
            // Act.
            await _service.CreatePointSeriesAsync(request);

            // Assert.
            _pointSeriesRepository.Should().NotBeEmpty().And.Should().Equals(request.Series);
        }

        [Fact]
        public async void DeletePointSeries_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeletePointSeries>();

            _pointSeriesRepository.Add(_fixture.Create<PointSeries>());
            _pointSeriesRepository.Add(_fixture.Build<PointSeries>()
                .With(x => x.LayoutId, request.Filter.LayoutId)
                .Create());

            // Act.
            await _service.DeletePointSeriesAsync(request);

            // Assert.
            _pointSeriesRepository.Should().NotContain(x => x.LayoutId == request.Filter.LayoutId);
        }
    }
}