using System.Threading.Tasks;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Exchange;

namespace OneGate.Backend.Rpc.Services
{
    public interface IAssetService
    {
        public Task<CreatedResourceResponse> CreateAsset(CreateAsset request);
        public Task<AssetResponse> GetAsset(GetAsset request);
        public Task<AssetsRangeResponse> GetAssetsRange(GetAssetsRange request);
        public Task<SuccessResponse> DeleteAsset(DeleteAsset request);
        
        public Task<CreatedResourceResponse> CreateExchange(CreateExchange request);
        public Task<ExchangeResponse> GetExchange(GetExchange request);
        public Task<ExchangesRangeResponse> GetExchangesRange(GetExchangesRange request);
        public Task<SuccessResponse> DeleteExchange(DeleteExchange request);
    }
}