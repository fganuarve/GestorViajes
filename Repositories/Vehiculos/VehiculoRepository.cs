using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Vehiculos
{
    public class VehiculoRepository
    {
        //private readonly IDbContextFactory<gestionturnosContext> _context;
        //public VehiculoRepository(IDbContextFactory<gestionturnosContext> context)
        //{
        //    _context = context;
        //}

        //public async Task<GenericResponse<List<vehiculos>>> List(Expression<Func<vehiculos, bool>>? predicate = null)
        //{
        //    try
        //    {
        //        await using var context = await _context.CreateDbContextAsync();
        //        var query = context.vehiculos.Include(v => v.usuario).AsQueryable();

        //        if (predicate != null)
        //        {
        //            query = query.Where(predicate);
        //        }

        //        return new GenericResponse<List<vehiculos>> { Data = await query.ToListAsync() };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GenericResponse<List<vehiculos>> { Error = new ErrorResponse(ex) };
        //    }
        //}

        //public async Task<GenericResponse<vehiculos>> Add(vehiculos vehiculo)
        //{
        //    try
        //    {
        //        await using var context = await _context.CreateDbContextAsync();
        //        context.vehiculos.Add(vehiculo);
        //        await context.SaveChangesAsync();

        //        return new GenericResponse<vehiculos>() { Data = vehiculo };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GenericResponse<vehiculos>() { Error = new ErrorResponse(ex) };
        //    }
        //}

        //public async Task<GenericResponse<vehiculos>> Delete(long id)
        //{
        //    try
        //    {
        //        await using var context = await _context.CreateDbContextAsync();
        //        var vehiculo = await context.vehiculos.FindAsync(id);
        //        if (vehiculo == null)
        //        {
        //            return new GenericResponse<vehiculos>() { Error = new ErrorResponse($"No se ha encontrado el vehículo con ID {id}") };
        //        }

        //        context.vehiculos.Remove(vehiculo);
        //        await context.SaveChangesAsync();

        //        return new GenericResponse<vehiculos>() { Data = vehiculo };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GenericResponse<vehiculos>() { Error = new ErrorResponse(ex) };
        //    }
        //}

        //public async Task<GenericResponse<vehiculos>> Edit(vehiculos vehiculo)
        //{
        //    try
        //    {
        //        await using var context = await _context.CreateDbContextAsync();

        //        var entity = await context.vehiculos.FindAsync(vehiculo.id);
        //        if (entity == null)
        //        {
        //            return new GenericResponse<vehiculos>() { Error = new ErrorResponse($"No se ha encontrado el vehículo con ID {vehiculo.id}") };
        //        }

        //        context.Entry(entity).CurrentValues.SetValues(vehiculo);
        //        await context.SaveChangesAsync();

        //        return new GenericResponse<vehiculos> { Data = vehiculo };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GenericResponse<vehiculos> { Error = new ErrorResponse(ex) };
        //    }
        //}

        //public async Task<GenericResponse<vehiculos>> Get(Expression<Func<vehiculos, bool>>? predicate = null)
        //{
        //    try
        //    {
        //        await using var context = await _context.CreateDbContextAsync();
        //        var query = context.vehiculos.Include(v => v.usuario).AsQueryable();  // Relación con Usuario

        //        if (predicate != null)
        //        {
        //            query = query.Where(predicate);
        //        }

        //        return new GenericResponse<vehiculos> { Data = await query.FirstOrDefaultAsync() };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GenericResponse<vehiculos> { Error = new ErrorResponse(ex) };
        //    }
        //}

        //public async Task<GenericResponse<bool>> Exists(Expression<Func<vehiculos, bool>> predicate)
        //{
        //    try
        //    {
        //        await using var context = await _context.CreateDbContextAsync();
        //        var exists = await context.vehiculos.AnyAsync(predicate);
        //        return new GenericResponse<bool> { Data = exists };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
        //    }
        //}
    }
}
