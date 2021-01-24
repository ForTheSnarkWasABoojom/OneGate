using System.Linq;
using System.Threading.Tasks;
using OneGate.Backend.Core.Timeseries.Converters;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Timeseries.Ohlc;
using OneGate.Backend.Transport.Contracts.Timeseries.Point;
using OneGate.Backend.Transport.Dto.Series.Ohlc;
using OneGate.Backend.Transport.Dto.Series.Point;

namespace OneGate.Backend.Core.Timeseries.Services
{
    public class Service : IService
    {
        private readonly IConverter _converter;
        private readonly IOhlcSeriesRepository _ohlcSeries;
        private readonly IPointSeriesRepository _pointSeries;

        public Service(IOhlcSeriesRepository ohlcSeries, IPointSeriesRepository pointSeries, IConverter converter)
        {
            _ohlcSeries = ohlcSeries;
            _pointSeries = pointSeries;
            _converter = converter;
        }

        public async Task OnOhlcSeriesUpdated(OnOhlcSeriesUpdated request)
        {
            foreach (var (intervalDto, ohlcDto) in request.Data)
            {
                await _ohlcSeries.UpsertAsync(new OhlcSeries
                {
                    AssetId = request.AssetId,
                    Interval = intervalDto.ToString(),
                    Low = ohlcDto.Low,
                    High = ohlcDto.High,
                    Open = ohlcDto.Open,
                    Close = ohlcDto.Close
                });
            }
        }

        public async Task<SuccessResponse> CreateOhlcSeriesAsync(CreateOhlcSeries request)
        {
            var series = request.Series.Range.Select(_converter.FromDto);
            await _ohlcSeries.AddAsync(series);
            return new SuccessResponse();
        }

        public async Task<OhlcSeriesResponse> GetOhlcSeriesAsync(GetOhlcSeries request)
        {
            var series = await _ohlcSeries.FilterAsync(request.Filter.Id, request.Filter.Interval.ToString(),
                request.Filter.AssetId,
                request.Filter.EndTimestamp, request.Filter.StartTimestamp, request.Filter.Shift,
                request.Filter.Count);
            
            return new OhlcSeriesResponse
            {
                Series = new OhlcSeriesDto
                {
                    AssetId = request.Filter.AssetId,
                    Range = series.Select(_converter.ToDto).ToList(),
                    Interval = request.Filter.Interval
                }
            };
        }

        public async Task<SuccessResponse> DeleteOhlcSeriesAsync(DeleteOhlcSeries request)
        {
            await _ohlcSeries.RemoveAsync(request.Filter.Interval.ToString(), request.Filter.AssetId,
                request.Filter.EndTimestamp,
                request.Filter.StartTimestamp, request.Filter.Shift, request.Filter.Count);
            return new SuccessResponse();
        }

        public async Task<SuccessResponse> CreatePointSeriesAsync(CreatePointSeries request)
        {
            var series = request.Series.Range.Select(_converter.FromDto);
            await _pointSeries.AddAsync(series);
            return new SuccessResponse();
        }

        public async Task<PointSeriesResponse> GetPointSeriesAsync(GetPointSeries request)
        {
            var series = await _pointSeries.FilterAsync(request.Filter.Id, request.Filter.LayoutId,
                request.Filter.AssetId,
                request.Filter.EndTimestamp, request.Filter.StartTimestamp, request.Filter.Shift,
                request.Filter.Count);
            return new PointSeriesResponse
            {
                Series = new PointSeriesDto
                {
                    LayoutId = request.Filter.LayoutId,
                    AssetId = request.Filter.AssetId,
                    Range = series.Select(_converter.ToDto).ToList()
                }
            };
        }

        public async Task<SuccessResponse> DeletePointSeriesAsync(DeletePointSeries request)
        {
            await _pointSeries.RemoveAsync(request.Filter.LayoutId, request.Filter.AssetId,
                request.Filter.EndTimestamp, request.Filter.StartTimestamp, request.Filter.Shift, request.Filter.Count);
            return new SuccessResponse();
        }
    }
}