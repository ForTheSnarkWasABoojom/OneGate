using System.Threading.Tasks;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Timeseries;

namespace OneGate.Backend.Rpc.Services
{
    public interface ITimeseriesService
    {
        public Task OnOhlcTimeseriesUpdated(OnOhlcTimeseriesUpdated request);
        public Task<SuccessResponse> CreateOhlcTimeseriesRange(CreateOhlcTimeseries request);
        public Task<OhlcTimeseriesResponse> GetOhlcTimeseriessRange(GetOhlcTimeseries request);
        public Task<SuccessResponse> DeleteOhlcTimeseriesRange(DeleteOhlcTimeseries request);

        public Task<SuccessResponse> CreateValueTimeseriesRange(CreateValueTimeseries request);
        public Task<ValueTimeseriesResponse> GetValueTimeseriessRange(GetValueTimeseries request);
        public Task<SuccessResponse> DeleteValueTimeseriesRange(DeleteValueTimeseries request);
    }
}