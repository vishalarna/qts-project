using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Common;
using QTD2.Domain.Interfaces.Repository.Common;

namespace QTD2.Data.Repository.Common
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable
         where TEntity : Entity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;

            _dbSet = _dbContext.Set<TEntity>();
        }

        protected DbContext Context
        {
            get { return _dbContext; }
        }

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            var cs = Context.Database.GetDbConnection().ConnectionString;

            await Context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(int id, bool noTracking = false)
        {
            if (noTracking)
            {
                return await DbSet.AsNoTracking().Where(r => r.Id == id).FirstOrDefaultAsync();
            }
            else
            {
                return await DbSet.Where(r => r.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<TEntity> GetWithIncludeAsync(int id, string[] includes)
        {
            var query = DbSet.Include(includes.First());
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            return await query.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                entry.State = EntityState.Unchanged;
                throw e;
            }

            return entity;
        }

        public virtual async Task<List<TEntity>> BulkUpdateAsync(List<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                var entry = Context.Entry(entity);
                DbSet.Attach(entity);
                entry.State = EntityState.Modified;
            }           

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                foreach (var entity in entities)
                {
                    var entry = Context.Entry(entity);
                    entry.State = EntityState.Unchanged;
                }
               
                throw e;
            }

            return entities;
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> LatestWithIncludesAsync(string[] includes, int count)
        {
            IQueryable<TEntity> query = DbSet.Include(includes.First());

            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            return await query.OrderByDescending(r => r.CreatedDate).Take(count).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> AllWithIncludeAsync(string[] includes)
        {
            IQueryable<TEntity> query = DbSet.Include(includes.First());

            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindWithIncludeAsync(List<Expression<Func<TEntity, bool>>> predicates, string[] includes, bool noTracking = false)
        {
            IQueryable<TEntity> query = DbSet.Include(includes.First());
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }

            if(noTracking)
            {
                query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(List<Expression<Func<TEntity, bool>>> predicates, bool noTracking = false)
        {
            IQueryable<TEntity> query = DbSet;

            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }

            if (noTracking)
            {
                query.AsNoTracking();
            }

            var sql = query.ToQueryString();

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            if (noTracking)
            {
                var cs = Context.Database.GetDbConnection().ConnectionString;
                return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
            }
            else
            {
                return await DbSet.Where(predicate).ToListAsync();
            }
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes)
        {           
            IQueryable<TEntity> query = DbSet.Include(includes.First());
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }
            var cs = Context.Database.GetDbConnection().ConnectionString;
            return await query.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindDeletedWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes)
        {
            IQueryable<TEntity> query = DbSet.IgnoreQueryFilters().Include(includes.First());
            foreach(var include in includes.Skip(1))
            {
                query = query.Include(include);
            }
            var cs = Context.Database.GetDbConnection().ConnectionString;
            return await query.Where(predicate).Where(s => s.Deleted == true).ToListAsync();
        }

        public virtual IQueryable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw($"exec {query}", parameters);
        }

        public virtual async Task<int> GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).CountAsync();
        }

        public virtual IQueryable<TEntity> AllQueryWithInclude(string[] includes, bool noTracking = false)
        {
            IQueryable<TEntity> query;
            if (noTracking)
            {
                query = DbSet.AsNoTracking().Include(includes.First());
            }
            else
            {
                query = DbSet.Include(includes.First());
            }

            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            return query;
        }

        public virtual IQueryable<TEntity> AllQuery()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> AllQueryWitDeleted()
        {
            return DbSet.AsQueryable().IgnoreQueryFilters();
        }

        public virtual IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            if (noTracking)
            {
                return DbSet.AsNoTracking().Where(predicate);
            }
            else
            {
                return DbSet.Where(predicate).Where(x => !x.Deleted);
            }
        }

        public virtual IQueryable<TEntity> FindQueryWithInclude(Expression<Func<TEntity, bool>> predicate, string[] includes, bool noTracking = false)
        {
            IQueryable<TEntity> query;
            if (noTracking)
            {
                query = DbSet.AsNoTracking().Include(includes.First());
            }
            else
            {
                query = DbSet.Include(includes.First());
            }
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            return query.Where(predicate);
        }
        public virtual async Task<IQueryable<TEntity>> FindQueryWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, string[] includes, bool noTracking = false)
        {
            IQueryable<TEntity> query;
            if (noTracking)
            {
                query = DbSet.AsNoTracking().Include(includes.First());
            }
            else
            {
                query = DbSet.Include(includes.First());
            }
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            return query.Where(predicate);
        }

        public virtual IQueryable<TEntity> ExecWithRawQuery(string query, params object[] parameters)
        {
            return DbSet.FromSqlRaw(query, parameters);
        }

        public virtual IQueryable<TEntity> AllQueryWithIncludeAndDeleted(string[] includes)
        {
            IQueryable<TEntity> query = DbSet.Include(includes.FirstOrDefault()).IgnoreQueryFilters();

            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            return query;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (Context == null)
            {
                return;
            }

            Context.Dispose();
        }

        public IQueryable<TEntity> FindQueryWithDeleted(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.IgnoreQueryFilters().Where(predicate);
        }

        public async Task<IQueryable<TEntity>> FindQueryAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            if (noTracking)
            {
                return DbSet.AsNoTracking().Where(predicate);
            }
            else
            {
                return DbSet.Where(predicate).Where(x => !x.Deleted);
            }
        }


        public async Task AddRangeAsync(List<TEntity> entites)
        {
            DbSet.AddRange(entites);
            await Context.SaveChangesAsync();
        }
    }
}
