using AutoMapper;
using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;
using GestorViajes.Repositories.Trips;
using GestorViajes.Repositories.Vehicles;
using GestorViajes.Services.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace GestorViajes.Services.Vehicles
{
	public class VehicleService : IVehicleService
	{
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly ITripRepository _tripRepository;
		public VehicleService(
			IVehicleRepository vehicleRepository,
			IMapper mapper,
			IUserService userService,
			ITripRepository tripRepository)
		{
			_vehicleRepository = vehicleRepository;
			_mapper = mapper;
			_userService = userService;
			_tripRepository = tripRepository;
		}

		public async Task<GenericResponse<List<VehicleViewModel>>> List(Expression<Func<Vehicle, bool>>? predicate = null)
		{
			var response = new GenericResponse<List<VehicleViewModel>>();
			try
			{
				var result = await _vehicleRepository.List(predicate);
				if (!result.Success)
				{
					response.Error = result.Error;
					return response;
				}

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
				var result = await _vehicleRepository.Get(id);
				if (!result.Success)
				{
					response.Error = result.Error;
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
			try
			{
				var current = await _userService.CurrentUser();

				// comprobar que la matricula es unica
				var exists = await _vehicleRepository.Exists(x => x.Plate == model.Plate);
				if(!exists.Success)
					return new GenericResponse<VehicleViewModel>() { Error = exists.Error };
				if (exists.Data)
					return new GenericResponse<VehicleViewModel>() { Error = new ErrorResponse("Ya existe un vehiculo con esta matricula.") };
				


				var vehicle = _mapper.Map<Vehicle>(model);
				vehicle.UserId = current!.Id;
				vehicle.Active = true;
				vehicle.CreatedBy = current.Email;
				vehicle.LastUpdatedBy = current.Email;

				var result = await _vehicleRepository.Add(vehicle);
				if (!result.Success)
				{
					return new GenericResponse<VehicleViewModel>() { Error = result.Error };
				}

				var data = _mapper.Map<VehicleViewModel>(result.Data);
				return new GenericResponse<VehicleViewModel>() { Data = data };
			}
			catch (Exception ex)
			{
				return new GenericResponse<VehicleViewModel>() { Error = new ErrorResponse(ex) };
			}
		}


		public async Task<GenericResponse<VehicleViewModel>> Update(VehicleViewModel model)
		{
			var response = new GenericResponse<VehicleViewModel>();
			try
			{
				// solo el propietario puede modificar el coche
				var current = await _userService.CurrentUser();
				var vehicleResponse = await _vehicleRepository.Get(model.Id);
				if (!vehicleResponse.Success)
				{
					response.Error = vehicleResponse.Error;
					return response;
				}
				if (vehicleResponse.Data!.UserId != current!.Id)
				{
					response.Error = new ErrorResponse("No tienes permisos para modificar este vehiculo.");
					return response;
				}

				// Comprobar que la matricula es unica
				var exists = await _vehicleRepository.Exists(x => x.Plate == model.Plate && x.Id != model.Id);
				if (!exists.Success)
					return new GenericResponse<VehicleViewModel>() { Error = exists.Error };
				if (exists.Data)
					return new GenericResponse<VehicleViewModel>() { Error = new ErrorResponse("Ya existe un vehiculo con esta matricula.") };


				var vehicle = _mapper.Map<Vehicle>(model);
				var result = await _vehicleRepository.Update(vehicle);
				if (!result.Success)
				{
					response.Error = result.Error;
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

		public async Task<GenericResponse<bool>> Toggle(long id)
		{
			var result = await _vehicleRepository.Get(id, true);
			if (!result.Success)
				return new GenericResponse<bool>() { Error = result.Error };

			// solo puede modificarlo el propietario
			var current = await _userService.CurrentUser();
			if (result.Data!.UserId != current!.Id)
			{
				return new GenericResponse<bool>() { Error = new ErrorResponse("No tienes permisos para modificar este vehiculo.") };
			}


			var vehicle = result.Data;
			if (result.Data!.Active)
			{
				if (vehicle.Trips.Any(x => x.Status == TripStatus.Available || x.Status == TripStatus.Full))
				{
					return new GenericResponse<bool>() { Error = new ErrorResponse("No se puede desactivar el vehiculo porque tiene viajes activos.") };
				}
				// Si no hay viajes activos, borramos todo
				var trips = vehicle.Trips.ToList();
				var errors = new List<string>();
				foreach (var trip in trips)
				{
					var passengersResponse = await _tripRepository.DeletePassengers(trip.Id);
					if (!passengersResponse.Success)
						errors.Add(passengersResponse.Error.Message);
					var tripResponse = await _tripRepository.Delete(trip.Id);
					if(!tripResponse.Success)
						errors.Add(tripResponse.Error.Message);
				}
				if(errors.Count > 0)
				{
					return new GenericResponse<bool>() { Error = new ErrorResponse(string.Join(", ", errors)) };
				}
			}

			// Negamos el estado actual del vehiculo
			result.Data!.Active = !result.Data!.Active;

			var updateResult = await _vehicleRepository.Update(result.Data);
			if (!updateResult.Success)
				return new GenericResponse<bool>() { Error = updateResult.Error };

			return new GenericResponse<bool>() { Data = true };
		}

		public async Task<GenericResponse<bool>> Delete(long id)
		{
			var response = new GenericResponse<bool>();
			try
			{
				// comprobar que no tenga dependencias en las tablas hijas
				var vehicle = await _vehicleRepository.Get(id, true);
				if (!vehicle.Success)
				{
					response.Error = vehicle.Error;
					return response;
				}
				// solo puede modificarlo el propietario
				var current = await _userService.CurrentUser();
				if (vehicle.Data!.UserId != current!.Id)
				{
					response.Error = new ErrorResponse("No tienes permisos para eliminar este vehiculo.");
					return response;
				}


				if (vehicle.Data!.Trips.Any(x => x.Status == TripStatus.Available || x.Status == TripStatus.Full))
				{
					response.Error = new ErrorResponse("No se puede eliminar el vehiculo porque tiene viajes activos.");
					return response;
				}
				// Si no hay viajes activos, borramos todo
				var trips = vehicle.Data.Trips.ToList();
				var errors = new List<string>();
				foreach (var trip in trips)
				{
					var passengersResponse = await _tripRepository.ForceDeletePassengers(trip.Id);
					if (!passengersResponse.Success)
						errors.Add(passengersResponse.Error.Message);
					var tripResponse = await _tripRepository.ForceDelete(trip.Id);
					if (!tripResponse.Success)
						errors.Add(tripResponse.Error.Message);
				}
				if (errors.Count > 0)
				{
					response.Error = new ErrorResponse(string.Join(", ", errors));
					return response;
				}


				var result = await _vehicleRepository.ForceDelete(id);
				if (!result.Success)
				{
					response.Error = result.Error;
					return response;
				}

				response.Data = result.Data;
			}
			catch (Exception ex)
			{
				response.Error = new ErrorResponse(ex);
			}
			return response;
		}

		public async Task<GenericResponse<bool>> Exists(Expression<Func<Vehicle, bool>> predicate)
		{
			var response = new GenericResponse<bool>();
			try
			{
				var result = await _vehicleRepository.Exists(predicate);
				if (!result.Success)
				{
					response.Error = result.Error;
					return response;
				}

				response.Data = result.Data;
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
				// Filtramos vehiculos que pertenecen al usuario
				var result = await _vehicleRepository.List(v => v.UserId == userId);
				if (!result.Success)
				{
					response.Error = result.Error;
					return response;
				}

				response.Data = _mapper.Map<List<VehicleViewModel>>(result.Data);
			}
			catch (Exception ex)
			{
				response.Error = new ErrorResponse(ex);
			}
			return response;
		}

		public async Task<GenericResponse<List<SelectListItem>>> DropdownByUser()
		{
			var user = await _userService.CurrentUser();
			var vehicleResponse = await _vehicleRepository.List(x => x.Owner.Id == user!.Id && x.Active);
			if (!vehicleResponse.Success)
				return new GenericResponse<List<SelectListItem>>() { Error = vehicleResponse.Error };

			try
			{
				var data = _mapper.Map<List<SelectListItem>>(vehicleResponse.Data);
				return new GenericResponse<List<SelectListItem>>() { Data = data };
			}
			catch (Exception ex)
			{
				return new GenericResponse<List<SelectListItem>>() { Error = new ErrorResponse(ex) };
			}
		}
	}
}
