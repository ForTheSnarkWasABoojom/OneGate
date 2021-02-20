﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Timeseries.Api.Contracts.Series;

namespace OneGate.Backend.Core.Timeseries.Api.Client
{
    public interface ITimeseriesApiClient
    {
        public Task<IEnumerable<SeriesDto>> GetTimeseriesAsync(FilterSeriesDto request);
    }
}