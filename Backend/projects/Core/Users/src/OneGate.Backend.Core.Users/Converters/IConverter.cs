using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Transport.Dto.Order;
using OneGate.Backend.Transport.Dto.Portfolio;

namespace OneGate.Backend.Core.Users.Converters
{
    public interface IConverter
    {
        public Account FromDto(CreateAccountDto src);
        public AccountDto ToDto(Account src);
        
        public Order FromDto(CreateOrderDto src);
        public OrderDto ToDto(Order src);
        
        public Portfolio FromDto(CreatePortfolio src);
        public PortfolioDto ToDto(Portfolio src);
    }
}