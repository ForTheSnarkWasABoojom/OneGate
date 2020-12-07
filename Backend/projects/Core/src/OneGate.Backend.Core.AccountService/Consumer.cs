using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Backend.Transport.Contracts.PortfolioAssetLink;

namespace OneGate.Backend.Core.AccountService
{
    public class Consumer : 
        IConsumer<CreateAccount>,
        IConsumer<GetAccounts>,
        IConsumer<DeleteAccount>,
        IConsumer<CreateOrder>,
        IConsumer<GetOrders>,
        IConsumer<DeleteOrder>,
        IConsumer<CreatePortfolio>,
        IConsumer<GetPortfolios>,
        IConsumer<DeletePortfolio>,
        IConsumer<CreatePortfolioAssetLink>,
        IConsumer<GetPortfolioAssetLinks>,
        IConsumer<DeletePortfolioAssetLink>
    {
        private readonly IService _service;

        public Consumer(IService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            await context.MarshallWith(_service.CreateAccount);
        }

        public async Task Consume(ConsumeContext<GetAccounts> context)
        {
            await context.MarshallWith(_service.GetAccounts);
        }

        public async Task Consume(ConsumeContext<DeleteAccount> context)
        {
            await context.MarshallWith(_service.DeleteAccount);
        }
        
        public async Task Consume(ConsumeContext<CreateOrder> context)
        {
            await context.MarshallWith(_service.CreateOrder);
        }

        public async Task Consume(ConsumeContext<GetOrders> context)
        {
            await context.MarshallWith(_service.GetOrders);
        }

        public async Task Consume(ConsumeContext<DeleteOrder> context)
        {
            await context.MarshallWith(_service.DeleteOrder);
        }

        public async Task Consume(ConsumeContext<CreatePortfolio> context)
        {
            await context.MarshallWith(_service.CreatePortfolio);
        }

        public async Task Consume(ConsumeContext<GetPortfolios> context)
        {
            await context.MarshallWith(_service.GetPortfolios);
        }

        public async Task Consume(ConsumeContext<DeletePortfolio> context)
        {
            await context.MarshallWith(_service.DeletePortfolio);
        }

        public async Task Consume(ConsumeContext<CreatePortfolioAssetLink> context)
        {
            await context.MarshallWith(_service.CreatePortfolioAssetLink);
        }

        public async Task Consume(ConsumeContext<GetPortfolioAssetLinks> context)
        {
            await context.MarshallWith(_service.GetPortfolioAssetLinks);
        }

        public async Task Consume(ConsumeContext<DeletePortfolioAssetLink> context)
        {
            await context.MarshallWith(_service.DeletePortfolioAssetLink);
        }
    }
}