using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Vehicles
{
    public interface IVehicleRepository
    {
        public interface IVehicleRepository
        {
            Task<GenericResponse<List<Vehicle>>> List(Expression<Func<Vehicle, bool>>? predicate = null);
            Task<GenericResponse<Vehicle>> Get(long id);
            Task<GenericResponse<Vehicle>> Add(Vehicle entity);
            Task<GenericResponse<Vehicle>> Update(Vehicle entity);
            Task<GenericResponse<bool>> Delete(long id);
            Task<GenericResponse<bool>> Exists(Expression<Func<Vehicle, bool>> predicate);
        }

    }
}
