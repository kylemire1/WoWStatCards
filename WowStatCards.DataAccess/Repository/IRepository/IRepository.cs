using System.Linq.Expressions;

namespace WowStatCards.DataAccess.Repository.IRepository
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task CreateAsync(T entity);
        Task<T> RemoveAsync(T entity);
        Task SaveAsync();
    }
}
