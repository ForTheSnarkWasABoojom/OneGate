using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OneGate.Backend.Core.AccountService.Repository;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Account;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Order;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Backend.Transport.Contracts.PortfolioAssetLink;
using OneGate.Common.Models.Common;
using OneGate.Common.Models.PortfolioAssetLink;

namespace OneGate.Backend.Core.AccountService
{
    public class Service : IService
    {
        private readonly IAccountRepository _accounts;
        private readonly IOrderRepository _orders;
        private readonly IPortfolioRepository _portfolios;
        private readonly IPorfolioAssetLinkRepository _links;

        public Service(IAccountRepository accounts, IOrderRepository orders, IPortfolioRepository portfolios,
            IPorfolioAssetLinkRepository links)
        {
            _accounts = accounts;
            _orders = orders;
            _portfolios = portfolios;
            _links = links;
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

        public async Task<AccountsResponse> GetAccounts(GetAccounts request)
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

        public async Task<OrdersResponse> GetOrders(GetOrders request)
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

        public async Task<CreatedResourceResponse> CreatePortfolio(CreatePortfolio request)
        {
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _portfolios.AddAsync(request.Portfolio)
                }
            };
        }

        public async Task<PortfoliosResponse> GetPortfolios(GetPortfolios request)
        {
            return new PortfoliosResponse
            {
                Portfolios = await _portfolios.FilterAsync(request.Filter, request.OwnerId)
            };
        }

        public async Task<SuccessResponse> DeletePortfolio(DeletePortfolio request)
        {
            var links = await _links.FilterAsync(new PortfolioAssetLinkFilterDto
            {
                PortfolioId = request.Id
            });

            if (links.Count() != 0)
                throw new ApiException("Portfolio is not empty", StatusCodes.Status409Conflict);

            await _portfolios.RemoveAsync(request.Id, request.OwnerId);
            return new SuccessResponse();
        }

        public async Task<CreatedResourceResponse> CreatePortfolioAssetLink(CreatePortfolioAssetLink request)
        {
            return new CreatedResourceResponse
            {
                Resource = new ResourceDto
                {
                    Id = await _links.AddAsync(request.PortfolioAssetLink)
                }
            };
        }

        public async Task<PortfolioAssetLinksResponse> GetPortfolioAssetLinks(GetPortfolioAssetLinks request)
        {
            return new PortfolioAssetLinksResponse
            {
                PortfolioAssetLinks = await _links.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeletePortfolioAssetLink(DeletePortfolioAssetLink request)
        {
            await _links.RemoveAsync(request.Id);
            return new SuccessResponse();
        }
    }
}