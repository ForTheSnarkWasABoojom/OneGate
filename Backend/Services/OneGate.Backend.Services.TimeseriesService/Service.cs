using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Contracts.Common;
using OneGate.Backend.Contracts.OhlcTimeseries;
using OneGate.Backend.Contracts.ValueTimeseries;
using OneGate.Backend.Services.TimeseriesService.Repository;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Services.TimeseriesService
{
    public class Service : ITimeseriesService
    {
        private readonly IOhlcTimeseriesRepository _ohlcTimeseries;
        private readonly IValueTimeseriesRepository _valueTimeseries;

        public Service(IOhlcTimeseriesRepository ohlcTimeseries, IValueTimeseriesRepository valueTimeseries)
        {
            _ohlcTimeseries = ohlcTimeseries;
            _valueTimeseries = valueTimeseries;
        }

        public async Task OnOhlcTimeseriesUpdated(OnOhlcTimeseriesUpdated request)
        {
            foreach (var (intervalDto, ohlcDto) in request.Ohlcs)
            {
                await _ohlcTimeseries.UpsertRangeAsync(new OhlcTimeseriesRangeDto
                {
                    AssetId = request.AssetId,
                    Interval = intervalDto,
                    Range = new List<OhlcTimeseriesDto>
                    {
                        ohlcDto
                    }
                });
            }
        }

        public async Task<SuccessResponse> CreateOhlcTimeseries(CreateOhlcTimeseries request)
        {
            await _ohlcTimeseries.AddRangeAsync(request.Ohlcs);
            return new SuccessResponse();
        }

        public async Task<OhlcTimeseriesResponse> GetOhlcTimeseriess(GetOhlcTimeseries request)
        {
            return new OhlcTimeseriesResponse
            {
                Ohlcs = await _ohlcTimeseries.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeleteOhlcTimeseries(DeleteOhlcTimeseries request)
        {
            await _ohlcTimeseries.RemoveRangeAsync(request.Filter);
            return new SuccessResponse();
        }

        public async Task<SuccessResponse> CreateValueTimeseries(CreateValueTimeseries request)
        {
            await _valueTimeseries.AddRangeAsync(request.Values);
            return new SuccessResponse();
        }

        public async Task<ValueTimeseriesResponse> GetValueTimeseries(GetValueTimeseries request)
        {
            return new ValueTimeseriesResponse
            {
                Values = await _valueTimeseries.FilterAsync(request.Filter)
            };
        }

        public async Task<SuccessResponse> DeleteValueTimeseries(DeleteValueTimeseries request)
        {
            await _valueTimeseries.RemoveRangeAsync(request.Filter);
            return new SuccessResponse();
        }
    }
}