using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Core.Users.Contracts.Account;
using OneGate.Backend.Core.Users.Contracts.Credentials;
using OneGate.Backend.Core.Users.Contracts.Order;
using OneGate.Backend.Core.Users.Contracts.Portfolio;
using OneGate.Backend.Core.Users.Services;
using OneGate.Backend.Transport.Bus;

namespace OneGate.Backend.Core.Users.Consumers
{
    public class RpcWorker :
        IConsumer<CreateAccount>,
        IConsumer<CreateAuthorization>,
        IConsumer<GetAccounts>,
        IConsumer<DeleteAccount>,
        IConsumer<CreateOrder>,
        IConsumer<GetOrders>,
        IConsumer<DeleteOrder>,
        IConsumer<CreatePortfolio>,
        IConsumer<GetPortfolios>,
        IConsumer<DeletePortfolio>
    {
        private readonly IAccountService _accountService;
        private readonly IOrderService _orderService;
        private readonly IPortfolioService _portfolioService;

        private readonly IResponseExceptionHandler _exceptionHandler;

        public RpcWorker(IAccountService accountService, IOrderService orderService, 
            IPortfolioService portfolioService, IResponseExceptionHandler exceptionHandler)
        {
            _accountService = accountService;
            _orderService = orderService;
            _portfolioService = portfolioService;
            _exceptionHandler = exceptionHandler;
        }

        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            await context.RespondFromMethod(_accountService.CreateAccountAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<GetAccounts> context)
        {
            await context.RespondFromMethod(_accountService.GetAccountsAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<DeleteAccount> context)
        {
            await context.RespondFromMethod(_accountService.DeleteAccountAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<CreateAuthorization> context)
        {
            await context.RespondFromMethod(_accountService.CreateAuthorizationAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<CreateOrder> context)
        {
            await context.RespondFromMethod(_orderService.CreateOrderAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<GetOrders> context)
        {
            await context.RespondFromMethod(_orderService.GetOrdersAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<DeleteOrder> context)
        {
            await context.RespondFromMethod(_orderService.DeleteOrderAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<CreatePortfolio> context)
        {
            await context.RespondFromMethod(_portfolioService.CreatePortfolioAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<GetPortfolios> context)
        {
            await context.RespondFromMethod(_portfolioService.GetPortfoliosAsync, _exceptionHandler);
        }

        public async Task Consume(ConsumeContext<DeletePortfolio> context)
        {
            await context.RespondFromMethod(_portfolioService.DeletePortfolioAsync, _exceptionHandler);
        }
    }
}