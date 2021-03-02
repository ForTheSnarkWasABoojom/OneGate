using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public interface ISeriesRepository : IRepository<Series>
    {
        public Task<IEnumerable<Series>> AddOrUpdateAsync(IEnumerable<Series> entity, DateTime createdAt = default);
    }
}