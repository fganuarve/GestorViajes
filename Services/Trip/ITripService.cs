using System.Linq.Expressions;
using GestorViajes.Models.ViewModels.Trip;
using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;

namespace GestorViajes.Services.Trips
{
    public interface ITripService
    {
        Task<GenericResponse<TripViewModel>> Add(TripViewModel model);
        Task<GenericResponse<TripViewModel>> GetById(long id);
        Task<GenericResponse<List<TripViewModel>>> List(Expression<Func<Trip, bool>>? predicate = null);
        Task<GenericResponse<TripViewModel>> Update(TripViewModel model);
        Task<GenericResponse<bool>> Delete(long id);
        //Para exists, trabajo sobre la entidad, no sobre el ViewModel
        Task<GenericResponse<bool>> Exists(Expression<Func<Trip, bool>> predicate);

        Task<GenericResponse<bool>> JoinTrip(long id);
		Task<GenericResponse<bool>> ExitTrip(long id, long userId);
        Task<GenericResponse<bool>> CancelTrip(long id);
        Task<GenericResponse<bool>> EndTrip(long id);
	}
}
