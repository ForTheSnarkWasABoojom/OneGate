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
using OneGate.Backend.Core.Timeseries.Api.Contracts.Ohlc;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;

namespace OneGate.Backend.Core.Timeseries.Api.Controllers
{
    [Route(RouteBase + "ohlc")]
    public class OhlcsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OhlcsController> _logger;
        private readonly IOhlcRepository _ohlcs;

        public OhlcsController(ILogger<OhlcsController> logger, IMapper mapper, IOhlcRepository ohlcs)
        {
            _logger = logger;
            _mapper = mapper;
            _ohlcs = ohlcs;
        }

        [HttpGet]
        public async Task<IActionResult> GetOhlcsAsync([FromQuery] FilterOhlcDto request)
        {
            Expression<Func<Ohlc, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);
            
            filter = filter
                .FilterBy(p => p.AssetId == request.AssetId)
                .FilterBy(p => p.Interval == request.Interval)
                .FilterBy(p => p.Timestamp >= request.StartTimestamp, request.StartTimestamp)
                .FilterBy(p => p.Timestamp <= request.EndTimestamp, request.EndTimestamp);

            var assets = await _ohlcs.FilterAsync(filter, limits: limits);

            var assetsDto = _mapper.Map<IEnumerable<OhlcDto>>(assets);
            return Ok(assetsDto);
        }
    }
}