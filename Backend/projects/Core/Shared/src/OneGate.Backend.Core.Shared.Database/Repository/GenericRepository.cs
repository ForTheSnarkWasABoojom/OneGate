using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OneGate.Backend.Core.Shared.Database.Repository
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(DbContext context, DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
            _context = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            var entity = await _dbSet.Where(filter).AsNoTracking().FirstOrDefaultAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, QueryLimits limits = default)
        {
            var query = _dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);
            
            return await query.Skip(limits.Shift).Take(limits.Count).AsNoTracking().ToArrayAsync();
        }

        public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> filter)
        {
            _dbSet.RemoveRange(_dbSet.Where(filter));
            await _context.SaveChangesAsync();
        }
    }
}