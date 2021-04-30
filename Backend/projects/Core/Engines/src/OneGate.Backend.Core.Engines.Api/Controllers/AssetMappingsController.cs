using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Engines.Api.Contracts.AssetMapping;
using OneGate.Backend.Core.Engines.Database.Models;
using OneGate.Backend.Core.Engines.Database.Repository;
using OneGate.Backend.Core.Shared.Api;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Shared.Linq;

namespace OneGate.Backend.Core.Engines.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "asset_mappings")]
    public class AssetMappingsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AssetMappingsController> _logger;
        private readonly IAssetMappingRepository _assetMappings;

        public AssetMappingsController(ILogger<AssetMappingsController> logger, IMapper mapper, IAssetMappingRepository assetMappings)
        {
            _logger = logger;
            _mapper = mapper;
            _assetMappings = assetMappings;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAssetMappingAsync([FromBody] CreateAssetMappingDto request)
        {
            var account = _mapper.Map<AssetMapping>(request);
            await _assetMappings.AddAsync(account);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAssetMappingsAsync([FromQuery] FilterAssetMappingsDto request)
        {
            Expression<Func<AssetMapping, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter = filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.AssetId == request.AssetId, request.AssetId)
                .FilterBy(p => p.EngineId == request.EngineId, request.EngineId);

            var assetMappings = await _assetMappings.FilterAsync(filter, limits: limits);

            var assetMappingsDto = _mapper.Map<IEnumerable<AssetMappingDto>>(assetMappings);
            return Ok(assetMappingsDto);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAssetMappingAsync([FromRoute] int id)
        {
            await _assetMappings.RemoveAsync(p =>
                p.Id == id
            );

            return Ok();
        }

    }
}