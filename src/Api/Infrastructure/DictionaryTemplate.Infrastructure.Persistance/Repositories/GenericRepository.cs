using DictionaryTemplate.Api.Application.Interfaces.Repositories;
using DictionaryTemplate.Api.Domain.Models;
using DictionaryTemplate.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DictionaryTemplate.Infrastructure.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext DbContext;

        /*
            will set the model coming into the repository.
            In this way, we will perform db operations on the related entity
        */
        protected DbSet<T> Entity => DbContext.Set<T>();

        public GenericRepository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual int Add(T entity)
        {
            Entity.Add(entity);
            return DbContext.SaveChanges();
        }

        public virtual int Add(IEnumerable<T> entities)
        {
            Entity.AddRange(entities);
            return DbContext.SaveChanges();
        }

        public virtual async Task<int> AddAsync(T entity)
        {
            await Entity.AddAsync(entity);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> AddAsync(IEnumerable<T> entities)
        {
            await Entity.AddRangeAsync(entities);
            return await DbContext.SaveChangesAsync();
        }

        public int AddOrUpdate(T entity)
        {
            if(!Entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                DbContext.Update(entity);

            return DbContext.SaveChanges();
        }

        public async Task<int> AddOrUpdateAsync(T entity)
        {
            if (!Entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                DbContext.Update(entity);

            return await DbContext.SaveChangesAsync();
        }

        public IQueryable<T> AsQueryable() => Entity.AsQueryable();

        public Task BulkAdd(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if(ids is null && ids.Any())
                return Task.CompletedTask;

            DbContext.RemoveRange(Entity.Where(i => ids.Contains(i.Id)));
            return DbContext.SaveChangesAsync();
        }

        public Task BulkUpdate(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual int Delete(T entity)
        {
            if(DbContext.Entry(entity).State == EntityState.Detached)
                Entity.Attach(entity);

            Entity.Remove(entity);
            return DbContext.SaveChanges();
        }

        public virtual int Delete(Guid id)
        {
            var entity = Entity.Find(id);
            return Delete(entity);
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entity.Attach(entity);

            Entity.Remove(entity);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = Entity.Find(id);
            return await DeleteAsync(entity);
        }

        public virtual bool DeleteRange(Expression<Func<T, bool>> predicate)
        {
            DbContext.RemoveRange(Entity.Where(predicate));
            return DbContext.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> predicate)
        {
            DbContext.RemoveRange(predicate);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Entity.AsQueryable();

            if(predicate is not null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query;
        }

        public Task<List<T>> GetAll(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            T found = await Entity.FindAsync(id);

            if (found is null)
                return null;

            if (noTracking)
                DbContext.Entry(found).State = EntityState.Detached;

            foreach (Expression<Func<T, object>> include in includes)
            {
                DbContext.Entry(found).Reference(include).Load();
            }

            return found;
        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> predicate, bool noTracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Entity;

            if(predicate is not null)
                query = query.Where(predicate);

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy is not null)
                query = orderBy(query);

            if(noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Entity;

            if(predicate is not null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }

        public virtual int Update(T entity)
        {
            Entity.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            return  DbContext.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            Entity.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            return await DbContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            if (includes is not null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }

            return query;
        }
    }
}
