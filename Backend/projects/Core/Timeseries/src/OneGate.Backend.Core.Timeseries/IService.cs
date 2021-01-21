using System.Threading.Tasks;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Backend.Transport.Contracts.Series.Point;

namespace OneGate.Backend.Core.Timeseries
{
    public interface IService
    {
        public Task OnOhlcSeriesUpdated(OnOhlcSeriesUpdated request);
        
        public Task<SuccessResponse> CreateOhlcSeriesAsync(CreateOhlcSeries request);
        public Task<OhlcSeriesResponse> GetOhlcSeriesAsync(GetOhlcSeries request);
        public Task<SuccessResponse> DeleteOhlcSeriesAsync(DeleteOhlcSeries request);

        public Task<SuccessResponse> CreatePointSeriesAsync(CreatePointSeries request);
        public Task<PointSeriesResponse> GetPointSeriesAsync(GetPointSeries request);
        public Task<SuccessResponse> DeletePointSeriesAsync(DeletePointSeries request);
    }
}