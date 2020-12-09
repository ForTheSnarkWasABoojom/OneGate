using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Series.Service.Repository;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Backend.Transport.Contracts.Series.Point;
using OneGate.Common.Models.Series.Ohlc;

namespace OneGate.Backend.Core.Series.Service
{
    public class SeriesService : ISeriesService
    {
        private readonly IOhlcSeriesRepository _ohlcSeries;
        private readonly IPointSeriesRepository _pointSeries;

        public SeriesService(IOhlcSeriesRepository ohlcSeries, IPointSeriesRepository pointSeries)
        {
            _ohlcSeries = ohlcSeries;
            _pointSeries = pointSeries;
        }

        public async Task OnOhlcSeriesUpdated(OnOhlcSeriesUpdated request)
        {
            foreach (var (intervalDto, ohlcDto) in request.Data)
            {
                await _ohlcSeries.UpsertAsync(new OhlcSeriesDto
                {
                    AssetId = request.AssetId,
                    Interval = intervalDto,
                    Range = new List<OhlcDto>
                    {
                        ohlcDto
                    }
                });
            }
        }

        public async Task<SuccessResponse> CreateOhlcSeries(CreateOhlcSeries request)
        {
            await _ohlcSeries.AddAsync(request.Series);
            return new SuccessResponse();
        }

        public async Task<OhlcSeriesResponse> GetOhlcSeries(GetOhlcSeries request)
        {
            return new OhlcSeriesResponse
            {
                Series = await _ohlcSeries.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeleteOhlcSeries(DeleteOhlcSeries request)
        {
            await _ohlcSeries.RemoveAsync(request.Filter);
            return new SuccessResponse();
        }

        public async Task<SuccessResponse> CreatePointSeries(CreatePointSeries request)
        {
            await _pointSeries.AddAsync(request.Series);
            return new SuccessResponse();
        }

        public async Task<PointSeriesResponse> GetPointSeries(GetPointSeries request)
        {
            return new PointSeriesResponse
            {
                Series = await _pointSeries.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeletePointSeries(DeletePointSeries request)
        {
            await _pointSeries.RemoveAsync(request.Filter);
            return new SuccessResponse();
        }
    }
}