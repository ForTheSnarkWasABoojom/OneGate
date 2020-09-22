using System.Threading.Tasks;
using EasyNetQ;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Contracts.Ohlc.CreateOhlcs;
using OneGate.Backend.Rpc.Contracts.Ohlc.DeleteOhlcs;
using OneGate.Backend.Rpc.Contracts.Ohlc.GetOhlcsByFilter;

namespace OneGate.Backend.Rpc.Services
{
    public interface IOhlcService
    {
        public Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request);
        public Task<CreateOhlcsResponse> CreateOhlcsAsync(CreateOhlcsRequest request);
        public Task<GetOhlcsByFilterResponse> GetOhlcsByFilterAsync(GetOhlcsByFilterRequest request);
        public Task<DeleteOhlcsResponse> DeleteOhlcsAsync(DeleteOhlcsRequest request);
    }

    public class OhlcService : IOhlcService
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

        public async Task<CreateOhlcsResponse> CreateOhlcsAsync(CreateOhlcsRequest request)
        {
            return await _bus.CallAsync<CreateOhlcsRequest, CreateOhlcsResponse>(request);
        }

        public async Task<GetOhlcsByFilterResponse> GetOhlcsByFilterAsync(GetOhlcsByFilterRequest request)
        {
            return await _bus.CallAsync<GetOhlcsByFilterRequest, GetOhlcsByFilterResponse>(request);
        }

        public async Task<DeleteOhlcsResponse> DeleteOhlcsAsync(DeleteOhlcsRequest request)
        {
            return await _bus.CallAsync<DeleteOhlcsRequest, DeleteOhlcsResponse>(request);
        }
    }
}