using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Viajes
{
    public class ViajeRepository : IViajeRepository
    {
        private readonly IDbContextFactory<GestorViajesDbContext> _context;

        public ViajeRepository(IDbContextFactory<GestorViajesDbContext> context)
        {
            _context = context;
        }

        // Listar viajes 
        public async Task<GenericResponse<List<Viaje>>> List(Expression<Func<Viaje, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Viajes.AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<List<Viaje>> { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<Viaje>> { Error = new ErrorResponse(ex) };
            }
        }

        // Agregar un nuevo viaje
        public async Task<GenericResponse<Viaje>> Add(Viaje viaje)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                context.Viajes.Add(viaje);
                await context.SaveChangesAsync();

                return new GenericResponse<Viaje>() { Data = viaje };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Viaje>() { Error = new ErrorResponse(ex) };
            }
        }

        // Eliminar un viaje por ID
        public async Task<GenericResponse<Viaje>> Delete(long id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var viaje = await context.Viajes.FindAsync(id);
                if (viaje == null)
                {
                    return new GenericResponse<Viaje>() { Error = new ErrorResponse($"No se ha encontrado el viaje con ID {id}") };
                }

                context.Viajes.Remove(viaje);
                await context.SaveChangesAsync();
                return new GenericResponse<Viaje>() { Data = viaje };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Viaje>() { Error = new ErrorResponse(ex) };
            }
        }

        // Editar un viaje 
        public async Task<GenericResponse<Viaje>> Edit(Viaje viaje)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var entity = await context.Viajes.FindAsync(viaje.Id);
                if (entity == null)
                {
                    return new GenericResponse<Viaje>() { Error = new ErrorResponse($"No se ha encontrado el viaje con ID {viaje.Id}") };
                }

                context.Entry(entity).CurrentValues.SetValues(viaje);
                await context.SaveChangesAsync();
                return new GenericResponse<Viaje> { Data = viaje };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Viaje> { Error = new ErrorResponse(ex) };
            }
        }

        // Obtener un viaje 
        public async Task<GenericResponse<Viaje>> Get(Expression<Func<Viaje, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.Viajes.AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<Viaje> { Data = await query.FirstOrDefaultAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Viaje> { Error = new ErrorResponse(ex) };
            }
        }

        // Verificar si existe un viaje 
        public async Task<GenericResponse<bool>> Exists(Expression<Func<Viaje, bool>> predicate)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var exists = await context.Viajes.AnyAsync(predicate);
                return new GenericResponse<bool> { Data = exists };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }
    }
}
