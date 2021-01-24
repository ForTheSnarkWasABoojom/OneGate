using System;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Backend.Transport.Dto.Account;
using OneGate.Backend.Transport.Dto.Order;
using OneGate.Backend.Transport.Dto.Portfolio;

namespace OneGate.Backend.Core.Users.Converters
{
    public class Converter:IConverter
    {
        public Account FromDto(CreateAccountDto src)
        {
            return new Account
            {
                Email = src.Email,
                Password = src.Password,
                FirstName = src.FirstName,
                LastName = src.LastName
            };
        }

        public AccountDto ToDto(Account src)
        {
            return new AccountDto
            {
                Id = src.Id,
                Email = src.Email,
                FirstName = src.FirstName,
                LastName = src.LastName,
                IsAdmin = src.IsAdmin
            };
        }

        public Order FromDto(CreateOrderDto src)
        {
            return src.Type switch
            {
                (OrderTypeDto.MARKET) => new MarketOrder
                {
                    AssetId = src.AssetId, Side = src.Side.ToString(), Quantity = src.Quantity
                },
                (OrderTypeDto.LIMIT) => new LimitOrder
                {
                    AssetId = src.AssetId, Side = src.Side.ToString(), Quantity = src.Quantity
                },
                (OrderTypeDto.STOP) => new StopOrder
                {
                    AssetId = src.AssetId, Side = src.Side.ToString(), Quantity = src.Quantity
                },
                _ => null
            };
        }

        public OrderDto ToDto(Order src)
        {
            Enum.TryParse(src.Side, out OrderSideDto side);
            Enum.TryParse(src.Type, out OrderTypeDto type);
            Enum.TryParse(src.State, out OrderStateDto state);
            return type switch
            {
                (OrderTypeDto.MARKET) => new MarketOrderDto
                {
                    AssetId = src.AssetId, Side = side, State = state, Quantity = src.Quantity
                },
                (OrderTypeDto.LIMIT) => new LimitOrderDto
                {
                    AssetId = src.AssetId, Side = side, State = state, Quantity = src.Quantity
                },
                (OrderTypeDto.STOP) => new StopOrderDto
                {
                    AssetId = src.AssetId, Side = side, State = state, Quantity = src.Quantity
                },
                _ => null
            };
        }

        public Portfolio FromDto(CreatePortfolio src)
        {
            return new Portfolio
            {
                Name = src.Portfolio.Name,
                Description = src.Portfolio.Description,
                OwnerId = src.OwnerId
            };
        }

        public PortfolioDto ToDto(Portfolio src)
        {
            return new PortfolioDto
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                OwnerId = src.OwnerId
            };
        }
    }
}