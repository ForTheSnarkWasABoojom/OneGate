using AutoMapper;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Series;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMapForSeries();
        }

        private void CreateMapForSeries()
        {
            CreateMap<Series, SeriesDto>()
                .IncludeAllDerived();

            CreateMap<OhlcSeries, OhlcSeriesDto>();
            CreateMap<PointSeries, PointSeriesDto>();
        }
    }
}