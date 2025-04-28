using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Trips
{
    public class TripRepository : ITripRepository
    {
        private readonly IDbContextFactory<RoveDbContext> _context;

        public TripRepository(IDbContextFactory<RoveDbContext> context)
        {
            _context = context;
        }

        #region Trip CRUD

        public async Task<GenericResponse<List<Trip>>> List(Expression<Func<Trip, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Trips
                    .Include(t => t.Driver)
                    .Include(t => t.Vehicle)
                    .Include(t => t.TripRequests)
                    .Include(t => t.Passengers)
                    .AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<List<Trip>>() { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<Trip>>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Trip>> GetById(long id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var trip = await context.Trips
                    .Include(t => t.Driver)
                    .Include(t => t.Vehicle)
                    .Include(t => t.TripRequests)
                    .Include(t => t.Passengers)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (trip == null)
                {
                    return new GenericResponse<Trip>() { Error = new ErrorResponse("Trip not found.") };
                }

                return new GenericResponse<Trip>() { Data = trip };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Trip>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Trip>> Add(Trip trip)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                context.Trips.Add(trip);
                await context.SaveChangesAsync();

                return new GenericResponse<Trip>() { Data = trip };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Trip>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Trip>> Update(Trip trip)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                context.Trips.Update(trip);
                await context.SaveChangesAsync();

                return new GenericResponse<Trip>() { Data = trip };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Trip>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Delete(long id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var trip = await context.Trips.FindAsync(id);

                if (trip == null)
                {
                    return new GenericResponse<bool>() { Error = new ErrorResponse("Trip not found.") };
                }

                context.Trips.Remove(trip);
                await context.SaveChangesAsync();

                return new GenericResponse<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Exists(Expression<Func<Trip, bool>> predicate)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var exists = await context.Trips.AnyAsync(predicate);

                return new GenericResponse<bool>() { Data = exists };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse(ex) };
            }
        }

        #endregion

    }
}
