using System.Threading.Tasks;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Timeseries.Ohlc;
using OneGate.Backend.Transport.Contracts.Timeseries.Point;

namespace OneGate.Backend.Core.Timeseries.Services
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