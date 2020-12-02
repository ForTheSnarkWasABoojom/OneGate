using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Contracts.Asset;
using OneGate.Backend.Contracts.Exchange;
using OneGate.Backend.Contracts.Layout;
using OneGate.Backend.Rpc;

namespace OneGate.Backend.Services.AssetService
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
            await context.MarshallWith(_service.CreateAsset);
        }

        public async Task Consume(ConsumeContext<DeleteAsset> context)
        {
            await context.MarshallWith(_service.DeleteAsset);
        }

        public async Task Consume(ConsumeContext<GetAssets> context)
        {
            await context.MarshallWith(_service.GetAssets);
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
            await context.MarshallWith(_service.GetExchanges);
        }
        
        public async Task Consume(ConsumeContext<CreateLayout> context)
        {
            await context.MarshallWith(_service.CreateLayout);
        }

        public async Task Consume(ConsumeContext<DeleteLayout> context)
        {
            await context.MarshallWith(_service.DeleteLayout);
        }

        public async Task Consume(ConsumeContext<GetLayouts> context)
        {
            await context.MarshallWith(_service.GetLayouts);
        }
    }
}