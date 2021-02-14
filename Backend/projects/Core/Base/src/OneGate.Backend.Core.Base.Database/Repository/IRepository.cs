using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneGate.Backend.Core.Base.Database.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> AddAsync(TEntity entity);
        public Task AddRangeAsync(IEnumerable<TEntity> entities);
        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        public Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, QueryLimits limits = default);
        public Task RemoveAsync(Expression<Func<TEntity, bool>> filter);
    }
}