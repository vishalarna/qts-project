using System;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Persistence
{
    /// <summary>
    /// Generic Unit of Work
    /// </summary>
    public interface IUnityOfWork : IDisposable
    {
        /// <summary>
        /// Creates a new repository instance for the specified entity if it was not in the cache
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <returns>An instance of generic repository for the specified entity</returns>
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Save changes to database
        /// </summary>
        /// <returns>Number of rows modified after save changes.</returns>
        Task<int> SaveChangesAsync();
    }
}
