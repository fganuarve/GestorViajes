using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<gestionturnosContext> _context;

        public UserRepository(IDbContextFactory<gestionturnosContext> context)
        {
            _context = context;
        }

        public async Task<GenericResponse<List<usuarios>>> List(Expression<Func<usuarios, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.usuarios.AsQueryable();  // Se debe usar "usuarios" como entidad
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                return new GenericResponse<List<usuarios>>() { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<usuarios>>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<usuarios>> Add(usuarios user)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                context.usuarios.Add(user);  // Se debe usar "usuarios" como entidad
                await context.SaveChangesAsync();
                return new GenericResponse<usuarios>() { Data = user };
            }
            catch (Exception ex)
            {
                return new GenericResponse<usuarios>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<usuarios>> Edit(usuarios user)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var entity = await context.usuarios.FindAsync(user.id);
                if (entity == null)
                {
                    return new GenericResponse<usuarios> { Error = new ErrorResponse("Usuario no encontrado") };
                }
                context.Entry(entity).CurrentValues.SetValues(user);
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return new GenericResponse<usuarios> { Data = entity };
            }
            catch (Exception ex)
            {
                return new GenericResponse<usuarios> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<usuarios>> Delete(long id)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var user = await context.usuarios.FindAsync(id);
                if (user == null)
                {
                    return new GenericResponse<usuarios>() { Error = new ErrorResponse($"No se ha encontrado el usuario con ID {id}") };
                }
                context.usuarios.Remove(user);
                await context.SaveChangesAsync();
                return new GenericResponse<usuarios> { Data = user };
            }
            catch (Exception ex)
            {
                return new GenericResponse<usuarios> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<usuarios>> Get(Expression<Func<usuarios, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var query = context.usuarios.AsQueryable();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                var result = await query.FirstOrDefaultAsync();
                if (result == null)
                {
                    return new GenericResponse<usuarios> { Error = new ErrorResponse("Usuario no encontrado") };
                }
                return new GenericResponse<usuarios> { Data = result };
            }
            catch (Exception ex)
            {
                return new GenericResponse<usuarios> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Exists(Expression<Func<usuarios, bool>> predicate)
        {
            try
            {
                await using var context = await _context.CreateDbContextAsync();
                var exists = await context.usuarios.AnyAsync(predicate);
                return new GenericResponse<bool> { Data = exists };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }
    }
}


