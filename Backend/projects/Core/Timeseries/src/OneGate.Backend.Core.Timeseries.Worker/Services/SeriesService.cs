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
        private readonly ILayerRepository _layers;
        private readonly ISeriesRepository _series;
        private readonly IMapper _mapper;

        public SeriesService(IMapper mapper, ILayerRepository layers, ISeriesRepository series)
        {
            _mapper = mapper;
            _layers = layers;
            _series = series;
        }

        public async Task UpdateMarketDataAsync(MarketDataUpdated request)
        {
            var masterLayer = await _layers.FindMasterAsync(request.AssetId);
            
            var ohlcRange = _mapper.Map<IEnumerable<OhlcSeries>>(request.Ohlc).ToList();
            ohlcRange.ForEach(p => p.LayerId = masterLayer.Id);
            
            await _series.AddOrUpdateAsync(ohlcRange, request.CreatedAt);
        }
    }
}