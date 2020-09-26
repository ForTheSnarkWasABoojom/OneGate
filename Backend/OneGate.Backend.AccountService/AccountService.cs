using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Contracts.Account.CreateAccount;
using OneGate.Backend.Rpc.Contracts.Account.CreateToken;
using OneGate.Backend.Rpc.Contracts.Account.DeleteAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccount;
using OneGate.Backend.Rpc.Contracts.Account.GetAccountsByFilter;
using OneGate.Backend.Rpc.Contracts.Asset.CreateAsset;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Contracts.Order.CreateOrder;
using OneGate.Backend.Rpc.Contracts.Order.DeleteOrder;
using OneGate.Backend.Rpc.Contracts.Order.GetOrder;
using OneGate.Backend.Rpc.Contracts.Order.GetOrdersByFilter;
using OneGate.Backend.Rpc.Services;
using OneGate.Shared.Models.Account;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Order;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace OneGate.Backend.AccountService
{
    public class AccountService : IHostedService, IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IBus _bus;

        public AccountService(ILogger<AccountService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;

            // Register method as remote callable.
            _bus.RegisterMethodAsync<HealthCheckRequest, HealthCheckResponse>(HealthCheckAsync);
            _bus.RegisterMethodAsync<CreateTokenRequest, CreateTokenResponse>(CreateTokenAsync);
            _bus.RegisterMethodAsync<CreateAccountRequest, CreateAccountResponse>(CreateAccountAsync);
            _bus.RegisterMethodAsync<GetAccountRequest, GetAccountResponse>(GetAccountAsync);
            _bus.RegisterMethodAsync<DeleteAccountRequest, DeleteAccountResponse>(DeleteAccountAsync);
            _bus.RegisterMethodAsync<GetAccountsByFilterRequest, GetAccountsByFilterResponse>(GetAccountsByFilterAsync);
            _bus.RegisterMethodAsync<CreateOrderRequest, CreateOrderResponse>(CreateOrderAsync);
            _bus.RegisterMethodAsync<GetOrderRequest, GetOrderResponse>(GetOrderAsync);
            _bus.RegisterMethodAsync<GetOrdersByFilterRequest, GetOrdersByFilterResponse>(GetOrdersByFiltersAsync);
            _bus.RegisterMethodAsync<DeleteOrderRequest,DeleteOrderResponse>(DeleteOrderAsync);
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(HealthCheckRequest request)
        {
            return new HealthCheckResponse
            {
                Timestamp = DateTime.Now
            };
        }

        public async Task<CreateTokenResponse> CreateTokenAsync(CreateTokenRequest request)
        {
            await using var db = new DatabaseContext();

            var account = await db.Accounts.FirstOrDefaultAsync(x =>
                x.Password == GetHash(request.Password) &&
                x.Email == request.Email);

            if (account is null)
                throw new ApiException("Account with specified username and password does not exist",
                    Status401Unauthorized);

            return new CreateTokenResponse
            {
                Account = ConvertAccountToDto(account),
                IsAdmin = account.IsAdmin
            };
        }

        public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            await using var db = new DatabaseContext();

            if (await db.Accounts.FirstOrDefaultAsync(x => x.Email == request.Account.Email) != null)
                throw new ApiException("Account must have unique email", Status400BadRequest);

            var account = await db.Accounts.AddAsync(new Account
            {
                FirstName = request.Account.FirstName,
                LastName = request.Account.LastName,
                Email = request.Account.Email,
                Password = GetHash(request.Account.Password)
            });
            await db.SaveChangesAsync();

            return new CreateAccountResponse()
            {
                Account = ConvertAccountToDto(account.Entity),
            };
        }

        public async Task<GetAccountResponse> GetAccountAsync(GetAccountRequest request)
        {
            await using var db = new DatabaseContext();

            var account = await db.Accounts.FindAsync(request.Id);
            if (account is null)
                throw new ApiException("Account with specified id does not exist", Status404NotFound);

            return new GetAccountResponse
            {
                Account = ConvertAccountToDto(account)
            };
        }

        public async Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request)
        {
            await using var db = new DatabaseContext();

            var account = await db.Accounts.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (account == null)
                throw new ApiException("Account with specified id does not exist", Status404NotFound);

            db.Accounts.Remove(account);
            await db.SaveChangesAsync();

            return new DeleteAccountResponse();
        }

        public async Task<GetAccountsByFilterResponse> GetAccountsByFilterAsync(GetAccountsByFilterRequest request)
        {
            await using var db = new DatabaseContext();

            var accountsQuery = db.Accounts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter.FirstName))
                accountsQuery = accountsQuery.Where(x =>
                    x.FirstName.ToLower().Contains(request.Filter.FirstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(request.Filter.LastName))
                accountsQuery = accountsQuery.Where(x =>
                    x.LastName.ToLower().Contains(request.Filter.LastName.ToLower()));

            if (!string.IsNullOrWhiteSpace(request.Filter.Email))
                accountsQuery = accountsQuery.Where(x =>
                    x.Email.ToLower().Contains(request.Filter.Email.ToLower()));

            if (request.Filter.IsAdmin != null)
                accountsQuery = accountsQuery.Where(x => x.IsAdmin == request.Filter.IsAdmin);

            var accounts = await accountsQuery.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetAccountsByFilterResponse
            {
                Accounts = accounts.Select(ConvertAccountToDto).ToList()
            };
        }

        public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request)
        {
            await using var db = new DatabaseContext();
            if (!await db.Assets.AnyAsync(x => x.Id == request.Order.AssetId))
                throw new ApiException("Asset with specified id does not exist", Status400BadRequest);

            OrderBase order = request.Order switch
            {
                CreateMarketOrderDto marketOrderDto => (await db.MarketOrders.AddAsync(new MarketOrder
                {
                    AccountId = request.AccountId,
                    State = OrderStateDto.WAITING.ToString(),
                    Type = marketOrderDto.Type.ToString(),
                    AssetId = marketOrderDto.AssetId,
                    Side = marketOrderDto.Side.ToString(),
                    Quantity = marketOrderDto.Quantity
                })).Entity,
                CreateStopOrderDto stopOrderDto => (await db.StopOrders.AddAsync(new StopOrder
                {
                    AccountId = request.AccountId,
                    State = OrderStateDto.WAITING.ToString(),
                    Type = stopOrderDto.Type.ToString(),
                    AssetId = stopOrderDto.AssetId,
                    Side = stopOrderDto.Side.ToString(),
                    Quantity = stopOrderDto.Quantity,
                    Price = stopOrderDto.Price
                })).Entity,
                CreateLimitOrderDto limitOrderDto => (await db.LimitOrders.AddAsync(new LimitOrder
                {
                    AccountId = request.AccountId,
                    State = OrderStateDto.WAITING.ToString(),
                    Type = limitOrderDto.Type.ToString(),
                    AssetId = limitOrderDto.AssetId,
                    Side = limitOrderDto.Side.ToString(),
                    Quantity = limitOrderDto.Quantity,
                    Price = limitOrderDto.Price
                })).Entity,
                _ => throw new ApiException("Unknown order type", Status400BadRequest)
            };

            await db.SaveChangesAsync();
            return new CreateOrderResponse()
            {
                Order = ConvertOrderToDto(order)
            };
        }

        public async Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request)
        {
            await using var db = new DatabaseContext();
            var order = await db.Orders.FirstOrDefaultAsync(x => 
                x.Id == request.Id && x.AccountId == request.AccountId);

            if (order is null)
                throw new ApiException("Order with specified id does not exist", Status404NotFound);

            return new GetOrderResponse
            {
                Order = ConvertOrderToDto(order)
            };
        }

        public async Task<GetOrdersByFilterResponse> GetOrdersByFiltersAsync(GetOrdersByFilterRequest request)
        {
            await using var db = new DatabaseContext();
            var orderQuery = db.Orders.Where(x => x.AccountId == request.AccountId);

            if (request.Filter.AssetId != null)
                orderQuery = orderQuery.Where(x => x.AssetId == request.Filter.AssetId);
            
            if (request.Filter.Type != null)
                orderQuery = orderQuery.Where(x => x.Type == request.Filter.Type.ToString());

            if (request.Filter.State != null)
                orderQuery = orderQuery.Where(x => x.State == request.Filter.State.ToString());
            
            if (request.Filter.Side != null)
                orderQuery = orderQuery.Where(x => x.Side == request.Filter.Side.ToString());

            var orders = await orderQuery.Skip(request.Filter.Shift).Take(request.Filter.Count).ToListAsync();
            return new GetOrdersByFilterResponse
            {
                Orders = orders.Select(ConvertOrderToDto).ToList()
            };
        }

        public async Task<DeleteOrderResponse> DeleteOrderAsync(DeleteOrderRequest request)
        {
            await using var db = new DatabaseContext();
            var order = await db.Orders.FirstOrDefaultAsync(x => 
                x.Id == request.Id && x.AccountId == request.AccountId);

            if (order == null)
                throw new ApiException("Order with specified id does not exist", Status404NotFound);

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return new DeleteOrderResponse();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken) 
        {
            _logger.LogInformation("Account service stopped");
        }

        private AccountDto ConvertAccountToDto(Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName
            };
        }

        private OrderBaseDto ConvertOrderToDto(OrderBase order)
        {
            return order switch
            {
                MarketOrder market => ConvertMarketOrderToDto(market),
                StopOrder stop => ConvertStopOrderToDto(stop),
                LimitOrder limit => ConvertLimitOrderToDto(limit),
                _ => null
            };
        }

        private MarketOrderDto ConvertMarketOrderToDto(MarketOrder order)
        {
            return new MarketOrderDto()
            {
                Id = order.Id,
                AssetId = order.AssetId,
                State = Enum.Parse<OrderStateDto>(order.State),
                Side = Enum.Parse<OrderSideDto>(order.Side),
                Quantity = order.Quantity
            };
        }
        
        private StopOrderDto ConvertStopOrderToDto(StopOrder order)
        {
            return new StopOrderDto()
            {
                Id = order.Id,
                AssetId = order.AssetId,
                State = Enum.Parse<OrderStateDto>(order.State),
                Side = Enum.Parse<OrderSideDto>(order.Side),
                Quantity = order.Quantity,
                Price = order.Price
            };
        }
        
        private LimitOrderDto ConvertLimitOrderToDto(LimitOrder order)
        {
            return new LimitOrderDto()
            {
                Id = order.Id,
                AssetId = order.AssetId,
                State = Enum.Parse<OrderStateDto>(order.State),
                Side = Enum.Parse<OrderSideDto>(order.Side),
                Quantity = order.Quantity,
                Price = order.Price
            };
        }
        private string GetHash(string str)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: str,
                salt: new byte[128 / 8],
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}