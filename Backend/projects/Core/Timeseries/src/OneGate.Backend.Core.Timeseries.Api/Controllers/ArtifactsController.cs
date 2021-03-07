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
using OneGate.Backend.Core.Timeseries.Api.Contracts.Artifact;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Layer;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;

namespace OneGate.Backend.Core.Timeseries.Api.Controllers
{
    [Route(RouteBase + "artifacts")]
    public class ArtifactsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ArtifactsController> _logger;
        private readonly IArtifactRepository _artifacts;

        public ArtifactsController(ILogger<ArtifactsController> logger, IMapper mapper, IArtifactRepository artifacts)
        {
            _logger = logger;
            _mapper = mapper;
            _artifacts = artifacts;
        }

        [HttpGet]
        public async Task<IActionResult> GetArtifactsAsync([FromQuery] FilterArtifactDto request)
        {
            Expression<Func<Artifact, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.LayerId == request.LayerId)
                .FilterBy(p => p.Timestamp >= request.StartTimestamp, request.StartTimestamp)
                .FilterBy(p => p.Timestamp <= request.EndTimestamp, request.EndTimestamp);

            var assets = await _artifacts.FilterAsync(filter, limits: limits);

            var assetsDto = _mapper.Map<IEnumerable<ArtifactDto>>(assets);
            return Ok(assetsDto);
        }
    }
}