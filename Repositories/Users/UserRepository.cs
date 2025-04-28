using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Users
{
    public class UserRepository : IUserRepository

    {
        private readonly IDbContextFactory<RoveDbContext> _context;

        public UserRepository(IDbContextFactory<RoveDbContext> context)
        {
            _context = context;
        }

        #region User CRUD

        public async Task<GenericResponse<List<User>>> List(Expression<Func<User, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Users
                    .Include(u => u.TripRequests)
                    .Include(u => u.UserTrips)
                    .Include(u => u.Vehicles)
                    .Include(u => u.Trips)
                    .AsQueryable();

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

        public async Task<GenericResponse<User>> GetById(long id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var user = await context.Users
                    .Include(u => u.TripRequests)
                    .Include(u => u.UserTrips)
                    .Include(u => u.Vehicles)
                    .Include(u => u.Trips)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return new GenericResponse<User>() { Error = new ErrorResponse("User not found.") };
                }

                return new GenericResponse<User>() { Data = user };
            }
            catch (Exception ex)
            {
                return new GenericResponse<User>() { Error = new ErrorResponse(ex) };
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

        public async Task<GenericResponse<User>> Update(User user)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                context.Users.Update(user);
                await context.SaveChangesAsync();

                return new GenericResponse<User>() { Data = user };
            }
            catch (Exception ex)
            {
                return new GenericResponse<User>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Delete(long id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var user = await context.Users.FindAsync(id);

                if (user == null)
                {
                    return new GenericResponse<bool>() { Error = new ErrorResponse("User not found.") };
                }

                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return new GenericResponse<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse(ex) };
            }
        }

        #endregion
    }
}


