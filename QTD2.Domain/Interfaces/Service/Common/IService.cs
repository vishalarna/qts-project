using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Interfaces.Service.Common
{
    public interface IService<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetAsync(int id, bool noTracking = false);

        Task<TEntity> GetWithIncludeAsync(int id, string[] includes);

        Task AddRangeAsync(List<TEntity> entities);

        Task<IEnumerable<TEntity>> AllAsync();

        Task<IEnumerable<TEntity>> LatestWithIncludesAsync(string[] includes, int count);      

        Task<IEnumerable<TEntity>> AllWithIncludeAsync(string[] includes);

        Task<IEnumerable<TEntity>> FindAsync(List<Expression<Func<TEntity, bool>>> predicates, bool noTracking = false);

        Task<IEnumerable<TEntity>> FindWithIncludeAsync(List<Expression<Func<TEntity, bool>>> predicates, string[] includes, bool noTracking = false);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        Task<IEnumerable<TEntity>> FindWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes);
        Task<IEnumerable<TEntity>> FindDeletedWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes);

        Task<ValidationResult> AddAsync(TEntity entity);

        Task<ValidationResult> UpdateAsync(TEntity entity);

        Task<ValidationResult> BulkUpdateAsync(List<TEntity> entity);

        Task<ValidationResult> DeleteAsync(TEntity entity);

        Task<IQueryable<TEntity>> GetSPResult(string sp, params object[] parameters);

        Task<IQueryable<TEntity>> GetQueryResult(string sp, params object[] parameters);

        Task<int> GetCount(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindQueryWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes, bool noTracking = false);

        IQueryable<TEntity> AllQueryWithInclude(string[] includes, bool noTracking = false);

        IQueryable<TEntity> AllQueryWithIncludeAndDeleted(string[] includes);

        IQueryable<TEntity> AllQuery();

        IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        Task<IQueryable<TEntity>> FindQueryAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        IQueryable<TEntity> FindQueryWithDeleted(Expression<Func<TEntity, bool>> predicate);

        Task<int> AllQueryWitDeletedCount();
        ValidationResult Validate(TEntity entity);
    }
}
