using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Backend.Transport.Contracts.Portfolio;

namespace OneGate.Backend.Core.Users.Consumers
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
        IConsumer<CreateAuthorizationContext>
    {
        private readonly IService _service;

        public Consumer(IService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            await context.MarshallWith(_service.CreateAccountAsync);
        }

        public async Task Consume(ConsumeContext<GetAccounts> context)
        {
            await context.MarshallWith(_service.GetAccountsAsync);
        }

        public async Task Consume(ConsumeContext<DeleteAccount> context)
        {
            await context.MarshallWith(_service.DeleteAccountAsync);
        }
        
        public async Task Consume(ConsumeContext<CreateOrder> context)
        {
            await context.MarshallWith(_service.CreateOrderAsync);
        }

        public async Task Consume(ConsumeContext<GetOrders> context)
        {
            await context.MarshallWith(_service.GetOrdersAsync);
        }

        public async Task Consume(ConsumeContext<DeleteOrder> context)
        {
            await context.MarshallWith(_service.DeleteOrderAsync);
        }

        public async Task Consume(ConsumeContext<CreatePortfolio> context)
        {
            await context.MarshallWith(_service.CreatePortfolioAsync);
        }

        public async Task Consume(ConsumeContext<GetPortfolios> context)
        {
            await context.MarshallWith(_service.GetPortfoliosAsync);
        }

        public async Task Consume(ConsumeContext<DeletePortfolio> context)
        {
            await context.MarshallWith(_service.DeletePortfolioAsync);
        }

        public async Task Consume(ConsumeContext<CreateAuthorizationContext> context)
        {
            await context.MarshallWith(_service.CreateAuthorizationContext);
        }
    }
}