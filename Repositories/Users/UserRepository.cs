using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Users
{
	public class UserRepository : IUserRepository

	{
		private readonly IDbContextFactory<RoveDbContext> _contextFactory;

		public UserRepository(IDbContextFactory<RoveDbContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		// Buscar usuario por email
		//Puede ser nullable:   User?
		//T? — el primer elemento que cumple la condición, o null si no existe.
		public async Task<GenericResponse<User>> Get(Expression<Func<User, bool>> predicate)
		{
			try
			{
				await using var context = await _contextFactory.CreateDbContextAsync();
				var response = await context.Users.FirstOrDefaultAsync(predicate);
				if (response == null)
				{
					return new GenericResponse<User>() { Error = new ErrorResponse("User not found.") };
				}
				return new GenericResponse<User>() { Data = response };
			}
			catch(Exception ex)
			{
				return new GenericResponse<User>() { Error = new ErrorResponse(ex) };
			}
		}

		#region User CRUD

		public async Task<GenericResponse<List<User>>> List(Expression<Func<User, bool>>? predicate = null)
		{
			try
			{
				await using var context = await _contextFactory.CreateDbContextAsync();
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

		public async Task<GenericResponse<User>> GetCurrentUser(long id)
		{
			try
			{
				await using var context = await _contextFactory.CreateDbContextAsync();
				var user = await context.Users.FindAsync(id);

				if (user == null)
				{
					return new GenericResponse<User>() { Error = new ErrorResponse("User not found.") };
				}
				if (!user.Active)
				{
					return new GenericResponse<User>() { Error = new ErrorResponse("Usuario inactivo") };
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
				await using var context = await _contextFactory.CreateDbContextAsync();
				context.Users.Add(user);  // Añadir el usuario al contexto
				await context.SaveChangesAsync();  // Guardar los cambios en la base de datos

				return new GenericResponse<User>() { Data = user };  // Devolver el usuario agregado
			}
			catch (Exception ex)
			{
				return new GenericResponse<User>() { Error = new ErrorResponse(ex) };  // En caso de error
			}
		}


		public async Task<GenericResponse<User>> Update(User user)
		{
			try
			{
				await using var context = await _contextFactory.CreateDbContextAsync();
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
				await using var context = await _contextFactory.CreateDbContextAsync();
				var user = await context.Users.FindAsync(id);

				if (user == null)
				{
					return new GenericResponse<bool>() { Error = new ErrorResponse("User not found.") };
				}

				// borrar todas las tablas que tengan a ese user id
				// Esta logica se ha sacado al service
				//context.TripRequests.RemoveRange(context.TripRequests.Where(tr => tr.UserId == id));
				//context.UserTrips.RemoveRange(context.UserTrips.Where(ut => ut.UserId == id));
				//context.Trips.RemoveRange(context.Trips.Where(t => t.DriverId == id));
				//context.Trips.RemoveRange(context.Trips.Where(t => t.Passengers.Any(p => p.UserId == id)));
				//context.FuelTickets.RemoveRange(context.FuelTickets.Where(ft => ft.UserId == id));
				//context.Vehicles.RemoveRange(context.Vehicles.Where(v => v.UserId == id));
				// TODO: Añadir las tablas que dependan de usuario- blog, post, etc


				context.Users.RemoveRange(context.Users.Where(u => u.Id == id));

				await context.SaveChangesAsync();

				return new GenericResponse<bool>() { Data = true };
			}
			catch (Exception ex)
			{
				return new GenericResponse<bool>() { Error = new ErrorResponse(ex) };
			}
		}


		public async Task<GenericResponse<bool>> Exists(Expression<Func<User, bool>> predicate)
		{
			try
			{
				await using var context = await _contextFactory.CreateDbContextAsync();
				var exists = await context.Users.AnyAsync(predicate);
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


