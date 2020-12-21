using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public interface IOhlcSeriesRepository
    {
        public Task AddAsync(IEnumerable<OhlcSeries> model);
        public Task UpsertAsync(OhlcSeries model);

        public Task<IEnumerable<OhlcSeries>> FilterAsync(int? id, string interval, int assetId,
            DateTime? endTimestamp, DateTime? startTimestamp, int shift, int count);

        public Task RemoveAsync(string interval, int assetId, DateTime? endTimestamp,
            DateTime? startTimestamp, int shift, int count);
    }
}