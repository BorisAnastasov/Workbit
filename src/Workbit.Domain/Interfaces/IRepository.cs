using System.Linq.Expressions;

namespace Workbit.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        IQueryable<T> All(Expression<Func<T, bool>> search);
        IQueryable<T> AllReadOnly();
        IQueryable<T> AllReadOnly(Expression<Func<T, bool>> search);
        Task<int> CountAsync(Expression<Func<T, bool>>? filter);
        Task<T?> GetByIdAsync(object id);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task DeleteAsync(object id);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void DeleteRange(Expression<Func<T, bool>> filter);
        void Dispose();
    }
}
