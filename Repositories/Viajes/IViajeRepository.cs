using GestorViajes.Models.EFCore.GestionTurnos;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Viajes
{
    public interface IViajeRepository
    {
        // listar viajes con filtro 
        Task<GenericResponse<List<Viaje>>> List(Expression<Func<Viaje, bool>>? predicate = null);

        // agregar un nuevo viaje
        Task<GenericResponse<Viaje>> Add(Viaje viaje);

        // eliminar un viaje por ID
        Task<GenericResponse<Viaje>> Delete(long id);

        // editar un viaje 
        Task<GenericResponse<Viaje>> Edit(Viaje viaje);

        // obtener un  viaje con filtro 
        Task<GenericResponse<Viaje>> Get(Expression<Func<Viaje, bool>>? predicate = null);

        // verificar si existe un viaje
        Task<GenericResponse<bool>> Exists(Expression<Func<Viaje, bool>> predicate);
    }
}
