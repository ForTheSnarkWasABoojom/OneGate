using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Core.Records.Services;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Asset;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Backend.Transport.Contracts.Layout;

namespace OneGate.Backend.Core.Records.Consumers
{
    public class Worker :
        IConsumer<GetExchanges>,
        IConsumer<GetAssets>,
        IConsumer<GetLayouts>
    {
        private readonly IService _service;

        public Worker(IService service)
        {
            _service = service;
        }
        
        public async Task Consume(ConsumeContext<GetAssets> context)
        {
            await context.MarshallWith(_service.GetAssetsAsync);
        }

        public async Task Consume(ConsumeContext<GetExchanges> context)
        {
            await context.MarshallWith(_service.GetExchangesAsync);
        }

        public async Task Consume(ConsumeContext<GetLayouts> context)
        {
            await context.MarshallWith(_service.GetLayoutsAsync);
        }
    }
}