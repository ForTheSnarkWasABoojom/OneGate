using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Base.Linq;
using OneGate.Backend.Core.Users.Contracts.Portfolio;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Contracts;

namespace OneGate.Backend.Core.Users.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolios;
        private readonly IMapper _mapper;

        public PortfolioService(IPortfolioRepository portfolios, IMapper mapper)
        {
            _portfolios = portfolios;
            _mapper = mapper;
        }
        
        public async Task<CreatedResourceResponse> CreatePortfolioAsync(CreatePortfolio request)
        {
            var portfolio = _mapper.Map<PortfolioDto, Portfolio>(request.Portfolio);
            await _portfolios.AddAsync(portfolio);

            return new CreatedResourceResponse
            {
                Id = portfolio.Id
            };
        }

        public async Task<PortfoliosResponse> GetPortfoliosAsync(GetPortfolios request)
        {
            Expression<Func<Portfolio, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.OwnerId == request.OwnerId, request.OwnerId);
            
            var portfolios = await _portfolios.FilterAsync(filter, limits: limits);

            var portfoliosDto = _mapper.Map<IEnumerable<Portfolio>, IEnumerable<PortfolioDto>>(portfolios);
            return new PortfoliosResponse
            {
                Portfolios = portfoliosDto
            };
        }

        public async Task<SuccessResponse> DeletePortfolioAsync(DeletePortfolio request)
        {
            await _portfolios.RemoveAsync(p =>
                p.Id == request.Id &&
                p.OwnerId == request.OwnerId
            );
            
            return new SuccessResponse();
        }
    }
}