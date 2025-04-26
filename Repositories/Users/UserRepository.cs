using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GestorViajes.Models;
using GestorViajes.Models.EFCore;
using GestorViajes.Models.ViewModels.User;

namespace GestorViajes.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<VetAppDbContext> _context;

        public UserRepository(IDbContextFactory<VetAppDbContext> context)
        {
            _context = context;
        }

        #region User

        public async Task<GenericResponse<List<User>>> List(Expression<Func<User, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Users.AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<List<User>>() { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<User>>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<User>> Add(User user)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                context.Users.Add(user);
                await context.SaveChangesAsync();

                return new GenericResponse<User>() { Data = user };
            }
            catch (Exception ex)
            {
                return new GenericResponse<User>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<User>> Edit(User user)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var entity = await context.Users.FindAsync(user.Id);

                if (entity == null)
                {
                    return new GenericResponse<User> { Error = new ErrorResponse("Usuario no encontrado") };
                }

                context.Entry(entity).CurrentValues.SetValues(user);
                context.Entry(entity).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return new GenericResponse<User> { Data = entity };
            }
            catch (Exception ex)
            {
                return new GenericResponse<User> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<User>> Delete(int id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var user = await context.Users.FindAsync(id);

                if (user == null)
                {
                    return new GenericResponse<User>() { Error = new ErrorResponse($"No se ha encontrado el usuario con ID {id}") };
                }

                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return new GenericResponse<User> { Data = user };
            }
            catch (Exception ex)
            {
                return new GenericResponse<User> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<User>> Get(Expression<Func<User, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Users.AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                var resultado = await query.FirstOrDefaultAsync();
                if (resultado == null)
                {
                    return new GenericResponse<User> { Error = new ErrorResponse("Usuario no encontrado") };
                }

                return new GenericResponse<User> { Data = resultado };
            }
            catch (Exception ex)
            {
                return new GenericResponse<User> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Exists(Expression<Func<User, bool>> predicate)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var exists = await context.Users.AnyAsync(predicate);

                return new GenericResponse<bool> { Data = exists };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }

        #endregion
    }
}
