using System.Linq;
using System.Threading.Tasks;
using OneGate.Backend.Core.Records.Converters;
using OneGate.Backend.Core.Records.Database.Repository;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Backend.Transport.Contracts.Layout;
using OneGate.Backend.Transport.Dto.Common;

namespace OneGate.Backend.Core.Records.Services
{
    public class Service : IService
    {
        private readonly IConverter _converter;

        private readonly IAssetRepository _assets;
        private readonly IExchangeRepository _exchanges;
        private readonly ILayoutRepository _layouts;

        public Service(IAssetRepository assets, IExchangeRepository exchanges, ILayoutRepository layouts,
            IConverter converter)
        {
            _assets = assets;
            _exchanges = exchanges;
            _layouts = layouts;
            _converter = converter;
        }

        public async Task<CreatedResourceResponse> CreateAssetAsync(CreateAsset request)
        {
            var asset = _converter.FromDto(request.Asset);
            var entity = await _assets.AddAsync(asset);
            
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = entity.Id
                }
            };
        }

        public async Task<AssetsResponse> GetAssetsAsync(GetAssets request)
        {
            var assets = await _assets.FilterAsync(request.Filter.Id, request.Filter.Type.ToString(),
                request.Filter.Ticker, request.Filter.Exchange.Id,
                request.Filter.Exchange.Title, request.Filter.Exchange.EngineType.ToString(), request.Filter.Shift,
                request.Filter.Count);
            return new AssetsResponse
            {
                Assets = assets.Select(_converter.ToDto)
            };
        }

        public async Task<SuccessResponse> DeleteAssetAsync(DeleteAsset request)
        {
            await _assets.RemoveAsync(request.Id);
            return new SuccessResponse();
        }

        public async Task<CreatedResourceResponse> CreateExchangeAsync(CreateExchange request)
        {
            var exchange = _converter.FromDto(request.Exchange);
            var entity = await _exchanges.AddAsync(exchange);
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = entity.Id
                }
            };
        }

        public async Task<ExchangesResponse> GetExchangesAsync(GetExchanges request)
        {
            var exchanges = await _exchanges.FilterAsync(request.Filter.Id, request.Filter.Title,
                request.Filter.EngineType.ToString(), request.Filter.Shift,
                request.Filter.Count);
            return new ExchangesResponse
            {
                Exchanges = exchanges.Select(_converter.ToDto)
            };
        }

        public async Task<SuccessResponse> DeleteExchangeAsync(DeleteExchange request)
        {
            await _exchanges.RemoveAsync(request.Id);
            return new SuccessResponse();
        }

        public async Task<CreatedResourceResponse> CreateLayoutAsync(CreateLayout request)
        {
            var layout = _converter.FromDto(request.Layout);
            var entity = await _layouts.AddAsync(layout);
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = entity.Id
                }
            };
        }

        public async Task<LayoutsResponse> GetLayoutsAsync(GetLayouts request)
        {
            var layouts = await _layouts.FilterAsync(request.Filter.Id, request.Filter.Name, request.Filter.Shift,
                request.Filter.Count);
            return new LayoutsResponse
            {
                Layouts = layouts.Select(_converter.ToDto)
            };
        }

        public async Task<SuccessResponse> DeleteLayoutAsync(DeleteLayout request)
        {
            await _layouts.RemoveAsync(request.Id);
            return new SuccessResponse();
        }
    }
}