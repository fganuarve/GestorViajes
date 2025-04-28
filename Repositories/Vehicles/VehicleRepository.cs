using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Routing;

namespace GestorViajes.Repositories.Vehicles
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly RoveDbContext _context;

        public VehicleRepository(RoveDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles.Include(v => v.Owner).ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(long id)
        {
            return await _context.Vehicles.Include(v => v.Owner)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetByUserIdAsync(long userId)
        {
            return await _context.Vehicles
                .Where(v => v.UserId == userId)
                .Include(v => v.Owner)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Vehicle, bool>> predicate)
        {
            return await _context.Vehicles.AnyAsync(predicate);
        }

    }
}
