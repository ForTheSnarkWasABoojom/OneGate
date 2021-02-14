using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Users.Contracts.Order;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Users.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orders;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orders, IMapper mapper)
        {
            _orders = orders;
            _mapper = mapper;
        }

        public async Task<CreatedResourceResponse> CreateOrderAsync(CreateOrder request)
        {
            var order = _mapper.Map<OrderDto, Order>(request.Order);
            await _orders.AddAsync(order);
            
            return new CreatedResourceResponse
            {
                Id = order.Id
            };
        }

        public async Task<OrdersResponse> GetOrdersAsync(GetOrders request)
        {
            Expression<Func<Order, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);
            
            if (request.Id != null)
                filter.And(p => p.Id == request.Id);
            
            if (request.OwnerId != null)
                filter.And(p => p.OwnerId == request.OwnerId);
            
            var orders = await _orders.FilterAsync(filter, limits: limits);

            var ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
            return new OrdersResponse
            {
                Orders = ordersDto
            };
        }

        public async Task<SuccessResponse> DeleteOrderAsync(DeleteOrder request)
        {
            await _orders.RemoveAsync(p =>
                p.Id == request.Id &&
                p.OwnerId == request.OwnerId
            );
            
            return new SuccessResponse();
        }
    }
}