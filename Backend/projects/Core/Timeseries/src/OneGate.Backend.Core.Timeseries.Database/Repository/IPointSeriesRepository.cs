using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public interface IPointSeriesRepository
    {
        public Task AddAsync(IEnumerable<PointSeries> request);
        public Task<IEnumerable<PointSeries>> FilterAsync(
            int? id, int layoutId, int assetId,
            DateTime? endTimestamp, DateTime? startTimestamp, int shift, int count);
        public Task RemoveAsync(int layoutId, int assetId,
            DateTime? endTimestamp, DateTime? startTimestamp, int shift, int count);
    }
}