using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;
using OneGate.Backend.Transport.Contracts.Timeseries;

namespace OneGate.Backend.Core.Timeseries.Worker.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly IOhlcRepository _ohlcs;
        private readonly IMapper _mapper;

        public SeriesService(IMapper mapper, IOhlcRepository ohlcs)
        {
            _mapper = mapper;
            _ohlcs = ohlcs;
        }

        public async Task UpdateMarketDataAsync(MarketDataUpdated request)
        {
            var ohlcRange = _mapper.Map<IEnumerable<Ohlc>>(request.Ohlc).ToList();
            ohlcRange.ForEach(p => p.AssetId = request.AssetId);
            
            await _ohlcs.AddOrUpdateRangeAsync(ohlcRange, request.CreatedAt);
        }
    }
}