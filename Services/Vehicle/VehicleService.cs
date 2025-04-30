using GestorViajes.Models;
using GestorViajes.Repositories.Users;
using System.Linq.Expressions;
using GestorViajes.Models.ViewModels.Vehicle;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Repositories.Vehicles;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GestorViajes.Models.ViewModels.Trip;

namespace GestorViajes.Services.Vehicle
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VehicleService(
            IVehicleRepository vehicleRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _vehicleRepository = vehicleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GenericResponse<List<VehicleViewModel>>> List()
        {
            var response = new GenericResponse<List<VehicleViewModel>>();
            try
            {
                var result = await _vehicleRepository.List();
                if (result.Error != null) return result.ConvertError<List<VehicleViewModel>>();

                response.Data = _mapper.Map<List<VehicleViewModel>>(result.Data);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<VehicleViewModel>> Get(long id)
        {
            var response = new GenericResponse<VehicleViewModel>();
            try
            {
                var result = await _vehicleRepository.GetById(id);
                if (result.Error != null || result.Data == null)
                {
                    response.Error = new ErrorResponse("Vehículo no encontrado.");
                    return response;
                }

                response.Data = _mapper.Map<VehicleViewModel>(result.Data);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<VehicleViewModel>> Add(VehicleViewModel model)
        {
            var response = new GenericResponse<VehicleViewModel>();
            try
            {
                var vehicle = _mapper.Map<Models.EFCore.Rove.Vehicle>(model);

                vehicle.Active = true;
                vehicle.CreatedAt = DateTime.UtcNow;
                vehicle.LastUpdateAt = DateTime.UtcNow;

                var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == "UserId")?.Value;

                if (!long.TryParse(userIdClaim, out var userId))
                    return response.ConvertError<VehicleViewModel>("No se ha podido encontrar el usuario");

                vehicle.UserId = userId;
                vehicle.CreatedBy = userId.ToString();
                vehicle.LastUpdatedBy = userId.ToString();

                var result = await _vehicleRepository.Add(vehicle);
                if (result.Error != null) return result.ConvertError<VehicleViewModel>();

                model.Id = result.Data.Id;
                response.Data = model;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<VehicleViewModel>> Update(VehicleViewModel model)
        {
            var response = new GenericResponse<VehicleViewModel>();
            try
            {
                var result = await _vehicleRepository.GetById(model.Id);
                if (result.Error != null || result.Data == null)
                    return response.ConvertError<VehicleViewModel>("Vehículo no encontrado.");

                var vehicle = result.Data;
                _mapper.Map(model, vehicle);

                var updateResult = await _vehicleRepository.Update(vehicle);
                if (updateResult.Error != null) return updateResult.ConvertError<VehicleViewModel>();

                response.Data = model;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<bool>> DeactivateVehicle(long id)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var result = await _vehicleRepository.GetById(id);
                if (result.Error != null || result.Data == null)
                    return response.ConvertError<bool>("Vehículo no encontrado.");

                result.Data.Active = false;
                await _vehicleRepository.Update(result.Data);

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<bool>> ReactivateVehicle(long id)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var result = await _vehicleRepository.GetById(id);
                if (result.Error != null || result.Data == null)
                    return response.ConvertError<bool>("Vehículo no encontrado.");

                result.Data.Active = true;
                await _vehicleRepository.Update(result.Data);

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<bool>> Delete(long id)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var result = await _vehicleRepository.GetById(id);
                if (result.Error != null || result.Data == null)
                    return response.ConvertError<bool>("Vehículo no encontrado.");

                var deleteResult = await _vehicleRepository.Delete(result.Data);
                response.Data = deleteResult.Data;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<List<VehicleViewModel>>> ListByUser(long userId)
        {
            var response = new GenericResponse<List<VehicleViewModel>>();
            try
            {
                var result = await _vehicleRepository.List(v => v.UserId == userId);
                if (result.Error != null) return result.ConvertError<List<VehicleViewModel>>();

                response.Data = _mapper.Map<List<VehicleViewModel>>(result.Data);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<bool>> Exists(Expression<Func<Models.EFCore.Rove.Vehicle, bool>> predicate)
        {
            return await _vehicleRepository.Exists(predicate);
        }
    }
}
