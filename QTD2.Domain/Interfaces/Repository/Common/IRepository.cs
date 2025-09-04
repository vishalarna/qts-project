using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Repository.Common
{
    public interface IRepository<TEntity>
      where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<List<TEntity>> BulkUpdateAsync(List<TEntity> entity);

        Task AddRangeAsync(List<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task<TEntity> GetAsync(int id, bool noTracking);

        Task<TEntity> GetWithIncludeAsync(int id, string[] includes);

        Task<IEnumerable<TEntity>> LatestWithIncludesAsync(string[] includes, int count);

        Task<IEnumerable<TEntity>> AllWithIncludeAsync(string[] includes);
        
        IQueryable<TEntity> AllQueryWithInclude(string[] includes, bool noTracking = false);

        Task<IEnumerable<TEntity>> AllAsync();

        IQueryable<TEntity> AllQuery();

        Task<IEnumerable<TEntity>> FindAsync(List<Expression<Func<TEntity, bool>>> predicates, bool noTracking);

        Task<IEnumerable<TEntity>> FindWithIncludeAsync(List<Expression<Func<TEntity, bool>>> predicates, string[] includes, bool noTracking = false);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        Task<IQueryable<TEntity>> FindQueryAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        IQueryable<TEntity> FindQueryWithDeleted(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes);

        Task<IEnumerable<TEntity>> FindDeletedWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes); 

        IQueryable<TEntity> FindQueryWithInclude(Expression<Func<TEntity, bool>> predicate, string[] includes, bool noTracking = false);

        Task<IQueryable<TEntity>> FindQueryWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes, bool noTracking = false);

        IQueryable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters);

        IQueryable<TEntity> ExecWithRawQuery(string query, params object[] parameters);

        IQueryable<TEntity> AllQueryWithIncludeAndDeleted(string[] includes);

        Task<int> GetCount(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> AllQueryWitDeleted();
    }
}
