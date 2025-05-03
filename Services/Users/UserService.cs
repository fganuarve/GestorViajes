using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Models;
using System.Linq.Expressions;
using GestorViajes.Repositories.Users;
using GestorViajes.Constants;
using GestorViajes.Repositories.Trips;
using GestorViajes.Repositories.FuelTickets;
using GestorViajes.Repositories.Vehicles;

namespace GestorViajes.Services.Users
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly ITripRepository _tripRepository;
		private readonly IFuelTicketRepository _fuelTicketRepository;
		private readonly IVehicleRepository _vehicleRepository;


		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;
		private readonly IHttpContextAccessor _accessor;

		public UserService(
			IUserRepository userRepository,
			IMapper mapper,
			ILogger<UserService> logger,
			IHttpContextAccessor accessor,
			ITripRepository tripRepository,
			IFuelTicketRepository fuelTicketRepository,
			IVehicleRepository vehicleRepository
			)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_logger = logger;
			_accessor = accessor;
			_tripRepository = tripRepository;
			_fuelTicketRepository = fuelTicketRepository;
			_vehicleRepository = vehicleRepository;
		}

		public async Task<GenericResponse<User>> AuthenticateUserAsync(string email, string password)
		{
			var userResponse = await _userRepository.Get(x => x.Email == email.ToLower());
			if (!userResponse.Success)
			{
				_logger.LogError(userResponse.Error!.Message);
				return new GenericResponse<User>() { Error = userResponse.Error };
			}
			if (!string.Equals(userResponse.Data!.Password.Trim(), password.Trim(), StringComparison.InvariantCulture))
			{
				return new GenericResponse<User>() { Error = new ErrorResponse($"Credenciales no válidas") };
			}
			if (!userResponse.Data.Active)
			{
				return new GenericResponse<User>() { Error = new ErrorResponse("Su cuenta está inactiva, contacte con un administrador para reactivarla.") };
			}
			return new GenericResponse<User>() { Data = userResponse.Data };
		}
		//Listar usuarios
		//no sirve para poblar un dropdown de conductores
		public async Task<GenericResponse<List<UserViewModel>>> List(Expression<Func<User, bool>>? predicate = null)
		{

			var result = await _userRepository.List(predicate);
			if (!result.Success)
				return new GenericResponse<List<UserViewModel>>() { Error = result.Error };

			try
			{
				var data = _mapper.Map<List<UserViewModel>>(result.Data);
				return new GenericResponse<List<UserViewModel>>() { Data = data };
			}
			catch (Exception ex)
			{
				return new GenericResponse<List<UserViewModel>>() { Error = new ErrorResponse(ex) };
			}
		}

		public async Task<GenericResponse<UserViewModel>> Get(Expression<Func<User, bool>> predicate)
		{
			var result = await _userRepository.Get(predicate);
			if (!result.Success)
				return new GenericResponse<UserViewModel>() { Error = result.Error };

			try
			{
				var data = _mapper.Map<UserViewModel>(result.Data);
				return new GenericResponse<UserViewModel>() { Data = data };
			}
			catch (Exception ex)
			{
				return new GenericResponse<UserViewModel>() { Error = new ErrorResponse(ex) };
			}
		}

		public async Task<GenericResponse<UserViewModel>> Add(UserViewModel model)
		{
			// Comprobar que el email es unico
			var existingUser = await _userRepository.Get(x => x.Email == model.Email.ToLower());

			if (!existingUser.Success && existingUser.Error?.Message != "User not found.")
				return new GenericResponse<UserViewModel>() { Error = existingUser.Error };

			if (existingUser.Data != null)
				return new GenericResponse<UserViewModel>() { Error = new ErrorResponse("El email ya está en uso.") };
			try
			{
				// Mapeo del modelo ViewModel a la entidad User
				var user = _mapper.Map<User>(model);

				// El rol se preasigno en el viewmodel
				//le digo que es activo a true
				user.Active = true;
				user.CreatedBy = model.Email;
				user.LastUpdatedBy = model.Email;

				// Llamada al repositorio para guardar el usuario
				var result = await _userRepository.Add(user);

				if (!result.Success)
					return new GenericResponse<UserViewModel>() { Error = result.Error };

				// Mapeo de la entidad User de vuelta a UserViewModel
				var data = _mapper.Map<UserViewModel>(result.Data);
				return new GenericResponse<UserViewModel>() { Data = data };
			}
			catch (Exception ex)
			{
				return new GenericResponse<UserViewModel>() { Error = new ErrorResponse(ex) };
			}
		}

		public async Task<GenericResponse<UserViewModel>> Update(UserViewModel model)
		{
			try
			{
				// solo puede modificarse el propio usuario
				var current = await CurrentUser();
				if(model.Id != current!.Id)
				{
					return new GenericResponse<UserViewModel>() { Error = new ErrorResponse("No puedes modificar otro usuario") };
				}

				var user = _mapper.Map<User>(model);

				var result = await _userRepository.Update(user);
				if (!result.Success)
					return new GenericResponse<UserViewModel>() { Error = result.Error };

				var data = _mapper.Map<UserViewModel>(result.Data);
				return new GenericResponse<UserViewModel>() { Data = data };
			}
			catch (Exception ex)
			{
				return new GenericResponse<UserViewModel>() { Error = new ErrorResponse(ex) };
			}
		}
		public async Task<GenericResponse<bool>> Toggle(long id)
		{
			var result = await _userRepository.Get(x => x.Id == id);
			if (!result.Success)
				return new GenericResponse<bool>() { Error = result.Error };

			// TODO: Comprobar las tablas que dependan de usuarios y no permitir la desactivacion en caso de que tengan datos


			var user = result.Data;
			result.Data!.Active = !result.Data.Active;

			var updateResult = await _userRepository.Update(result.Data);
			if (!updateResult.Success)
				return new GenericResponse<bool>() { Error = updateResult.Error };

			return new GenericResponse<bool>() { Data = true };
		}

		//borrado total, solo deberia ser realizado por admin
		public async Task<GenericResponse<bool>> Delete(long id)
		{
			// comprobar que el usuario que se borra lo hace el mismo
			var current = await CurrentUser();
			if (current!.Id != id)
			{
				return new GenericResponse<bool>() { Error = new ErrorResponse("No puedes eliminar otro usuario") };
			}


			// borrar usertrips de este usuario
			var userTrips = await _tripRepository.List(x => x.DriverId == id || x.Passengers.Select(y => y.UserId).Contains(id));
			if (!userTrips.Success)
				return new GenericResponse<bool>() { Error = userTrips.Error };
			if(userTrips.Data.Any(x=> x.Status == TripStatus.Available || x.Status == TripStatus.Full))
			{
				return new GenericResponse<bool>() { Error = new ErrorResponse("No se puede eliminar el usuario porque tiene viajes activos.") };
			}
			else
			{
				var errors = new List<string>();
				foreach (var trip in userTrips.Data)
				{
					var passengersResponse = await _tripRepository.ForceDeletePassengers(trip.Id);
					if (!passengersResponse.Success)
						errors.Add(passengersResponse.Error.Message);
					var tripResponse = await _tripRepository.ForceDelete(trip.Id);
					if (!tripResponse.Success)
						errors.Add(tripResponse.Error.Message);
				}
				if (errors.Count > 0)
					return new GenericResponse<bool>() { Error = new ErrorResponse(string.Join(", ", errors)) };
				// limpiamos los errores
				errors.Clear();
				// borramos los vehiculos
				var vehicles = await _vehicleRepository.List(x => x.UserId == id);
				if (!vehicles.Success)
					return new GenericResponse<bool>() { Error = vehicles.Error };
				foreach(var vehicle in vehicles.Data)
				{
					var vehicleResponse = await _vehicleRepository.ForceDelete(vehicle.Id);
					if (!vehicleResponse.Success)
						errors.Add(vehicleResponse.Error!.Message);
				}

				// borramos todos los tickets con sus imagenes
				var tickets = await _fuelTicketRepository.List(x => x.UserId == id);
				if (!tickets.Success)
					return new GenericResponse<bool>() { Error = tickets.Error };
				foreach (var ticket in tickets.Data)
				{
					var ticketResponse = await _fuelTicketRepository.Delete(ticket.Id);
					if (!ticketResponse.Success)
						errors.Add(ticketResponse.Error!.Message);
				}

				if(errors.Count > 0)
				{
					return new GenericResponse<bool>() { Error = new ErrorResponse(string.Join(", ", errors)) };
				}

				var result = await _userRepository.Delete(id);
				if (!result.Success)
					return new GenericResponse<bool>() { Error = result.Error };
				return new GenericResponse<bool>() { Data = true };
			}
		}

		public async Task<User?> CurrentUser()
		{
			var userIdClaim = _accessor.HttpContext?.User?.FindFirst(Settings.UserId);

			if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out var userId))
			{
				return null;
			}
			var response = await _userRepository.GetCurrentUser(userId);

			return response.Success ? response.Data : null;
		}


		#region Subscription
		public async Task<GenericResponse<bool>> ChangeSubscriptionAsync(long id, SubscriptionType newPlan)
		{
			var userResponse = await _userRepository.Get(x => x.Id == id);
			if (!userResponse.Success)
			{
				_logger.LogError(userResponse.Error!.Message);
				return new GenericResponse<bool>() { Error = userResponse.Error };
			}
			userResponse.Data!.CurrentPlan = newPlan;
			var updateResponse = await _userRepository.Update(userResponse.Data);
			if (!updateResponse.Success)
			{
				_logger.LogError(updateResponse.Error!.Message);
				return new GenericResponse<bool>() { Error = updateResponse.Error };
			}
			return new GenericResponse<bool>() { Data = true };
		}
		#endregion

	}
}
