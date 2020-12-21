using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Backend.Transport.Contracts.PortfolioAssetLink;
using OneGate.Common.Models.Account;
using OneGate.Common.Models.Common;
using OneGate.Common.Models.Order;
using OneGate.Common.Models.Portfolio;
using OneGate.Common.Models.PortfolioAssetLink;

namespace OneGate.Backend.Core.Users.Node
{
    public class Service : IService
    {
        private readonly IMapper _mapper;

        private readonly IAccountRepository _accounts;
        private readonly IOrderRepository _orders;
        private readonly IPortfolioRepository _portfolios;
        private readonly IPorfolioAssetLinkRepository _links;

        public Service(IMapper mapper, IAccountRepository accounts, IOrderRepository orders,
            IPortfolioRepository portfolios,
            IPorfolioAssetLinkRepository links)
        {
            _mapper = mapper;
            _accounts = accounts;
            _orders = orders;
            _portfolios = portfolios;
            _links = links;
        }

        public async Task<CreatedResourceResponse> CreateAccountAsync(CreateAccount request)
        {
            var account = _mapper.Map<Account>(request.Account);
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _accounts.AddAsync(account)
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
            return new AuthorizationResponse
            {
                Account = _mapper.Map<AccountDto>(await _accounts.AnyMatch(request))
            };
        }

        public async Task<CreatedResourceResponse> CreateOrderAsync(CreateOrder request)
        {
            var orderBase = _mapper.Map<CreateOrderDto, Order>(request.Order);
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _orders.AddAsync(orderBase)
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
            var portfolio = _mapper.Map<Portfolio>(request);
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _portfolios.AddAsync(portfolio)
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
            var links = await _links.FilterAsync(null, request.Id, null, 1, 1);

            if (links.Count() != 0)
                throw new ApiException("Portfolio is not empty", StatusCodes.Status409Conflict);

            await _portfolios.RemoveAsync(request.Id, request.OwnerId);
            return new SuccessResponse();
        }

        public async Task<CreatedResourceResponse> CreatePortfolioAssetLinkAsync(CreatePortfolioAssetLink request)
        {
            var link = new PortfolioAssetLink
            {
                Count = request.PortfolioAssetLink.Count,
                PortfolioId = request.PortfolioAssetLink.PortfolioId,
                AssetId = request.PortfolioAssetLink.AssetId
            };
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _links.AddAsync(link)
                }
            };
        }

        public async Task<PortfolioAssetLinksResponse> GetPortfolioAssetLinksAsync(GetPortfolioAssetLinks request)
        {
            var portfolioAssetLinks = await _links.FilterAsync(request.Filter.Id, request.Filter.PortfolioId,
                request.Filter.AssetId, request.Filter.Shift, request.Filter.Count);
            return new PortfolioAssetLinksResponse
            {
                PortfolioAssetLinks = _mapper.Map<IEnumerable<PortfolioAssetLinkDto>>(portfolioAssetLinks)
            };
        }

        public async Task<SuccessResponse> DeletePortfolioAssetLinkAsync(DeletePortfolioAssetLink request)
        {
            await _links.RemoveAsync(request.Id);
            return new SuccessResponse();
        }
    }
}