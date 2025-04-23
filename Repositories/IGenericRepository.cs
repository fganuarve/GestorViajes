using System.Linq.Expressions;

namespace GestorViajes.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? expression = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
