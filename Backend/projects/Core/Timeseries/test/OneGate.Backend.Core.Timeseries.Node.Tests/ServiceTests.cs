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

namespace OneGate.Backend.Core.Timeseries.Node.Tests
{
    public class ServiceTests
    {
        private readonly Fixture _fixture;
        private readonly IPointSeriesRepository _pointSeriesRepository;
        private readonly IOhlcSeriesRepository _ohlcSeriesRepository;
        private readonly IMapper _mapper;

        private readonly IService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();

            _pointSeriesRepository = A.Fake<IPointSeriesRepository>();
            _ohlcSeriesRepository = A.Fake<IOhlcSeriesRepository>();
            _mapper = A.Fake<IMapper>();
            _service = new Service(_ohlcSeriesRepository, _pointSeriesRepository, _mapper);
        }


        [Fact]
        public async void CreateOhlcSeries_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreateOhlcSeries>();
            var fakeRepository = new List<OhlcSeries>();
            A.CallTo(() => _ohlcSeriesRepository.AddAsync(A<IEnumerable<OhlcSeries>>.Ignored))
                .Invokes((IEnumerable<OhlcSeries> x) => { fakeRepository.AddRange(x); });

            // Act.
            await _service.CreateOhlcSeriesAsync(request);


            // Assert.
            fakeRepository.Should().NotBeEmpty().And.Should().Equals(request.Series);
        }

        [Fact]
        public async void DeleteOhlcSeries_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeleteOhlcSeries>();

            var fakeRepository = new List<OhlcSeries>
            {
                _fixture.Create<OhlcSeries>(),
                _fixture.Build<OhlcSeries>()
                    .With(x => x.AssetId, request.Filter.AssetId)
                    .Create()
            };

            A.CallTo(() => _ohlcSeriesRepository.RemoveAsync(A<string>.Ignored, A<int>.Ignored
                    , A<DateTime?>.Ignored, A<DateTime?>.Ignored, A<int>.Ignored, A<int>.Ignored))
                .Invokes((string interval, int assetId, DateTime? endTimestamp,
                    DateTime? startTimestamp, int shift, int count) =>
                {
                    fakeRepository.RemoveAll(series => series.AssetId == assetId);
                });

            // Act.
            await _service.DeleteOhlcSeriesAsync(request);

            // Assert.
            fakeRepository.Should().NotContain(x => x.AssetId == request.Filter.AssetId);
        }
        

        [Fact]
        public async void CreatePointSeries_ShouldCreateEntityInRepository()
        {
            // Arrange.
            var request = _fixture.Create<CreatePointSeries>();
            var fakeRepository = new List<PointSeries>();
            A.CallTo(() => _pointSeriesRepository.AddAsync(A<IEnumerable<PointSeries>>.Ignored))
                .Invokes((IEnumerable<PointSeries> x) => { fakeRepository.AddRange(x); });

            // Act.
            await _service.CreatePointSeriesAsync(request);


            // Assert.
            fakeRepository.Should().NotBeEmpty().And.Should().Equals(request.Series);
        }

        [Fact]
        public async void DeletePointSeries_ShouldRemoveEntityFromRepository()
        {
            // Arrange.
            var request = _fixture.Create<DeletePointSeries>();

            var fakeRepository = new List<PointSeries>
            {
                _fixture.Create<PointSeries>(),
                _fixture.Build<PointSeries>()
                    .With(x => x.LayoutId, request.Filter.LayoutId)
                    .Create()
            };

            A.CallTo(() => _pointSeriesRepository.RemoveAsync(A<int>.Ignored, A<int>.Ignored
                    , A<DateTime?>.Ignored, A<DateTime?>.Ignored, A<int>.Ignored, A<int>.Ignored))
                .Invokes((int layoutId, int assetId, DateTime? endTimestamp, DateTime? startTimestamp, int shift,
                    int count) =>
                {
                    fakeRepository.RemoveAll(series => series.LayoutId == layoutId);
                });

            // Act.
            await _service.DeletePointSeriesAsync(request);

            // Assert.
            fakeRepository.Should().NotContain(x => x.LayoutId == request.Filter.LayoutId);
        }
    }
}