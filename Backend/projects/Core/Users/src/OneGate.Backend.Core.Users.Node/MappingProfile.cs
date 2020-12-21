using AutoMapper;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Common.Models.Account;
using OneGate.Common.Models.Order;
using OneGate.Common.Models.Portfolio;
using OneGate.Common.Models.PortfolioAssetLink;

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
            
            CreateMap<Portfolio, PortfolioDto>();
            CreateMap<PortfolioDto, Portfolio>();
            
            CreateMap<PortfolioAssetLink, PortfolioAssetLinkDto>();
            CreateMap<PortfolioAssetLinkDto, PortfolioAssetLink>();

            CreateMap<CreateOrderDto, Order>().IncludeAllDerived();
            CreateMap<CreateLimitOrderDto, LimitOrder>();
            CreateMap<CreateMarketOrderDto,MarketOrder>();
            
            CreateMap<Order, OrderDto>().IncludeAllDerived();
            CreateMap<LimitOrder, LimitOrderDto>();
            CreateMap<MarketOrder,MarketOrderDto>();
        }
    }
}