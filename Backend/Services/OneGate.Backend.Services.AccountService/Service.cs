using System.Threading.Tasks;
using OneGate.Backend.Contracts.Account;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.Order;
using OneGate.Backend.Rpc.Services;
using OneGate.Backend.Services.AccountService.Repository;
using OneGate.Shared.Models.Common;

namespace OneGate.Backend.Services.AccountService
{
    public class Service : IAccountService
    {
        private readonly IAccountRepository _accounts;
        private readonly IOrderRepository _orders;

        public Service(IAccountRepository accounts, IOrderRepository orders)
        {
            _accounts = accounts;
            _orders = orders;
        }

        public async Task<CreatedResourceResponse> CreateAccount(CreateAccount request)
        {
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _accounts.AddAsync(request.Account)
                }
            };
        }                    

        public async Task<AccountsResponse> GetAccountsRange(GetAccounts request)
        {
            return new AccountsResponse
            {
                Accounts = await _accounts.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeleteAccount(DeleteAccount request)
        {
            await _accounts.RemoveAsync(request.Id);
            return new SuccessResponse();
        }

        public async Task<CreatedResourceResponse> CreateOrder(CreateOrder request)
        {
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _orders.AddAsync(request.Order, request.OwnerId)
                }
            };
        }

        public async Task<OrdersResponse> GetOrdersRange(GetOrders request)
        {
            return new OrdersResponse
            {
                Orders = await _orders.FilterAsync(request.Filter, request.OwnerId)
            };
        }

        public async Task<SuccessResponse> DeleteOrder(DeleteOrder request)
        {
            await _orders.RemoveAsync(request.Id, request.OwnerId);
            return new SuccessResponse();
        }
    }
}