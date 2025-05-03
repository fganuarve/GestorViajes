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

                return new GenericResponse<List<Trip>>() { Data = await query.OrderByDescending(x=> x.CreatedAt).ToListAsync() };
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
                    .Include(t => t.Passengers).ThenInclude(x => x.User)
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
                trip.Status = TripStatus.Cancelled;
				trip.Active = false;
				context.Trips.Update(trip);
				await context.SaveChangesAsync();

                return new GenericResponse<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse(ex) };
            }
        }

		public async Task<GenericResponse<bool>> ForceDelete(long id)
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
		#region Trip passengers
		public async Task<GenericResponse<UserTrip>> AddPassenger(UserTrip usertrip)
		{
			try
			{
				await using var context = await _context.CreateDbContextAsync();
				context.UserTrips.Add(usertrip);
				await context.SaveChangesAsync();

				return new GenericResponse<UserTrip>() { Data = usertrip };
			}
			catch (Exception ex)
			{
				return new GenericResponse<UserTrip>() { Error = new ErrorResponse(ex) };
			}
		}

		public async Task<GenericResponse<UserTrip>> DeletePassenger(long tripId, long userId)
		{
			try
			{
				await using var context = await _context.CreateDbContextAsync();
                var usertrip = context.UserTrips.FirstOrDefault(x => x.TripId == tripId && x.UserId == userId);
                if(usertrip == null)
                {
					return new GenericResponse<UserTrip>() { Error = new ErrorResponse("User trip not found.") };
				}
				context.UserTrips.Remove(usertrip);
				await context.SaveChangesAsync();

				return new GenericResponse<UserTrip>() { Data = usertrip };
			}
			catch (Exception ex)
			{
				return new GenericResponse<UserTrip>() { Error = new ErrorResponse(ex) };
			}
		}

        public async Task<GenericResponse<bool>> DeletePassengers(long tripid)
        {
			try
			{
				await using var context = await _context.CreateDbContextAsync();
				var userTrips = context.UserTrips.Where(x => x.TripId == tripid);
				if (userTrips == null)
				{
					return new GenericResponse<bool>() { Error = new ErrorResponse("User trips not found.") };
				}
				foreach (var item in userTrips)
				{
                    item.Active = false;
				}

				context.UserTrips.UpdateRange(userTrips);
				await context.SaveChangesAsync();
				return new GenericResponse<bool>() { Data = true };
			}
			catch (Exception ex)
			{
				return new GenericResponse<bool>() { Error = new ErrorResponse(ex) };
			}
		}

		public async Task<GenericResponse<bool>> ForceDeletePassengers(long tripid)
		{
			try
			{
				await using var context = await _context.CreateDbContextAsync();
				var userTrips = context.UserTrips.Where(x => x.TripId == tripid);
				if (userTrips == null)
				{
					return new GenericResponse<bool>() { Error = new ErrorResponse("User trips not found.") };
				}
				
				context.UserTrips.RemoveRange(userTrips);
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
