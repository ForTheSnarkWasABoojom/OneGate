using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Exchange;
using OneGate.Backend.Database;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Services;
using OneGate.Backend.Services.AssetService.Repository;

namespace OneGate.Backend.Services.AssetService
{
    public class Consumer :
        IConsumer<CreateExchange>,
        IConsumer<GetExchange>,
        IConsumer<GetExchangesRange>,
        IConsumer<DeleteExchange>,
        IConsumer<CreateAsset>,
        IConsumer<GetAsset>,
        IConsumer<GetAssetsRange>,
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

        public async Task Consume(ConsumeContext<GetAsset> context)
        {
            await context.MarshallWith(_service.GetAsset);
        }

        public async Task Consume(ConsumeContext<DeleteAsset> context)
        {
            await context.MarshallWith(_service.DeleteAsset);
        }

        public async Task Consume(ConsumeContext<GetAssetsRange> context)
        {
            await context.MarshallWith(_service.GetAssetsRange);
        }

        public async Task Consume(ConsumeContext<CreateExchange> context)
        {
            await context.MarshallWith(_service.CreateExchange);
        }

        public async Task Consume(ConsumeContext<GetExchange> context)
        {
            await context.MarshallWith(_service.GetExchange);
        }

        public async Task Consume(ConsumeContext<DeleteExchange> context)
        {
            await context.MarshallWith(_service.DeleteExchange);
        }

        public async Task Consume(ConsumeContext<GetExchangesRange> context)
        {
            await context.MarshallWith(_service.GetExchangesRange);
        }
    }
}