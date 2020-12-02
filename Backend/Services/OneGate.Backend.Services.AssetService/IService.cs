using System.Threading.Tasks;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Exchange;
using OneGate.Backend.Contracts.Layout;

namespace OneGate.Backend.Services.AssetService
{
    public interface IService
    {
        public Task<CreatedResourceResponse> CreateAsset(CreateAsset request);
        public Task<AssetsResponse> GetAssets(GetAssets request);
        public Task<SuccessResponse> DeleteAsset(DeleteAsset request);
        
        public Task<CreatedResourceResponse> CreateExchange(CreateExchange request);
        public Task<ExchangesResponse> GetExchanges(GetExchanges request);
        public Task<SuccessResponse> DeleteExchange(DeleteExchange request);
        
        public Task<CreatedResourceResponse> CreateLayout(CreateLayout request);
        public Task<LayoutsResponse> GetLayouts(GetLayouts request);
        public Task<SuccessResponse> DeleteLayout(DeleteLayout request);
    }
}