using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Workbit.Domain.Interface;
using Workbit.Infrastructure.Persistance;

namespace Workbit.Infrastructure.Database.Repository
{
    public class Repository<T>(WorkbitDbContext context) : IRepository<T> where T : class
    {
        protected DbSet<T> _dbSet = context.Set<T>();

        public IQueryable<T> All()
        {
            return _dbSet;
        }
        public IQueryable<T> AllReadOnly()
        {
            return _dbSet.AsNoTracking();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public IQueryable<T> All(Expression<Func<T, bool>> search)
        {
            return _dbSet.Where(search);
        }
        public IQueryable<T> AllReadOnly(Expression<Func<T, bool>> search)
        {
            return _dbSet.Where(search).AsNoTracking();
        }
        public async Task DeleteAsync(object id)
        {
            T entity = (await GetByIdAsync(id))!;

            Delete(entity);
        }
        public void Delete(T entity)
        {
            EntityEntry entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public void DeleteRange(Expression<Func<T, bool>> deleteWhereClause)
        {
            var entities = All(deleteWhereClause);
            DeleteRange(entities);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter)
        {
            return filter == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(filter);
        }
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }
    }
}
