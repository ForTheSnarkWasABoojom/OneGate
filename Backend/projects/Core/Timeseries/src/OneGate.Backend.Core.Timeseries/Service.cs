using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OneGate.Backend.Core.Timeseries.Database.Models;
using OneGate.Backend.Core.Timeseries.Database.Repository;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Backend.Transport.Contracts.Series.Point;
using OneGate.Backend.Transport.Dto.Series.Ohlc;
using OneGate.Backend.Transport.Dto.Series.Point;

namespace OneGate.Backend.Core.Timeseries
{
    public class Service : IService
    {
        private readonly IMapper _mapper;

        private readonly IOhlcSeriesRepository _ohlcSeries;
        private readonly IPointSeriesRepository _pointSeries;

        public Service(IOhlcSeriesRepository ohlcSeries, IPointSeriesRepository pointSeries, IMapper mapper)
        {
            _ohlcSeries = ohlcSeries;
            _pointSeries = pointSeries;
            _mapper = mapper;
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
            var series = request.Series.Range.Select(ohlc => new OhlcSeries
            {
                Low = ohlc.Low,
                High = ohlc.High,
                Open = ohlc.Open,
                Close = ohlc.Close,
                Timestamp = ohlc.Timestamp,
                Interval = request.Series.Interval.ToString(),
                AssetId = request.Series.AssetId,
                LastUpdate = DateTime.Now
            });
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
                    Range = _mapper.Map<List<OhlcDto>>(series),
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
            var series = request.Series.Range.Select(value =>
                new PointSeries
                {
                    LayoutId = request.Series.LayoutId,
                    AssetId = request.Series.AssetId,
                    Timestamp = value.Timestamp,
                    Value = value.Value
                });
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
                    Range = _mapper.Map<List<PointDto>>(series)
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