using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Persistence;
using QTD2.Domain.Entities.Common;

namespace QTD2.Data.Persistence
{
    public class GenericUnitOfWork<TDbContext> : IUnityOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly Hashtable _repositories = new();

        public GenericUnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<,>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof(TDbContext), typeof(TEntity)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            if (_repositories[type] is not GenericRepository<TDbContext, TEntity> repository)
            {
                throw new InvalidOperationException("Could not create a repository");
            }
        
            return repository;
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}