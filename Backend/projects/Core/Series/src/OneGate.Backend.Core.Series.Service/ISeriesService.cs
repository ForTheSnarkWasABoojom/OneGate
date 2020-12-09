using System.Threading.Tasks;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Backend.Transport.Contracts.Series.Point;

namespace OneGate.Backend.Core.Series.Service
{
    public interface ISeriesService
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