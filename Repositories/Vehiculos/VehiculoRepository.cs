using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Vehiculos
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly IDbContextFactory<VetAppDbContext> _context;
        public VehiculoRepository(IDbContextFactory<VetAppDbContext> context)
        {
            _context = context;
        }

        public async Task<GenericResponse<List<Vehiculo>>> List(Expression<Func<Vehiculo, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Vehiculos.Include(v => v.Usuario).AsQueryable();  // Relación con Usuario (similar al de User)

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<List<Vehiculo>> { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<Vehiculo>> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Vehiculo>> Add(Vehiculo vehiculo)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                context.Vehiculos.Add(vehiculo);
                await context.SaveChangesAsync();

                return new GenericResponse<Vehiculo>() { Data = vehiculo };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Vehiculo>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Vehiculo>> Delete(long id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var vehiculo = await context.Vehiculos.FindAsync(id);
                if (vehiculo == null)
                {
                    return new GenericResponse<Vehiculo>() { Error = new ErrorResponse($"No se ha encontrado el vehículo con ID {id}") };
                }

                context.Vehiculos.Remove(vehiculo);
                await context.SaveChangesAsync();

                return new GenericResponse<Vehiculo>() { Data = vehiculo };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Vehiculo>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Vehiculo>> Edit(Vehiculo vehiculo)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();

                var entity = await context.Vehiculos.FindAsync(vehiculo.Id);
                if (entity == null)
                {
                    return new GenericResponse<Vehiculo>() { Error = new ErrorResponse($"No se ha encontrado el vehículo con ID {vehiculo.Id}") };
                }

                context.Entry(entity).CurrentValues.SetValues(vehiculo);
                await context.SaveChangesAsync();

                return new GenericResponse<Vehiculo> { Data = vehiculo };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Vehiculo> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<Vehiculo>> Get(Expression<Func<Vehiculo, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Vehiculos.Include(v => v.Usuario).AsQueryable();  // Relación con Usuario

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<Vehiculo> { Data = await query.FirstOrDefaultAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Vehiculo> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Exists(Expression<Func<Vehiculo, bool>> predicate)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var exists = await context.Vehiculos.AnyAsync(predicate);
                return new GenericResponse<bool> { Data = exists };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }
    }
}
