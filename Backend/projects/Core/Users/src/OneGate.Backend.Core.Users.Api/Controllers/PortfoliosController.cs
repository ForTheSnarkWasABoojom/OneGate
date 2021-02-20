using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Shared.Api;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Shared.Linq;
using OneGate.Backend.Core.Users.Api.Contracts.Portfolio;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;

namespace OneGate.Backend.Core.Users.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "portfolios")]
    public class PortfoliosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PortfoliosController> _logger;
        private readonly IPortfolioRepository _portfolios;

        public PortfoliosController(ILogger<PortfoliosController> logger, IMapper mapper,
            IPortfolioRepository portfolios)
        {
            _logger = logger;
            _mapper = mapper;
            _portfolios = portfolios;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolioAsync([FromBody] CreatePortfolioDto request)
        {
            var portfolio = _mapper.Map<Portfolio>(request);
            await _portfolios.AddAsync(portfolio);

            return Ok(portfolio.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetPortfoliosAsync([FromQuery] FilterPortfoliosDto request)
        {
            Expression<Func<Portfolio, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.OwnerId == request.OwnerId, request.OwnerId);

            var portfolios = await _portfolios.FilterAsync(filter, limits: limits);

            var portfoliosDto = _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);
            return Ok(portfoliosDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePortfolioAsync([FromRoute] int id,
            [FromQuery(Name = "owner_id")] int ownerId)
        {
            await _portfolios.RemoveAsync(p =>
                p.Id == id &&
                p.OwnerId == ownerId
            );

            return Ok();
        }
    }
}