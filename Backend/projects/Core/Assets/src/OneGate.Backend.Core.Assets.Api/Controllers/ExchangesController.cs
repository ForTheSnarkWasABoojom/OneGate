using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Assets.Api.Contracts.Exchange;
using OneGate.Backend.Core.Assets.Database.Models;
using OneGate.Backend.Core.Assets.Database.Repository;
using OneGate.Backend.Core.Shared.Api;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Shared.Linq;

namespace OneGate.Backend.Core.Assets.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "exchanges")]
    public class ExchangesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ExchangesController> _logger;
        private readonly IExchangeRepository _exchanges;

        public ExchangesController(ILogger<ExchangesController> logger, IMapper mapper, IExchangeRepository exchanges)
        {
            _logger = logger;
            _mapper = mapper;
            _exchanges = exchanges;
        }

        [HttpGet]
        public async Task<IActionResult> GetExchangesAsync([FromQuery] FilterExchangesDto request)
        {
            Expression<Func<Exchange, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter = filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.Title == request.Title, request.Title);

            var exchanges = await _exchanges.FilterAsync(filter, limits: limits);

            var exchangesDto = _mapper.Map<IEnumerable<ExchangeDto>>(exchanges);
            return Ok(exchangesDto);
        }
    }
}