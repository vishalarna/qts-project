#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Persistence
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="TEntity">Class that implements the <see cref="IEntity"/> interface</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets the reference of the IQueryable instance
        /// </summary>
        /// <param name="includes">List of child entities to load</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(params string[] includes);

        /// <summary>
        /// Asynchronously counts number of entities.
        /// </summary>
        /// <param name="predicate">Predicate to filter query</param>
        /// <returns>Number of elements that satisfies the specified condition</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = default);

        /// <summary>
        /// Gets an entity object filtered by predicate.
        /// </summary>
        /// <param name="predicate">Predicate to filter query</param>
        /// <param name="includes">List of child entities to load</param>
        /// <returns>Entity object</returns>
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        /// <summary>
        /// Gets a list of the entity objects
        /// </summary>
        /// <param name="predicate">Predicate to filter query</param>
        /// <param name="includes">List of child entities to load</param>
        /// <returns>List of entity objects</returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = default, params string[] includes);

        /// <summary>
        /// Adds new entry to database.
        /// </summary>
        /// <param name="entity">Entity object</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates existing entry.
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes entity object from database.
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Delete(TEntity? entity);
    }
}