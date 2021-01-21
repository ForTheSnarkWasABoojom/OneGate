using System.Threading.Tasks;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Backend.Transport.Contracts.Layout;

namespace OneGate.Backend.Core.Records
{
    public interface IService
    {
        public Task<CreatedResourceResponse> CreateAssetAsync(CreateAsset request);
        public Task<AssetsResponse> GetAssetsAsync(GetAssets request);
        public Task<SuccessResponse> DeleteAssetAsync(DeleteAsset request);
        
        public Task<CreatedResourceResponse> CreateExchangeAsync(CreateExchange request);
        public Task<ExchangesResponse> GetExchangesAsync(GetExchanges request);
        public Task<SuccessResponse> DeleteExchangeAsync(DeleteExchange request);
        
        public Task<CreatedResourceResponse> CreateLayoutAsync(CreateLayout request);
        public Task<LayoutsResponse> GetLayoutsAsync(GetLayouts request);
        public Task<SuccessResponse> DeleteLayoutAsync(DeleteLayout request);
    }
}