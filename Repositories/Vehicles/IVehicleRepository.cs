using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Vehicles
{
    public interface IVehicleRepository
    {

        Task<GenericResponse<List<Vehicle>>> List(Expression<Func<Vehicle, bool>>? predicate = null);
        Task<GenericResponse<Vehicle>> Get(long id, bool? include = false);
        Task<GenericResponse<Vehicle>> Add(Vehicle entity);
        Task<GenericResponse<Vehicle>> Update(Vehicle entity);
        Task<GenericResponse<bool>> ForceDelete(long id);
        Task<GenericResponse<bool>> Exists(Expression<Func<Vehicle, bool>> predicate);
    }
}
