using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Routing;

namespace GestorViajes.Repositories.Vehicles
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDbContextFactory<RoveDbContext> _contextFactory;

        public VehicleRepository(IDbContextFactory<RoveDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<GenericResponse<List<Vehicle>>> List(Expression<Func<Vehicle, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                var query = context.Vehicles
                    .Include(v => v.Owner)
                    .AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<List<Vehicle>> { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<Vehicle>> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Vehicle>> Get(long id, bool? include = false)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();

                IQueryable<Vehicle> query = context.Vehicles;

                if (include == true)
                {
                    query = query.Include(v => v.Owner)
                                 .Include(v => v.Trips).ThenInclude(x => x.Passengers);
                }

                var entity = await query.FirstOrDefaultAsync(v => v.Id == id);

                if (entity == null)
                    return new GenericResponse<Vehicle> { Error = new ErrorResponse("Vehículo no encontrado.") };

                return new GenericResponse<Vehicle> { Data = entity };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Vehicle> { Error = new ErrorResponse(ex) };
            }
        }


        public async Task<GenericResponse<Vehicle>> Add(Vehicle entity)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                context.Vehicles.Add(entity);
                await context.SaveChangesAsync();
                return new GenericResponse<Vehicle> { Data = entity };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Vehicle> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Vehicle>> Update(Vehicle entity)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                context.Vehicles.Update(entity);
                await context.SaveChangesAsync();
                return new GenericResponse<Vehicle> { Data = entity };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Vehicle> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> ForceDelete(long id)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                var entity = await context.Vehicles.FindAsync(id);
                if (entity == null)
                    return new GenericResponse<bool> { Error = new ErrorResponse("Vehículo no encontrado.") };

                context.Vehicles.Remove(entity);
                await context.SaveChangesAsync();
                return new GenericResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Exists(Expression<Func<Vehicle, bool>> predicate)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                var exists = await context.Vehicles.AnyAsync(predicate);
                return new GenericResponse<bool> { Data = exists };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }
    }
}
