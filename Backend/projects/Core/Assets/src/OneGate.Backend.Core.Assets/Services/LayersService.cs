using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Assets.Contracts.Layer;
using OneGate.Backend.Core.Assets.Database.Models;
using OneGate.Backend.Core.Assets.Database.Repository;
using OneGate.Backend.Core.Base.Linq;

namespace OneGate.Backend.Core.Assets.Services
{
    public class LayerService : ILayerService
    {
        private readonly ILayerRepository _layouts;
        private readonly IMapper _mapper;

        public LayerService(ILayerRepository layouts, IMapper mapper)
        {
            _layouts = layouts;
            _mapper = mapper;
        }

        public async Task<LayersResponse> GetLayersAsync(GetLayers request)
        {
            Expression<Func<Layer, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter.FilterBy(p => p.Id == request.Id, request.Id);
            
            var layouts = await _layouts.FilterAsync(filter, limits: limits);

            var layoutsDto = _mapper.Map<IEnumerable<Layer>, IEnumerable<LayersDto>>(layouts);
            return new LayersResponse
            {
                Layers = layoutsDto
            };
        }
    }
}