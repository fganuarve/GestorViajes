using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.Vehicles
{
    public interface IVehicleRepository
    {
        public interface IVehicleRepository
        {
            Task<List<Vehicle>> GetAllAsync();
            Task<Vehicle?> GetByIdAsync(long id);
            Task AddAsync(Vehicle vehicle);
            Task UpdateAsync(Vehicle vehicle);
            Task DeleteAsync(Vehicle vehicle);
            Task<List<Vehicle>> GetByUserIdAsync(long userId);
            Task<bool> ExistsAsync(Expression<Func<Vehicle, bool>> predicate);
        }

    }
}
