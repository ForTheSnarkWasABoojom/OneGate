using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Backend.Transport.Contracts.Layout;

namespace OneGate.Backend.Core.Records.Consumers
{
    public class Consumer :
        IConsumer<CreateExchange>,
        IConsumer<GetExchanges>,
        IConsumer<DeleteExchange>,
        IConsumer<CreateAsset>,
        IConsumer<GetAssets>,
        IConsumer<DeleteAsset>,
        IConsumer<CreateLayout>,
        IConsumer<GetLayouts>,
        IConsumer<DeleteLayout>
    {
        private readonly IService _service;

        public Consumer(IService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<CreateAsset> context)
        {
            await context.MarshallWith(_service.CreateAssetAsync);
        }

        public async Task Consume(ConsumeContext<DeleteAsset> context)
        {
            await context.MarshallWith(_service.DeleteAssetAsync);
        }

        public async Task Consume(ConsumeContext<GetAssets> context)
        {
            await context.MarshallWith(_service.GetAssetsAsync);
        }

        public async Task Consume(ConsumeContext<CreateExchange> context)
        {
            await context.MarshallWith(_service.CreateExchangeAsync);
        }

        public async Task Consume(ConsumeContext<DeleteExchange> context)
        {
            await context.MarshallWith(_service.DeleteExchangeAsync);
        }

        public async Task Consume(ConsumeContext<GetExchanges> context)
        {
            await context.MarshallWith(_service.GetExchangesAsync);
        }
        
        public async Task Consume(ConsumeContext<CreateLayout> context)
        {
            await context.MarshallWith(_service.CreateLayoutAsync);
        }

        public async Task Consume(ConsumeContext<DeleteLayout> context)
        {
            await context.MarshallWith(_service.DeleteLayoutAsync);
        }

        public async Task Consume(ConsumeContext<GetLayouts> context)
        {
            await context.MarshallWith(_service.GetLayoutsAsync);
        }
    }
}