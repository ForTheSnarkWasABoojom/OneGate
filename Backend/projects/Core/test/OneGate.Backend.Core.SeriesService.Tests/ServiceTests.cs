using FakeItEasy;
using OneGate.Backend.Core.SeriesService.Repository;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Backend.Transport.Contracts.Series.Point;
using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Core.SeriesService.Tests
{
    public class ServiceTests
    {
        private readonly Fixture _fixture;
        private readonly IPointSeriesRepository _pointSeriesRepository;
        private readonly IOhlcSeriesRepository _ohlcSeriesRepository;

        private readonly IService _service;
        public ServiceTests()
        {
            _fixture = new Fixture();
            
            _pointSeriesRepository = A.Fake<IPointSeriesRepository>();
            _ohlcSeriesRepository = A.Fake<IOhlcSeriesRepository>();

            _service = new Service(_ohlcSeriesRepository, _pointSeriesRepository);
        }
        
        [Fact]
        public async void CreateOhlcSeries_ShouldTouchRepositoryAdd()
        {
            // Arrange.
            var request = _fixture.Create<CreateOhlcSeries>();

            // Act.
            await _service.CreateOhlcSeries(request);

            // Assert.
            A.CallTo(() => _ohlcSeriesRepository.AddAsync(request.Series)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void GetOhlcSeries_ShouldTouchRepositoryFilter()
        {
            // Arrange.
            var request = _fixture.Create<GetOhlcSeries>();

            // Act.
            await _service.GetOhlcSeries(request);

            // Assert.
            A.CallTo(() => _ohlcSeriesRepository.FilterAsync(request.Filter)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void DeleteOhlcSeries_ShouldTouchRepositoryRemove()
        {
            // Arrange.
            var request = _fixture.Create<DeleteOhlcSeries>();

            // Act.
            await _service.DeleteOhlcSeries(request);

            // Assert.
            A.CallTo(() => _ohlcSeriesRepository.RemoveAsync(request.Filter)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void CreatePointSeries_ShouldTouchRepositoryAdd()
        {
            // Arrange.
            var request = _fixture.Create<CreatePointSeries>();

            // Act.
            await _service.CreatePointSeries(request);

            // Assert.
            A.CallTo(() => _pointSeriesRepository.AddAsync(request.Series)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void GetPointSeries_ShouldTouchRepositoryFilter()
        {
            // Arrange.
            var request = _fixture.Create<GetPointSeries>();

            // Act.
            await _service.GetPointSeries(request);

            // Assert.
            A.CallTo(() => _pointSeriesRepository.FilterAsync(request.Filter)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void DeletePointSeries_ShouldTouchRepositoryRemove()
        {
            // Arrange.
            var request = _fixture.Create<DeletePointSeries>();

            // Act.
            await _service.DeletePointSeries(request);

            // Assert.
            A.CallTo(() => _pointSeriesRepository.RemoveAsync(request.Filter)).MustHaveHappenedOnceExactly();
        }
    }
}