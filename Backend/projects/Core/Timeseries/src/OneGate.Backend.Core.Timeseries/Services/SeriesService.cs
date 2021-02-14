using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Timeseries.Contracts;
using OneGate.Backend.Core.Timeseries.Contracts.Series;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;

namespace OneGate.Backend.Core.Timeseries.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly ISeriesRepository _series;
        private readonly IMapper _mapper;
        
        public SeriesService(ISeriesRepository series, IMapper mapper)
        {
            _series = series;
            _mapper = mapper;
        }

        public async Task<SeriesResponse> GetSeriesAsync(GetSeries request)
        {
            Expression<Func<Series, bool>> filter = p => p.LayerId == request.LayoutId;
            var limits = new QueryLimits(request.Shift, request.Count);

            if (request.StartTimestamp != null)
                filter.And(p => p.Timestamp >= request.StartTimestamp);
            
            if (request.EndTimestamp != null)
                filter.And(p => p.Timestamp <= request.EndTimestamp);
            
            var series = await _series.FilterAsync(filter, limits: limits);

            var seriesDto = _mapper.Map<IEnumerable<Series>, IEnumerable<SeriesDto>>(series);
            return new SeriesResponse
            {
                Series = seriesDto
            };
        }
    }
}