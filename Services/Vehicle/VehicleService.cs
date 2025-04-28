using GestorViajes.Models;
using GestorViajes.Repositories.Users;
using System.Linq.Expressions;
using GestorViajes.Models.ViewModels.Vehicle;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Repositories.Vehicles;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace GestorViajes.Services.Vehicle
{
    //public class VehicleService : IVehicleService
    //{
    //    private readonly IVehicleRepository _vehicleRepository;
    //    private readonly IUserRepository _userRepository;
    //    private readonly RoveDbContext _context;
    //    private readonly IMapper _mapper;

    //    public VehicleService(
    //        IVehicleRepository vehicleRepository,
    //        IUserRepository userRepository,
    //        RoveDbContext context,
    //        IMapper mapper)
    //    {
    //        _vehicleRepository = vehicleRepository;
    //        _userRepository = userRepository;
    //        _context = context;
    //        _mapper = mapper;
    //    }

    //    public async Task<GenericResponse<List<VehicleViewModel>>> List()
    //    {
    //        var response = new GenericResponse<List<VehicleViewModel>>();
    //        try
    //        {
    //            var vehicles = await _context.Vehicles
    //                .Include(v => v.Owner)
    //                .ToListAsync();

    //            response.Data = _mapper.Map<List<VehicleViewModel>>(vehicles);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Error = new ErrorResponse(ex);
    //        }
    //        return response;
    //    }

    //    public async Task<GenericResponse<VehicleViewModel>> Get(long id)
    //    {
    //        var response = new GenericResponse<VehicleViewModel>();
    //        try
    //        {
    //            var vehicle = await _context.Vehicles
    //                .Include(v => v.Owner)
    //                .FirstOrDefaultAsync(v => v.Id == id);

    //            if (vehicle == null)
    //            {
    //                response.Error = new ErrorResponse("Vehículo no encontrado.");
    //                return response;
    //            }

    //            response.Data = _mapper.Map<VehicleViewModel>(vehicle);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Error = new ErrorResponse(ex);
    //        }
    //        return response;
    //    }

    //    public async Task<GenericResponse<VehicleViewModel>> Add(VehicleViewModel model)
    //    {
    //        var response = new GenericResponse<VehicleViewModel>();
    //        try
    //        {
    //            var vehicle = _mapper.Map<Vehicle>(model);

    //            _context.Vehicles.Add(vehicle);
    //            await _context.SaveChangesAsync();

    //            model.Id = vehicle.Id;
    //            response.Data = model;
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Error = new ErrorResponse(ex);
    //        }
    //        return response;
    //    }

    //    public async Task<GenericResponse<VehicleViewModel>> Edit(VehicleViewModel model)
    //    {
    //        var response = new GenericResponse<VehicleViewModel>();
    //        try
    //        {
    //            var vehicle = await _context.Vehicles.FindAsync(model.Id);

    //            if (vehicle == null)
    //            {
    //                response.Error = new ErrorResponse("Vehículo no encontrado.");
    //                return response;
    //            }

    //            _mapper.Map(model, vehicle);
    //            vehicle.LastUpdateAt = DateTime.UtcNow; // Opcional si quieres actualizar manualmente

    //            _context.Vehicles.Update(vehicle);
    //            await _context.SaveChangesAsync();

    //            response.Data = model;
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Error = new ErrorResponse(ex);
    //        }
    //        return response;
    //    }

    //    public async Task<GenericResponse<bool>> Delete(long id)
    //    {
    //        var response = new GenericResponse<bool>();
    //        try
    //        {
    //            var vehicle = await _context.Vehicles.FindAsync(id);

    //            if (vehicle == null)
    //            {
    //                response.Error = new ErrorResponse("Vehículo no encontrado.");
    //                return response;
    //            }

    //            _context.Vehicles.Remove(vehicle);
    //            await _context.SaveChangesAsync();

    //            response.Data = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Error = new ErrorResponse(ex);
    //        }
    //        return response;
    //    }

    //    public async Task<GenericResponse<List<VehicleViewModel>>> ListByUser(long userId)
    //    {
    //        var response = new GenericResponse<List<VehicleViewModel>>();
    //        try
    //        {
    //            var vehicles = await _context.Vehicles
    //                .Where(v => v.UserId == userId)
    //                .Include(v => v.Owner)
    //                .ToListAsync();

    //            response.Data = _mapper.Map<List<VehicleViewModel>>(vehicles);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Error = new ErrorResponse(ex);
    //        }
    //        return response;
    //    }

    //    public async Task<GenericResponse<bool>> Exists(Expression<Func<Vehicle, bool>> predicate)
    //    {
    //        var response = new GenericResponse<bool>();
    //        try
    //        {
    //            response.Data = await _context.Vehicles.AnyAsync(predicate);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Error = new ErrorResponse(ex);
    //        }
    //        return response;
    //    }
    //}
}
