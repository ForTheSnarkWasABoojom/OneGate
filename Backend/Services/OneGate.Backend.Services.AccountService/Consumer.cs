using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Contracts.Account;
using OneGate.Backend.Contracts.Order;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Services.AccountService
{
    public class Consumer : 
        IConsumer<CreateAccount>,
        IConsumer<GetAccounts>,
        IConsumer<DeleteAccount>,
        IConsumer<CreateOrder>,
        IConsumer<GetOrders>,
        IConsumer<DeleteOrder>
    {
        private readonly IAccountService _service;

        public Consumer(IAccountService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            await context.MarshallWith(_service.CreateAccount);
        }

        public async Task Consume(ConsumeContext<GetAccounts> context)
        {
            await context.MarshallWith(_service.GetAccountsRange);
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
            await context.MarshallWith(_service.GetOrdersRange);
        }

        public async Task Consume(ConsumeContext<DeleteOrder> context)
        {
            await context.MarshallWith(_service.DeleteOrder);
        }
    }
}