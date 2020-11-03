using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Exchange;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Services.AssetService
{
    public class Consumer :
        IConsumer<CreateExchange>,
        IConsumer<GetExchanges>,
        IConsumer<DeleteExchange>,
        IConsumer<CreateAsset>,
        IConsumer<GetAssets>,
        IConsumer<DeleteAsset>
    {
        private readonly IAssetService _service;

        public Consumer(IAssetService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<CreateAsset> context)
        {
            await context.MarshallWith(_service.CreateAsset);
        }

        public async Task Consume(ConsumeContext<DeleteAsset> context)
        {
            await context.MarshallWith(_service.DeleteAsset);
        }

        public async Task Consume(ConsumeContext<GetAssets> context)
        {
            await context.MarshallWith(_service.GetAssetsRange);
        }

        public async Task Consume(ConsumeContext<CreateExchange> context)
        {
            await context.MarshallWith(_service.CreateExchange);
        }

        public async Task Consume(ConsumeContext<DeleteExchange> context)
        {
            await context.MarshallWith(_service.DeleteExchange);
        }

        public async Task Consume(ConsumeContext<GetExchanges> context)
        {
            await context.MarshallWith(_service.GetExchangesRange);
        }
    }
}