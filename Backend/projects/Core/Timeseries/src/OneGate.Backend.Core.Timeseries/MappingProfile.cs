using System.Collections.Generic;
using AutoMapper;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Backend.Transport.Dto.Series.Ohlc;

namespace OneGate.Backend.Core.Timeseries
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OhlcSeries, OhlcDto>();
            CreateMap<OhlcDto, OhlcSeries>();
            
            CreateMap<OhlcSeries, CreateOhlcSeries>();
            CreateMap<CreateOhlcSeries, OhlcSeries>();

            CreateMap<IEnumerable<OhlcSeries>, IEnumerable<CreateOhlcSeries>>();
            CreateMap<IEnumerable<CreateOhlcSeries>, IEnumerable<OhlcSeries>>();
        }
    }
}