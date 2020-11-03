using System.Threading.Tasks;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Exchange;

namespace OneGate.Backend.Rpc.Services
{
    public interface IAssetService
    {
        public Task<CreatedResourceResponse> CreateAsset(CreateAsset request);
        public Task<AssetsResponse> GetAssetsRange(GetAssets request);
        public Task<SuccessResponse> DeleteAsset(DeleteAsset request);
        
        public Task<CreatedResourceResponse> CreateExchange(CreateExchange request);
        public Task<ExchangesResponse> GetExchangesRange(GetExchanges request);
        public Task<SuccessResponse> DeleteExchange(DeleteExchange request);
    }
}