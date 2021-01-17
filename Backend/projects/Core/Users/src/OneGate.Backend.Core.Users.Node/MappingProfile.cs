using AutoMapper;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Shared.ApiContracts.Account;
using OneGate.Shared.ApiContracts.Order;
using OneGate.Shared.ApiContracts.Portfolio;

namespace OneGate.Backend.Core.Users.Node
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAccountDto, Account>();
            CreateMap<Account, CreateAccountDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            
            CreateMap<CreatePortfolioDto, Portfolio>();
            CreateMap<Portfolio, PortfolioDto>();
            CreateMap<PortfolioDto, Portfolio>();

            CreateMap<CreateOrderDto, Order>().IncludeAllDerived();
            CreateMap<CreateLimitOrderDto, LimitOrder>();
            CreateMap<CreateMarketOrderDto,MarketOrder>();
            
            CreateMap<Order, OrderDto>().IncludeAllDerived();
            CreateMap<LimitOrder, LimitOrderDto>();
            CreateMap<MarketOrder,MarketOrderDto>();
        }
    }
}