using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Base.Linq;
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
            Expression<Func<Series, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter
                .FilterBy(p => p.LayerId == request.LayoutId)
                .FilterBy(p => p.Timestamp == request.StartTimestamp, request.StartTimestamp)
                .FilterBy(p => p.Timestamp == request.EndTimestamp, request.EndTimestamp);

            var series = await _series.FilterAsync(filter, limits: limits);

            var seriesDto = _mapper.Map<IEnumerable<Series>, IEnumerable<SeriesDto>>(series);
            return new SeriesResponse
            {
                Series = seriesDto
            };
        }
    }
}