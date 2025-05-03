using System.Linq.Expressions;
using AutoMapper;
using Azure;
using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Trip;
using GestorViajes.Repositories.Trips;
using GestorViajes.Repositories.Users;
using GestorViajes.Services.Users;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace GestorViajes.Services.Trips
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<TripService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TripService(
            ITripRepository tripRepository,
            IUserService userService,
            IMapper mapper,
            ILogger<TripService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _tripRepository = tripRepository;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GenericResponse<TripViewModel>> Add(TripViewModel model)
        {
            var user = await _userService.CurrentUser();
            var trip = _mapper.Map<Trip>(model);
            trip.DriverId = user!.Id;
            trip.CreatedAt = DateTime.UtcNow;
            trip.Active = true;
            trip.CreatedBy = user.Email;
            trip.LastUpdatedBy = user.Email;
            trip.Status = TripStatus.Available;

            var result = await _tripRepository.Add(trip);
            if (!result.Success)
            {
                return new GenericResponse<TripViewModel>
                {
                    Error = result.Error
                };
            }
            return new GenericResponse<TripViewModel> { Data = _mapper.Map<TripViewModel>(result.Data) };
        }

        public async Task<GenericResponse<TripViewModel>> GetById(long id)
        {
            var response = new GenericResponse<TripViewModel>();

            try
            {
                var current = await _userService.CurrentUser();
                var result = await _tripRepository.GetById(id);
                if (!result.Success)
                {
                    response.Error = result.Error;
                    return response;
                }

                // verificamos que el usuario actual tiene permisos al ser conductor o pasajero
                if (result.Data!.DriverId != current!.Id && !result.Data.Passengers.Any(x => x.UserId == current.Id))
                {
                    response.Error = new ErrorResponse("No tienes permiso para ver este viaje.");
                    return response;
                }

                response.Data = _mapper.Map<TripViewModel>(result.Data);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }

            return response;
        }

        public async Task<GenericResponse<List<TripViewModel>>> List(Expression<Func<Trip, bool>>? predicate = null)
        {
            var response = new GenericResponse<List<TripViewModel>>();
            try
            {
                var result = await _tripRepository.List(predicate);
                if (!result.Success)
                {
                    response.Error = result.Error;
                    return response;
                }
                response.Data = _mapper.Map<List<TripViewModel>>(result.Data);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse(ex);
            }
            return response;
        }

        public async Task<GenericResponse<TripViewModel>> Update(TripViewModel model)
        {
            var response = new GenericResponse<TripViewModel>();

            try
            {
                // solo puede editar el viaje el conductor
                var current = await _userService.CurrentUser();
                var viajeResponse = await _tripRepository.GetById(model.Id);
                if (!viajeResponse.Success)
                {
                    response.Error = viajeResponse.Error;
                    return response;
                }
                if (viajeResponse.Data!.DriverId != current!.Id)
                {
                    response.Error = new ErrorResponse("No tienes permiso para editar este viaje.");
                    return response;
                }

                var trip = _mapper.Map<Trip>(model);
                var result = await _tripRepository.Update(trip);
                if (!result.Success)
                {
                    response.Error = result.Error;
                    return response;
                }

                response.Data = _mapper.Map<TripViewModel>(result.Data);
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
                // solo puede borrarlo el conductor
                var current = await _userService.CurrentUser();
                var viajeResponse = await _tripRepository.GetById(id);
                if (!viajeResponse.Success)
                {
                    response.Error = viajeResponse.Error;
                    return response;
                }
                if (viajeResponse.Data!.DriverId != current!.Id)
                {
                    response.Error = new ErrorResponse("No tienes permiso para eliminar este viaje.");
                    return response;
                }

                var result = await _tripRepository.Delete(id);
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

        public async Task<GenericResponse<bool>> Exists(Expression<Func<Models.EFCore.Rove.Trip, bool>> predicate)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var result = await _tripRepository.Exists(predicate);
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

        public async Task<GenericResponse<bool>> JoinTrip(long id)
        {
            var current = await _userService.CurrentUser();
            var tripResponse = await _tripRepository.GetById(id);
            if (!tripResponse.Success)
            {
                return new GenericResponse<bool>() { Error = tripResponse.Error };
            }
            var trip = tripResponse.Data;
            if (trip.Status != TripStatus.Available)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse("El viaje no está disponible.") };
            }
            var userTrip = new UserTrip()
            {
                UserId = current!.Id,
                TripId = trip.Id,
                Active = true
            };
            var result = await _tripRepository.AddPassenger(userTrip);
            if (!result.Success)
            {
                return new GenericResponse<bool>() { Error = result.Error };
            }

            // Verificamos si se han llenado todos los asientos para cambiar el estado del viaje
            var passengersCount = await _tripRepository.GetById(id);
            if (!passengersCount.Success)
                return new GenericResponse<bool>() { Error = passengersCount.Error };

            if (passengersCount.Data!.Passengers.Count == trip.Seats)
            {
                trip.Status = TripStatus.Full;
                var updateResponse = await _tripRepository.Update(trip);
                if (!updateResponse.Success)
                {
                    return new GenericResponse<bool>() { Error = updateResponse.Error };
                }
            }

            return new GenericResponse<bool> { Data = true };
        }
        public async Task<GenericResponse<bool>> ExitTrip(long id, long userId)
        {
            var tripResponse = await _tripRepository.GetById(id);
            if (!tripResponse.Success)
            {
                return new GenericResponse<bool>() { Error = tripResponse.Error };
            }
            var trip = tripResponse.Data;
            if (trip.Status == TripStatus.Cancelled || trip.Status == TripStatus.Completed)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse("El viaje no está disponible.") };
            }

            // comprobamos que el usuario es un pasajero
            var passenger = trip.Passengers.FirstOrDefault(x => x.UserId == userId);
            if (passenger == null)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse("No eres un pasajero en este viaje.") };
            }

            var deleteResponse = await _tripRepository.DeletePassenger(id, userId);
            if (!deleteResponse.Success)
            {
                return new GenericResponse<bool>() { Error = deleteResponse.Error };
            }
            // Reactualizamos el trip
            tripResponse = await _tripRepository.GetById(id);
            if (!tripResponse.Success)
            {
                return new GenericResponse<bool>() { Error = tripResponse.Error };
            }
            trip = tripResponse.Data;
            if (trip.Status == TripStatus.Full)
            {
                // Si el viaje estaba lleno, cambiamos el estado a Available
                trip.Status = TripStatus.Available;
                var updateResponse = await _tripRepository.Update(trip);
                if (!updateResponse.Success)
                {
                    return new GenericResponse<bool>() { Error = updateResponse.Error };
                }
            }

            return new GenericResponse<bool> { Data = true };
        }


        public async Task<GenericResponse<bool>> CancelTrip(long id)
        {
            try
            {
                var current = await _userService.CurrentUser();
                var tripResponse = await _tripRepository.GetById(id);
                if (!tripResponse.Success)
                {
                    return new GenericResponse<bool>() { Error = tripResponse.Error };
                }

                if (tripResponse.Data.DriverId != current.Id)
                {
                    return new GenericResponse<bool>() { Error = new ErrorResponse("No tienes permiso para cancelar este viaje.") };
                }

                var trip = tripResponse.Data;
                if (trip.Status == TripStatus.Completed || trip.Status == TripStatus.Cancelled)
                {
                    return new GenericResponse<bool>() { Error = new ErrorResponse("El viaje no está disponible.") };
                }
                trip.Status = TripStatus.Cancelled;
                trip.Active = false;
                var result = await _tripRepository.Update(trip);
                if (!result.Success)
                {
                    return new GenericResponse<bool>() { Error = result.Error };
                }
                // Eliminar todos los pasajeros del viaje
                var deleteResponse = await _tripRepository.DeletePassengers(id);
                if (!deleteResponse.Success)
                {
                    return new GenericResponse<bool>() { Error = deleteResponse.Error };
                }
                return new GenericResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> EndTrip(long id)
        {
            try
            {
                var current = await _userService.CurrentUser();
                var tripResponse = await _tripRepository.GetById(id);
                if (!tripResponse.Success)
                {
                    return new GenericResponse<bool>() { Error = tripResponse.Error };
                }
                if (tripResponse.Data.DriverId != current.Id)
                {
                    return new GenericResponse<bool>() { Error = new ErrorResponse("No tienes permiso para finalizar este viaje.") };
                }
                var trip = tripResponse.Data;
                if (trip.Status == TripStatus.Completed || trip.Status == TripStatus.Cancelled)
                {
                    return new GenericResponse<bool>() { Error = new ErrorResponse("El viaje no está disponible.") };
                }
                trip.Status = TripStatus.Completed;
                trip.Active = false;
                var result = await _tripRepository.Update(trip);
                if (!result.Success)
                {
                    return new GenericResponse<bool>() { Error = result.Error };
                }
                return new GenericResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool>() { Error = new ErrorResponse(ex) };
            }
        }
    }
}
