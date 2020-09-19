using System.Threading.Tasks;
using EasyNetQ;
using OneGate.Backend.Rpc.Contracts.Account.DeleteAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccount;
using OneGate.Backend.Rpc.Contracts.Asset.CreateAsset;
using OneGate.Backend.Rpc.Contracts.Asset.DeleteAsset;
using OneGate.Backend.Rpc.Contracts.Asset.GetAsset;
using OneGate.Backend.Rpc.Contracts.Asset.GetAssetsByFilter;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Contracts.Exchange.CreateExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.DeleteExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.GetExchange;
using OneGate.Backend.Rpc.Contracts.Exchange.GetExchangesByFilter;

namespace OneGate.Backend.Rpc.Services
{
    public interface IAssetService
    {
        public Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request);
        
        public Task<CreateExchangeResponse> CreateExchangeAsync(CreateExchangeRequest request);
        public Task<GetExchangeResponse> GetExchangeAsync(GetExchangeRequest request);
        public Task<DeleteExchangeResponse> DeleteExchangeAsync(DeleteExchangeRequest request);
        public Task<GetExchangesByFilterResponse> GetExchangesByFilterAsync(GetExchangesByFilterRequest request);
        
        public Task<CreateAssetResponse> CreateAssetAsync(CreateAssetRequest request);
        public Task<GetAssetResponse> GetAssetAsync(GetAssetRequest request);
        public Task<DeleteAssetResponse> DeleteAssetAsync(DeleteAssetRequest request);
        public Task<GetAssetsByFilterResponse> GetAssetsByFilterAsync(GetAssetsByFilterRequest request);
    }

    public class AssetService : IAssetService
    {
        private IBus _bus;

        public AssetService(IBus bus)
        {
            _bus = bus;
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return await _bus.CallAsync<HealthCheckRequest, HealthCheckResponse>(request);
        }

        public async Task<CreateExchangeResponse> CreateExchangeAsync(CreateExchangeRequest request)
        {
            return await _bus.CallAsync<CreateExchangeRequest, CreateExchangeResponse>(request);
        }

        public async Task<GetExchangeResponse> GetExchangeAsync(GetExchangeRequest request)
        {
            return await _bus.CallAsync<GetExchangeRequest, GetExchangeResponse>(request);
        }

        public async Task<DeleteExchangeResponse> DeleteExchangeAsync(DeleteExchangeRequest request)
        {
            return await _bus.CallAsync<DeleteExchangeRequest, DeleteExchangeResponse>(request);
        }

        public async Task<GetExchangesByFilterResponse> GetExchangesByFilterAsync(GetExchangesByFilterRequest request)
        {
            return await _bus.CallAsync<GetExchangesByFilterRequest, GetExchangesByFilterResponse>(request);
        }

        public async Task<CreateAssetResponse> CreateAssetAsync(CreateAssetRequest request)
        {
            return await _bus.CallAsync<CreateAssetRequest, CreateAssetResponse>(request);
        }

        public async Task<GetAssetResponse> GetAssetAsync(GetAssetRequest request)
        {
            return await _bus.CallAsync<GetAssetRequest, GetAssetResponse>(request);
        }

        public async Task<DeleteAssetResponse> DeleteAssetAsync(DeleteAssetRequest request)
        {
            return await _bus.CallAsync<DeleteAssetRequest, DeleteAssetResponse>(request);
        }

        public async Task<GetAssetsByFilterResponse> GetAssetsByFilterAsync(GetAssetsByFilterRequest request)
        {
            return await _bus.CallAsync<GetAssetsByFilterRequest, GetAssetsByFilterResponse>(request);
        }
    }
}