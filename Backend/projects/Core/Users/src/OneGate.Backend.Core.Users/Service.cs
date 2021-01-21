using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Transport.Dto.Common;
using OneGate.Backend.Transport.Dto.Order;
using OneGate.Backend.Transport.Dto.Portfolio;

namespace OneGate.Backend.Core.Users
{
    public class Service : IService
    {
        private readonly IMapper _mapper;

        private readonly IAccountRepository _accounts;
        private readonly IOrderRepository _orders;
        private readonly IPortfolioRepository _portfolios;

        public Service(IMapper mapper, IAccountRepository accounts, IOrderRepository orders,
            IPortfolioRepository portfolios)
        {
            _mapper = mapper;
            _accounts = accounts;
            _orders = orders;
            _portfolios = portfolios;
        }

        public async Task<CreatedResourceResponse> CreateAccountAsync(CreateAccount request)
        {
            var account = _mapper.Map<Account>(request.Account);
            var entity = await _accounts.AddAsync(account);
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = entity.Id
                }
            };
        }

        public async Task<AccountsResponse> GetAccountsAsync(GetAccounts request)
        {
            var accounts = await _accounts.FilterAsync(request.Filter.Id, request.Filter.Email,
                request.Filter.FirstName, request.Filter.LastName, request.Filter.IsAdmin, request.Filter.Shift,
                request.Filter.Count);
            return new AccountsResponse
            {
                Accounts = _mapper.Map<IEnumerable<AccountDto>>(accounts)
            };
        }

        public async Task<SuccessResponse> DeleteAccountAsync(DeleteAccount request)
        {
            await _accounts.RemoveAsync(request.Id);
            return new SuccessResponse();
        }

        public async Task<AuthorizationResponse> CreateAuthorizationContext(CreateAuthorizationContext request)
        {
            var entity = await _accounts.FindAsync(request.AuthDto.Username, request.AuthDto.Password);
            var accountDto = _mapper.Map<AccountDto>(entity);
            return new AuthorizationResponse
            {
                Account = accountDto
            };
        }

        public async Task<CreatedResourceResponse> CreateOrderAsync(CreateOrder request)
        {
            var order = _mapper.Map<CreateOrderDto, Order>(request.Order);
            order.OwnerId = request.OwnerId;
            
            var entity = await _orders.AddAsync(order);
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = entity.Id
                }
            };
        }

        public async Task<OrdersResponse> GetOrdersAsync(GetOrders request)
        {
            var orders = await _orders.FilterAsync(request.Filter.Id, request.Filter.Type.ToString(),
                request.Filter.AssetId,
                request.Filter.State.ToString(), request.Filter.Side.ToString(), request.OwnerId, request.Filter.Shift,
                request.Filter.Count);
            return new OrdersResponse
            {
                Orders = orders.Select(x => _mapper.Map<OrderDto>(x))
            };
        }

        public async Task<SuccessResponse> DeleteOrderAsync(DeleteOrder request)
        {
            await _orders.RemoveAsync(request.Id, request.OwnerId);
            return new SuccessResponse();
        }

        public async Task<CreatedResourceResponse> CreatePortfolioAsync(CreatePortfolio request)
        {
            var portfolio = _mapper.Map<Portfolio>(request.Portfolio);
            portfolio.OwnerId = request.OwnerId;
            
            var entity = await _portfolios.AddAsync(portfolio);
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = entity.Id
                }
            };
        }

        public async Task<PortfoliosResponse> GetPortfoliosAsync(GetPortfolios request)
        {
            var portfolios = await _portfolios.FilterAsync(request.Filter.Id, request.OwnerId, request.Filter.Name,
                request.Filter.Shift, request.Filter.Count);
            return new PortfoliosResponse
            {
                Portfolios = _mapper.Map<IEnumerable<PortfolioDto>>(portfolios)
            };
        }

        public async Task<SuccessResponse> DeletePortfolioAsync(DeletePortfolio request)
        {
            await _portfolios.RemoveAsync(request.Id, request.OwnerId);
            return new SuccessResponse();
        }
    }
}