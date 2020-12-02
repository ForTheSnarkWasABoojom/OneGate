using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Services.AccountService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _db;

        public OrderRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(CreateOrderBaseDto model, int ownerId)
        {
            OrderBase orderBase = model switch
            {
                CreateMarketOrderDto marketOrderDto => new MarketOrder
                {
                    OwnerId = ownerId,
                    State = OrderStateDto.ACCEPTED.ToString(),
                    Type = marketOrderDto.Type.ToString(),
                    AssetId = marketOrderDto.AssetId,
                    Side = marketOrderDto.Side.ToString(),
                    Quantity = marketOrderDto.Quantity
                },
                CreateStopOrderDto stopOrderDto => new StopOrder
                {
                    OwnerId = ownerId,
                    State = OrderStateDto.ACCEPTED.ToString(),
                    Type = stopOrderDto.Type.ToString(),
                    AssetId = stopOrderDto.AssetId,
                    Side = stopOrderDto.Side.ToString(),
                    Quantity = stopOrderDto.Quantity,
                    Price = stopOrderDto.Price
                },
                CreateLimitOrderDto limitOrderDto => new LimitOrder
                {
                    OwnerId = ownerId,
                    State = OrderStateDto.ACCEPTED.ToString(),
                    Type = limitOrderDto.Type.ToString(),
                    AssetId = limitOrderDto.AssetId,
                    Side = limitOrderDto.Side.ToString(),
                    Quantity = limitOrderDto.Quantity,
                    Price = limitOrderDto.Price
                },
                _ => throw new ArgumentException("Invalid order type")
            };

            var order = await _db.Orders.AddAsync(orderBase);
            await _db.SaveChangesAsync();

            return order.Entity.Id;
        }

        public async Task<OrderBaseDto> FindAsync(int id, int ownerId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(x =>
                x.Id == id && x.OwnerId == ownerId);

            return ConvertOrderToDto(order);
        }

        public async Task<IEnumerable<OrderBaseDto>> FilterAsync(OrderBaseFilterDto filter, int ownerId)
        {
            var orderQuery = _db.Orders.Where(x => x.OwnerId == ownerId);

            if (filter.Id != null)
                orderQuery = orderQuery.Where(x => x.Id == filter.Id);
            
            if (filter.AssetId != null)
                orderQuery = orderQuery.Where(x => x.AssetId == filter.AssetId);

            if (filter.Type != null)
                orderQuery = orderQuery.Where(x => x.Type == filter.Type.ToString());

            if (filter.State != null)
                orderQuery = orderQuery.Where(x => x.State == filter.State.ToString());

            if (filter.Side != null)
                orderQuery = orderQuery.Where(x => x.Side == filter.Side.ToString());

            var orders = await orderQuery.Skip(filter.Shift).Take(filter.Count).ToListAsync();
            return orders.Select(ConvertOrderToDto);
        }

        public async Task RemoveAsync(int id, int ownerId)
        {
            _db.Orders.RemoveRange(_db.Orders.Where(x =>
                x.Id == id && x.OwnerId == ownerId));
            await _db.SaveChangesAsync();
        }

        private static OrderBaseDto ConvertOrderToDto(OrderBase order)
        {
            if (order is null)
                return null;
            
            return order switch
            {
                MarketOrder market => ConvertMarketOrderToDto(market),
                StopOrder stop => ConvertStopOrderToDto(stop),
                LimitOrder limit => ConvertLimitOrderToDto(limit),
                _ => throw new ArgumentException("Invalid order type")
            };
        }

        private static MarketOrderDto ConvertMarketOrderToDto(MarketOrder order)
        {
            return new MarketOrderDto
            {
                Id = order.Id,
                AssetId = order.AssetId,
                State = Enum.Parse<OrderStateDto>(order.State),
                Side = Enum.Parse<OrderSideDto>(order.Side),
                Quantity = order.Quantity
            };
        }

        private static StopOrderDto ConvertStopOrderToDto(StopOrder order)
        {
            return new StopOrderDto
            {
                Id = order.Id,
                AssetId = order.AssetId,
                State = Enum.Parse<OrderStateDto>(order.State),
                Side = Enum.Parse<OrderSideDto>(order.Side),
                Quantity = order.Quantity,
                Price = order.Price
            };
        }

        private static LimitOrderDto ConvertLimitOrderToDto(LimitOrder order)
        {
            return new LimitOrderDto
            {
                Id = order.Id,
                AssetId = order.AssetId,
                State = Enum.Parse<OrderStateDto>(order.State),
                Side = Enum.Parse<OrderSideDto>(order.Side),
                Quantity = order.Quantity,
                Price = order.Price
            };
        }
    }
}