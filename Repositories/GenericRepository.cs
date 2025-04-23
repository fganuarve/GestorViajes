using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories
{
    public class GenericRepository<T, TContext> : IGenericRepository<T>
        where T : class
        where TContext : DbContext
    {
        private readonly IDbContextFactory<TContext> _contextFactory;
        public GenericRepository(IDbContextFactory<TContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? expression = null)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var query = context.Set<T>().AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? expression = null)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var query = context.Set<T>().AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.FirstAsync();
        }

        public async Task AddAsync(T entity)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
