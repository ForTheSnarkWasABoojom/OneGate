using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Assets.Api.Contracts.Asset;
using OneGate.Backend.Core.Assets.Database.Models;
using OneGate.Backend.Core.Assets.Database.Repository;
using OneGate.Backend.Core.Shared.Api;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Shared.Linq;

namespace OneGate.Backend.Core.Assets.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "assets")]
    public class AssetsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AssetsController> _logger;
        private readonly IAssetRepository _assets;

        public AssetsController(ILogger<AssetsController> logger, IMapper mapper, IAssetRepository assets)
        {
            _logger = logger;
            _mapper = mapper;
            _assets = assets;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssetsAsync([FromQuery] FilterAssetsDto request)
        {
            Expression<Func<Asset, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.Ticker == request.Ticker, request.Ticker)
                .FilterBy(p => p.ExchangeId == request.ExchangeId, request.ExchangeId);

            var assets = await _assets.FilterAsync(filter, limits: limits);

            var assetsDto = _mapper.Map<IEnumerable<AssetDto>>(assets);
            return Ok(assetsDto);
        }
    }
}