#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Persistence;
using QTD2.Domain.Entities.Common;

namespace QTD2.Data.Persistence
{
    public class GenericRepository<TDbContext, TEntity> : IGenericRepository<TEntity> 
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public GenericRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Query(params string[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        
        public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = default)
        {
            if (predicate is null)
            {
                return _dbContext.Set<TEntity>().CountAsync();
            }
            
            return _dbContext.Set<TEntity>().CountAsync(predicate);
        }

        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.FirstOrDefaultAsync(predicate)!;
        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = default, params string[] includes)
        {
            var query = predicate is null ? _dbContext.Set<TEntity>() : _dbContext.Set<TEntity>().Where(predicate);
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.ToListAsync();
        }

        public Task AddAsync(TEntity entity)
        {
            return _dbContext.Set<TEntity>().AddAsync(entity).AsTask();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity? entity)
        {
            if (entity is null)
            {
                return;
            }

            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}