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
using OneGate.Backend.Core.Timeseries.Api.Contracts.Series;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;

namespace OneGate.Backend.Core.Timeseries.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "timeseries")]
    public class TimeseriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TimeseriesController> _logger;
        private readonly ISeriesRepository _series;

        public TimeseriesController(ILogger<TimeseriesController> logger, IMapper mapper, ISeriesRepository series)
        {
            _logger = logger;
            _mapper = mapper;
            _series = series;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeseriesAsync([FromQuery] FilterSeriesDto request)
        {
            Expression<Func<Series, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.LayerId == request.LayoutId)
                .FilterBy(p => p.Timestamp >= request.StartTimestamp, request.StartTimestamp)
                .FilterBy(p => p.Timestamp <= request.EndTimestamp, request.EndTimestamp);

            var series = await _series.FilterAsync(filter, limits: limits);

            var seriesDto = _mapper.Map<IEnumerable<SeriesDto>>(series);
            return Ok(seriesDto);
        }
    }
}