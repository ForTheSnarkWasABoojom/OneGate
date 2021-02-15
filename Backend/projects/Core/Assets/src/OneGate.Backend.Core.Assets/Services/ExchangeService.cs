using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Assets.Contracts.Exchange;
using OneGate.Backend.Core.Assets.Database.Models;
using OneGate.Backend.Core.Assets.Database.Repository;
using OneGate.Backend.Core.Base.Linq;

namespace OneGate.Backend.Core.Assets.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangeRepository _exchanges;
        private readonly IMapper _mapper;

        public ExchangeService(IExchangeRepository exchanges, IMapper mapper)
        {
            _exchanges = exchanges;
            _mapper = mapper;
        }

        public async Task<ExchangesResponse> GetExchangesAsync(GetExchanges request)
        {
            Expression<Func<Exchange, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.Title == request.Title, request.Title);
            
            var exchanges = await _exchanges.FilterAsync(filter, limits: limits);

            var exchangesDto = _mapper.Map<IEnumerable<Exchange>, IEnumerable<ExchangeDto>>(exchanges);
            return new ExchangesResponse
            {
                Exchanges = exchangesDto
            };
        }
    }
}