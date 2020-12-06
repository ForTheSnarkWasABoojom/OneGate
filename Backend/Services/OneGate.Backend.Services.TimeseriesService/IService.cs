using System.Threading.Tasks;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Series.Ohlc;
using OneGate.Backend.Contracts.Series.Point;

namespace OneGate.Backend.Services.TimeseriesService
{
    public interface IService
    {
        public Task OnOhlcSeriesUpdated(OnOhlcSeriesUpdated request);
        
        public Task<SuccessResponse> CreateOhlcSeries(CreateOhlcSeries request);
        public Task<OhlcSeriesResponse> GetOhlcSeries(GetOhlcSeries request);
        public Task<SuccessResponse> DeleteOhlcSeries(DeleteOhlcSeries request);

        public Task<SuccessResponse> CreatePointSeries(CreatePointSeries request);
        public Task<PointSeriesResponse> GetPointSeries(GetPointSeries request);
        public Task<SuccessResponse> DeletePointSeries(DeletePointSeries request);
    }
}