using AutoMapper;
using OneGate.Backend.Core.Users.Api.Contracts.Account;
using OneGate.Backend.Core.Users.Api.Contracts.Administrator;
using OneGate.Backend.Core.Users.Api.Contracts.Order;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Limit;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Market;
using OneGate.Backend.Core.Users.Api.Contracts.Order.Stop;
using OneGate.Backend.Core.Users.Api.Contracts.Portfolio;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMapForAccount();

            CreateMapForAdministrator();

            CreateMapForOrder();

            CreateMapForPortfolio();
        }

        private void CreateMapForAdministrator()
        {
            CreateMap<CreateAdministratorDto, Administrator>();
            CreateMap<Administrator, AdministratorDto>();
        }

        private void CreateMapForPortfolio()
        {
            CreateMap<CreatePortfolioDto, Portfolio>();
            CreateMap<Portfolio, PortfolioDto>();
        }

        private void CreateMapForOrder()
        {
            CreateMap<CreateOrderDto, Order>()
                .IncludeAllDerived();

            CreateMap<CreateMarketOrderDto, MarketOrder>();
            CreateMap<CreateLimitOrderDto, LimitOrder>();
            CreateMap<CreateStopOrderDto, StopOrder>();

            CreateMap<Order, OrderDto>()
                .IncludeAllDerived();

            CreateMap<MarketOrder, MarketOrderDto>();
            CreateMap<LimitOrder, LimitOrderDto>();
            CreateMap<StopOrder, StopOrderDto>();
        }

        private void CreateMapForAccount()
        {
            CreateMap<CreateAccountDto, Account>();
            CreateMap<Account, AccountDto>();
        }
    }
}