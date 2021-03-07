using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public interface IOhlcRepository : IRepository<Ohlc>
    {
        public Task<List<Ohlc>> AddOrUpdateRangeAsync(IEnumerable<Ohlc> entity, DateTime createdAt = default);
    }
}