using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GestorViajes.Models;
using GestorViajes.Models.EFCore;

namespace GestorViajes.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<VetAppDbContext> _context;

        public UserRepository(IDbContextFactory<VetAppDbContext> context)
        {
            _context = context;
        }
        public async Task<GenericResponse<List<User>>> List(Expression<Func<User, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Users.Include(u => u.Pets).AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                return new GenericResponse<List<User>> { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<User>> { Error = new ErrorResponse(ex) };
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

        public async Task<GenericResponse<User>> Delete(string id, bool? hardDelete = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var user = await context.Users.FindAsync(id);
                if (user == null)
                {
                    return new GenericResponse<User>() { Error = new ErrorResponse($"No se ha encontrado el usuario con ID {id}") };
                }

                // soft delete
                var shouldHardDelete = hardDelete ?? false;
                if (shouldHardDelete)
                {
                    if (user.Pets.Any())
                    {
                        return new GenericResponse<User> { Error = new ErrorResponse("El usuario tiene mascotas asociadas. No se ha borrado") };
                    }
                    context.Users.Remove(user);
                }
                else
                {
                    user.Active = false;
                    context.Entry(user).State = EntityState.Modified;
                }

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
                    return new GenericResponse<User>() { Error = new ErrorResponse($"No se ha encontrado el usuario con ID {user.Id}") };
                }
                context.Entry(entity).CurrentValues.SetValues(user);

                await context.SaveChangesAsync();

                return new GenericResponse<User> { Data = user };
            }
            catch (Exception ex)
            {
                return new GenericResponse<User> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<User>> UserExists(string nationalId)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var user = await context.Users.FirstOrDefaultAsync(u => u.NationalId == nationalId);

                if (user == null)
                {
                    return new GenericResponse<User>
                    {
                        Error = new ErrorResponse($"No se ha encontrado el usuario con DNI {nationalId}")
                    };
                }
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
                //No podría incluir Pets si no fuese por el Property Navigation Pet en User
                var query = context.Users.Include(u => u.Pets).AsQueryable();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                return new GenericResponse<User> { Data = await query.FirstOrDefaultAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<User> { Error = new ErrorResponse(ex) };
            }
        }
    }
}
