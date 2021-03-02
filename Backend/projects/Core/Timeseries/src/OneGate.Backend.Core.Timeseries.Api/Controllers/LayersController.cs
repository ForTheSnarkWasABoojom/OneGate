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
using OneGate.Backend.Core.Timeseries.Api.Contracts.Layer;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;

namespace OneGate.Backend.Core.Timeseries.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "layers")]
    public class LayersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LayersController> _logger;
        private readonly ILayerRepository _layers;

        public LayersController(ILogger<LayersController> logger, IMapper mapper, ILayerRepository layers)
        {
            _logger = logger;
            _mapper = mapper;
            _layers = layers;
        }

        [HttpGet]
        public async Task<IActionResult> GetLayersAsync([FromQuery] FilterLayersDto request)
        {
            Expression<Func<Layer, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.Id == request.Id)
                .FilterBy(p => p.AssetId == request.AssetId, request.AssetId)
                .FilterBy(p => p.IsMaster == request.IsMaster, request.IsMaster);

            var layers = await _layers.FilterAsync(filter, limits: limits);

            var layersDto = _mapper.Map<IEnumerable<LayerDto>>(layers);
            return Ok(layersDto);
        }
    }
}