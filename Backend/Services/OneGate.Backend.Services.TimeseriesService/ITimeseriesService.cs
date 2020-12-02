using System.Threading.Tasks;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.OhlcTimeseries;
using OneGate.Backend.Contracts.ValueTimeseries;

namespace OneGate.Backend.Services.TimeseriesService
{
    public interface ITimeseriesService
    {
        public Task OnOhlcTimeseriesUpdated(OnOhlcTimeseriesUpdated request);
        public Task<SuccessResponse> CreateOhlcTimeseries(CreateOhlcTimeseries request);
        public Task<OhlcTimeseriesResponse> GetOhlcTimeseriess(GetOhlcTimeseries request);
        public Task<SuccessResponse> DeleteOhlcTimeseries(DeleteOhlcTimeseries request);

        public Task<SuccessResponse> CreateValueTimeseries(CreateValueTimeseries request);
        public Task<ValueTimeseriesResponse> GetValueTimeseries(GetValueTimeseries request);
        public Task<SuccessResponse> DeleteValueTimeseries(DeleteValueTimeseries request);
    }
}