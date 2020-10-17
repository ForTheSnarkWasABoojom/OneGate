using System.Threading.Tasks;
using EasyNetQ;
using OneGate.Backend.Rpc.Contracts.Account.CreateAccount;
using OneGate.Backend.Rpc.Contracts.Account.CreateToken;
using OneGate.Backend.Rpc.Contracts.Account.DeleteAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccountsByFilter;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Contracts.Order.CreateOrder;
using OneGate.Backend.Rpc.Contracts.Order.DeleteOrder;
using OneGate.Backend.Rpc.Contracts.Order.GetOrder;
using OneGate.Backend.Rpc.Contracts.Order.GetOrdersByFilter;
using OneGate.Backend.Rpc.Contracts.Order.UpdateOrder;

namespace OneGate.Backend.Rpc.Services
{
    public interface IAccountService
    {
        public Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request);
        public Task<CreateTokenResponse> CreateTokenAsync(CreateTokenRequest request);
        
        public Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request);
        public Task<GetAccountResponse> GetAccountAsync(GetAccountRequest request);
        public Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request);
        public Task<GetAccountsByFilterResponse> GetAccountsByFilterAsync(GetAccountsByFilterRequest request);
        public Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request);
        public Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request);
        public Task<GetOrdersByFilterResponse> GetOrdersByFiltersAsync(GetOrdersByFilterRequest request);
        public Task<UpdateOrderResponse> UpdateOrderAsync(UpdateOrderRequest request);
        public Task<DeleteOrderResponse> DeleteOrderAsync(DeleteOrderRequest request);
    }

    public class AccountService : IAccountService
    {
        private IBus _bus;
        
        public AccountService(IBus bus)
        {
            _bus = bus;
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return await _bus.CallAsync<HealthCheckRequest, HealthCheckResponse>(request);
        }

        public async Task<CreateTokenResponse> CreateTokenAsync(CreateTokenRequest request)
        {
            return await _bus.CallAsync<CreateTokenRequest, CreateTokenResponse>(request);
        }
        
        public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            return await _bus.CallAsync<CreateAccountRequest, CreateAccountResponse>(request);
        }

        public async Task<GetAccountResponse> GetAccountAsync(GetAccountRequest request)
        {
            return await _bus.CallAsync<GetAccountRequest, GetAccountResponse>(request);
        }

        public async Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request)
        {
            return await _bus.CallAsync<DeleteAccountRequest, DeleteAccountResponse>(request);
        }

        public async Task<GetAccountsByFilterResponse> GetAccountsByFilterAsync(GetAccountsByFilterRequest request)
        {
            return await _bus.CallAsync<GetAccountsByFilterRequest, GetAccountsByFilterResponse>(request);
        }

        public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request)
        {
            return await _bus.CallAsync<CreateOrderRequest, CreateOrderResponse>(request);
        }

        public async Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request)
        {
            return await _bus.CallAsync<GetOrderRequest, GetOrderResponse>(request);
        }

        public async Task<GetOrdersByFilterResponse> GetOrdersByFiltersAsync(GetOrdersByFilterRequest request)
        {
            return await _bus.CallAsync<GetOrdersByFilterRequest, GetOrdersByFilterResponse>(request);
        }

        public async Task<UpdateOrderResponse> UpdateOrderAsync(UpdateOrderRequest request)
        {
            return await _bus.CallAsync<UpdateOrderRequest, UpdateOrderResponse>(request);
        }

        public async Task<DeleteOrderResponse> DeleteOrderAsync(DeleteOrderRequest request)
        {
            return await _bus.CallAsync<DeleteOrderRequest, DeleteOrderResponse>(request);
        }
    }
}