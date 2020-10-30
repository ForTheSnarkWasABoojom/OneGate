using System.Threading.Tasks;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Timeseries;

namespace OneGate.Backend.Rpc.Services
{
    public interface ITimeseriesService
    {
        public Task OnOhlcTimeseriesUpdated (OnOhlcTimeseriesUpdated request);
        public Task<SuccessResponse> CreateOhlcTimeseriesRange(CreateOhlcTimeseriesRange request);
        public Task<OhlcTimeseriesRangeResponse> GetOhlcTimeseriessRange(GetOhlcTimeseriesRange request);
        public Task<SuccessResponse> DeleteOhlcTimeseriesRange(DeleteOhlcTimeseriesRange request);
        
        public Task<SuccessResponse> CreateValueTimeseriesRange(CreateValueTimeseriesRange request);
        public Task<ValueTimeseriesRangeResponse> GetValueTimeseriessRange(GetValueTimeseriesRange request);
        public Task<SuccessResponse> DeleteValueTimeseriesRange(DeleteValueTimeseriesRange request);
    }
}