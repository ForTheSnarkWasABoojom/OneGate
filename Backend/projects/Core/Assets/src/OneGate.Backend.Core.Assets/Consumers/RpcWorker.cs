using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Core.Assets.Services;
using OneGate.Backend.Core.Assets.Contracts.Asset;
using OneGate.Backend.Core.Assets.Contracts.Exchange;
using OneGate.Backend.Core.Assets.Contracts.Layer;
using OneGate.Backend.Transport.Bus;

namespace OneGate.Backend.Core.Assets.Consumers
{
    public class RpcWorker :
        IConsumer<GetExchanges>,
        IConsumer<GetAssets>,
        IConsumer<GetLayers>
    {
        private readonly IAssetService _assetService;
        private readonly ILayerService _layerService;
        private readonly IExchangeService _exchangeService;

        private readonly IResponseExceptionHandler _exceptionHandler;

        public RpcWorker(IAssetService assetService, ILayerService layerService, 
            IExchangeService exchangeService, IResponseExceptionHandler exceptionHandler)
        {
            _assetService = assetService;
            _layerService = layerService;
            _exchangeService = exchangeService;
            _exceptionHandler = exceptionHandler;
        }
        
        public async Task Consume(ConsumeContext<GetAssets> context)
        {
            await context.RespondFromMethod(_assetService.GetAssetsAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<GetExchanges> context)
        {
            await context.RespondFromMethod(_exchangeService.GetExchangesAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<GetLayers> context)
        {
            await context.RespondFromMethod(_layerService.GetLayersAsync, _exceptionHandler);
        }
    }
}