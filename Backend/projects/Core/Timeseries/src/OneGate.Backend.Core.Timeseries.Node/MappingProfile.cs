using System.Collections.Generic;
using System.Drawing;
using AutoMapper;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Common.Models.Series.Ohlc;
using OneGate.Common.Models.Series.Point;

namespace OneGate.Backend.Core.Timeseries.Node
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Point, PointDto>();
            CreateMap<PointDto, Point>();

            CreateMap<OhlcSeries, OhlcDto>();
            CreateMap<OhlcDto, OhlcSeries>();
            
            CreateMap<OhlcSeries, CreateOhlcSeries>();
            CreateMap<CreateOhlcSeries, OhlcSeries>();
            
            
            CreateMap<IEnumerable<OhlcSeries>, IEnumerable<CreateOhlcSeries>>();
            CreateMap<IEnumerable<CreateOhlcSeries>, IEnumerable<OhlcSeries>>();
            
        }
    }
}