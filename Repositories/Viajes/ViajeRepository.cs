using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Viajes
{
    public class ViajeRepository : IViajeRepository
    {
        private readonly IDbContextFactory<gestionturnosContext> _context;

        public ViajeRepository(IDbContextFactory<gestionturnosContext> context)
        {
            _context = context;
        }

        // Listar viajes 
        public async Task<GenericResponse<List<viajes>>> List(Expression<Func<viajes, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.viajes.AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<List<viajes>> { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<viajes>> { Error = new ErrorResponse(ex) };
            }
        }

        // Agregar un nuevo viaje
        public async Task<GenericResponse<viajes>> Add(viajes viaje)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                context.viajes.Add(viaje);
                await context.SaveChangesAsync();

                return new GenericResponse<viajes>() { Data = viaje };
            }
            catch (Exception ex)
            {
                return new GenericResponse<viajes>() { Error = new ErrorResponse(ex) };
            }
        }

        // Eliminar un viaje por ID
        public async Task<GenericResponse<viajes>> Delete(long id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var viaje = await context.viajes.FindAsync(id);
                if (viaje == null)
                {
                    return new GenericResponse<viajes>() { Error = new ErrorResponse($"No se ha encontrado el viaje con ID {id}") };
                }

                context.viajes.Remove(viaje);
                await context.SaveChangesAsync();
                return new GenericResponse<viajes>() { Data = viaje };
            }
            catch (Exception ex)
            {
                return new GenericResponse<viajes>() { Error = new ErrorResponse(ex) };
            }
        }

        // Editar un viaje 
        public async Task<GenericResponse<viajes>> Edit(viajes viaje)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var entity = await context.viajes.FindAsync(viaje.id);
                if (entity == null)
                {
                    return new GenericResponse<viajes>() { Error = new ErrorResponse($"No se ha encontrado el viaje con ID {viaje.id}") };
                }

                context.Entry(entity).CurrentValues.SetValues(viaje);
                await context.SaveChangesAsync();
                return new GenericResponse<viajes> { Data = viaje };
            }
            catch (Exception ex)
            {
                return new GenericResponse<viajes> { Error = new ErrorResponse(ex) };
            }
        }

        // Obtener un viaje 
        public async Task<GenericResponse<viajes>> Get(Expression<Func<viajes, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.viajes.AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<viajes> { Data = await query.FirstOrDefaultAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<viajes> { Error = new ErrorResponse(ex) };
            }
        }

        // Verificar si existe un viaje 
        public async Task<GenericResponse<bool>> Exists(Expression<Func<viajes, bool>> predicate)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var exists = await context.viajes.AnyAsync(predicate);
                return new GenericResponse<bool> { Data = exists };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }
    }
}
