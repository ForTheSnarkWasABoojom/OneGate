using System.Threading.Tasks;
using EasyNetQ;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Contracts.Timeseries.CreateOhlcTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.CreateValueTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.DeleteOhlcTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.DeleteValueTimeseries;
using OneGate.Backend.Rpc.Contracts.Timeseries.GetOhlcTimeseriesByFilter;
using OneGate.Backend.Rpc.Contracts.Timeseries.GetValueTimeseriesByFilter;

namespace OneGate.Backend.Rpc.Services
{
    public interface ITimeseriesService
    {
        public Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request);
        
        public Task<CreateOhlcTimeseriesResponse> CreateOhlcTimeseriesAsync(CreateOhlcTimeseriesRequest request);

        public Task<GetOhlcTimeseriesByFilterResponse>
            GetOhlcTimeseriesByFilterAsync(GetOhlcTimeseriesByFilterRequest request);
        public Task<DeleteOhlcTimeseriesResponse> DeleteOhlcTimeseriesAsync(DeleteOhlcTimeseriesRequest request);
      
        public Task<CreateValueTimeseriesResponse> CreateValueTimeseriesAsync(CreateValueTimeseriesRequest request);

        public Task<GetValueTimeseriesByFilterResponse> GetValueTimeseriesByFilterAsync(
            GetValueTimeseriesByFilterRequest request);

        public Task<DeleteValueTimeseriesResponse> DeleteValueTimeseriesAsync(DeleteValueTimeseriesRequest request);
    }

    public class OhlcService : ITimeseriesService
    {
        private IBus _bus;

        public OhlcService(IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return await _bus.CallAsync<HealthCheckRequest, HealthCheckResponse>(request);
        }

        public async Task<CreateOhlcTimeseriesResponse> CreateOhlcTimeseriesAsync(CreateOhlcTimeseriesRequest request)
        {
            return await _bus.CallAsync<CreateOhlcTimeseriesRequest, CreateOhlcTimeseriesResponse>(request);
        }

        public async Task<GetOhlcTimeseriesByFilterResponse> GetOhlcTimeseriesByFilterAsync(
            GetOhlcTimeseriesByFilterRequest request)
        {
            return await _bus.CallAsync<GetOhlcTimeseriesByFilterRequest, GetOhlcTimeseriesByFilterResponse>(request);
        }

        public async Task<DeleteOhlcTimeseriesResponse> DeleteOhlcTimeseriesAsync(DeleteOhlcTimeseriesRequest request)
        {
            return await _bus.CallAsync<DeleteOhlcTimeseriesRequest, DeleteOhlcTimeseriesResponse>(request);
        }

        public async Task<CreateValueTimeseriesResponse> CreateValueTimeseriesAsync(
            CreateValueTimeseriesRequest request)
        {
            return await _bus.CallAsync<CreateValueTimeseriesRequest, CreateValueTimeseriesResponse>(request);
        }

        public async Task<GetValueTimeseriesByFilterResponse> GetValueTimeseriesByFilterAsync(
            GetValueTimeseriesByFilterRequest request)
        {
            return await _bus.CallAsync<GetValueTimeseriesByFilterRequest, GetValueTimeseriesByFilterResponse>(request);
        }

        public async Task<DeleteValueTimeseriesResponse> DeleteValueTimeseriesAsync(
            DeleteValueTimeseriesRequest request)
        {
            return await _bus.CallAsync<DeleteValueTimeseriesRequest, DeleteValueTimeseriesResponse>(request);
        }
    }
}