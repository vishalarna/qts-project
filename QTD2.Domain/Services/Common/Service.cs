using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Service.Common;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Services.Common
{
    public class Service<TEntity> : IService<TEntity>
            where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IValidation<TEntity> _validation;

        private ValidationResult _validationResult;

        public Service(
            IRepository<TEntity> repository,
            IValidation<TEntity> validation)
        {
            _repository = repository;
            _validation = validation;
        }

        protected IRepository<TEntity> Repository
        {
            get { return _repository; }
        }

        protected ValidationResult ValidationResult
        {
            get { return _validationResult; }
        }

        public virtual async Task<TEntity> GetAsync(int id, bool noTracking = false)
        {
            return await _repository.GetAsync(id, noTracking);
        }

        public async Task<TEntity> GetWithIncludeAsync(int id, string[] includes)
        {
            return await _repository.GetWithIncludeAsync(id, includes);
        }
        public virtual async Task<IEnumerable<TEntity>> LatestWithIncludesAsync(string[] includes, int count)
        {
            if (includes == null || includes.Length == 0)
            {
                return Enumerable.Empty<TEntity>();
            }

            return await _repository.LatestWithIncludesAsync(includes, count);
        }

        public virtual async Task<IEnumerable<TEntity>> AllWithIncludeAsync(string[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                return Enumerable.Empty<TEntity>();
            }

            return await _repository.AllWithIncludeAsync(includes);
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _repository.AllAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(List<Expression<Func<TEntity, bool>>> predicates, bool noTracking = false)
        {
            return await _repository.FindAsync(predicates, noTracking);
        }

        public async Task<IEnumerable<TEntity>> FindWithIncludeAsync(List<Expression<Func<TEntity, bool>>> predicates, string[] includes, bool noTracking = false)
        {
            return await _repository.FindWithIncludeAsync(predicates, includes, noTracking);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            return await _repository.FindAsync(predicate, noTracking);
        }

        public async Task<IEnumerable<TEntity>> FindWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes)
        {
            return await _repository.FindWithIncludeAsync(predicate, includes);
        }

        public IQueryable<TEntity> FindQueryWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes, bool noTracking = false)
        {
            return _repository.FindQueryWithInclude(predicate, includes, noTracking);
        }

        public virtual Task<IEnumerable<TEntity>> FindDeletedWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes)
        {
            return _repository.FindDeletedWithIncludeAsync(predicate, includes);
        }

        public virtual async Task<ValidationResult> AddAsync(TEntity entity)
        {
            _validationResult = _validation.Valid(entity);

            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }

            await _repository.AddAsync(entity);
            return _validationResult;
        }

        public virtual async Task<ValidationResult> UpdateAsync(TEntity entity)
        {
            _validationResult = _validation.Valid(entity);

            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }

            await _repository.UpdateAsync(entity);
            return _validationResult;
        }

        public virtual async Task<ValidationResult> BulkUpdateAsync(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _validationResult = _validation.Valid(entity);

                if (!ValidationResult.IsValid)
                {
                    return ValidationResult;
                }
            }

            await _repository.BulkUpdateAsync(entities);
            return _validationResult;
        }

        public virtual async Task<ValidationResult> DeleteAsync(TEntity entity)
        {
            _validationResult = _validation.Valid(entity);

            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }

            await _repository.DeleteAsync(entity);
            return _validationResult;
        }

        public virtual async Task<IQueryable<TEntity>> GetSPResult(string sp, params object[] parameters)
        {
            /* Usage
             * GetSPResult(
             * "spGetProducts @bigCategoryId",
             * new SqlParameter("bigCategoryId", SqlDbType.BigInt)
             * { Value = categoryId });
             */

            return await Task.Run(() => _repository.ExecWithStoreProcedure(sp, parameters));
        }

        public virtual async Task<IQueryable<TEntity>> GetQueryResult(string sp, params object[] parameters)
        {
            /* Usage
             * GetQueryResult(
             * "SELECT * FROM FOO WHERE ID= @ID",
             * new SqlParameter("ID", SqlDbType.BigInt)
             * { Value = SOMEIDVALUE });
             */
            return await Task.Run(() => _repository.ExecWithRawQuery(sp, parameters));
        }

        public async Task<int> GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.GetCount(predicate);
        }

        public virtual IQueryable<TEntity> AllQueryWithInclude(string[] includes, bool noTracking = false)
        {
            return _repository.AllQueryWithInclude(includes, noTracking);
        }

        public virtual IQueryable<TEntity> AllQuery()
        {
            return _repository.AllQuery();
        }

        public virtual IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return _repository.FindQuery(predicate, noTracking);
        }
        public virtual async Task<IQueryable<TEntity>> FindQueryAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return _repository.FindQuery(predicate, noTracking);
        }

        public virtual IQueryable<TEntity> AllQueryWithIncludeAndDeleted(string[] includes)
        {
            return _repository.AllQueryWithIncludeAndDeleted(includes);
        }

        public async Task<int> AllQueryWitDeletedCount()
        {
            return await _repository.AllQueryWitDeleted().CountAsync();
        }

        public IQueryable<TEntity> FindQueryWithDeleted(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FindQueryWithDeleted(predicate);
        }

        public Task AddRangeAsync(List<TEntity> entities)
        {
            return _repository.AddRangeAsync(entities);
        }

        public virtual ValidationResult Validate(TEntity entity)
        {
            _validationResult = _validation.Valid(entity);
            return ValidationResult;
        }
    }
}
