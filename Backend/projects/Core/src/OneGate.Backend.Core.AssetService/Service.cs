using System.Threading.Tasks;
using OneGate.Backend.Core.AssetService.Repository;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Backend.Transport.Contracts.Layout;
using OneGate.Common.Models.Common;

namespace OneGate.Backend.Core.AssetService
{
    public class Service : IService
    {
        private readonly IAssetRepository _assets;
        private readonly IExchangeRepository _exchanges;
        private readonly ILayoutRepository _layouts;

        public Service(IAssetRepository assets, IExchangeRepository exchanges, ILayoutRepository layouts)
        {
            _assets = assets;
            _exchanges = exchanges;
            _layouts = layouts;
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

        public async Task<AssetsResponse> GetAssets(GetAssets request)
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

        public async Task<ExchangesResponse> GetExchanges(GetExchanges request)
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

        public async Task<CreatedResourceResponse> CreateLayout(CreateLayout request)
        {
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _layouts.AddAsync(request.Layout)
                }
            };
        }

        public async Task<LayoutsResponse> GetLayouts(GetLayouts request)
        {
            return new LayoutsResponse
            {
                Layouts = await _layouts.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeleteLayout(DeleteLayout request)
        {
            await _layouts.RemoveAsync(request.Id);
            return new SuccessResponse();
        }
    }
}