using AutoMapper;
using OneGate.Backend.Core.Users.Contracts.Account;
using OneGate.Backend.Core.Users.Contracts.Order;
using OneGate.Backend.Core.Users.Contracts.Portfolio;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountDto, Account>();
            CreateMap<Account, AccountDto>();
            
            CreateMap<OrderDto, Order>()
                .IncludeAllDerived();

            CreateMap<MarketOrderDto, MarketOrder>();
            CreateMap<LimitOrderDto, LimitOrder>();
            CreateMap<StopOrderDto, StopOrder>();

            CreateMap<Order, OrderDto>()
                .IncludeAllDerived();
            
            CreateMap<MarketOrder, MarketOrderDto>();
            CreateMap<LimitOrder, LimitOrderDto>();
            CreateMap<StopOrder, StopOrderDto>();
            
            CreateMap<PortfolioDto, Portfolio>();
            CreateMap<Portfolio, PortfolioDto>();
        }
    }
}