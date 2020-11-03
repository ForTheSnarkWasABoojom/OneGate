using System.Threading.Tasks;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Exchange;
using OneGate.Backend.Rpc.Services;
using OneGate.Backend.Services.AssetService.Repository;
using OneGate.Shared.Models.Common;

namespace OneGate.Backend.Services.AssetService
{
    public class Service : IAssetService
    {
        private readonly IAssetRepository _assets;
        private readonly IExchangeRepository _exchanges;

        public Service(IAssetRepository assets, IExchangeRepository exchanges)
        {
            _assets = assets;
            _exchanges = exchanges;
        }

        public async Task<CreatedResourceResponse> CreateAsset(CreateAsset request)
        {
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _assets.AddAsync(request.Asset)
                }
            };
        }

        public async Task<AssetsResponse> GetAssetsRange(GetAssets request)
        {
            return new AssetsResponse
            {
                Assets = await _assets.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeleteAsset(DeleteAsset request)
        {
            await _assets.RemoveAsync(request.Id);
            return new SuccessResponse();
        }

        public async Task<CreatedResourceResponse> CreateExchange(CreateExchange request)
        {
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _exchanges.AddAsync(request.Exchange)
                }
            };
        }

        public async Task<ExchangesResponse> GetExchangesRange(GetExchanges request)
        {
            return new ExchangesResponse
            {
                Exchanges = await _exchanges.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeleteExchange(DeleteExchange request)
        {
            await _exchanges.RemoveAsync(request.Id);
            return new SuccessResponse();
        }
    }
}