using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Assets.Contracts.Asset;
using OneGate.Backend.Core.Assets.Database.Models;
using OneGate.Backend.Core.Assets.Database.Repository;

namespace OneGate.Backend.Core.Assets.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assets;

        private readonly IMapper _mapper;

        public AssetService(IAssetRepository assets, IMapper mapper)
        {
            _assets = assets;
            _mapper = mapper;
        }

        public async Task<AssetsResponse> GetAssetsAsync(GetAssets request)
        {
            Expression<Func<Asset, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            if (request.Id != null)
                filter.And(p => p.Id == request.Id);
            
            var assets = await _assets.FilterAsync(filter, limits: limits);

            var assetsDto = _mapper.Map<IEnumerable<Asset>, IEnumerable<AssetDto>>(assets);
            return new AssetsResponse
            {
                Assets = assetsDto
            };
        }
    }
}