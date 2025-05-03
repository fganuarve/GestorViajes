using System.Linq.Expressions;
using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;

namespace GestorViajes.Repositories.Trips
{
    public interface ITripRepository
    {
        Task<GenericResponse<List<Trip>>> List(Expression<Func<Trip, bool>>? predicate = null);
        Task<GenericResponse<Trip>> GetById(long id);
        Task<GenericResponse<Trip>> Add(Trip trip);
        Task<GenericResponse<Trip>> Update(Trip trip);
        Task<GenericResponse<bool>> Delete(long id);
        Task<GenericResponse<bool>> ForceDelete(long id);
        // verificar si existe un viaje
        Task<GenericResponse<bool>> Exists(Expression<Func<Trip, bool>> predicate);

        #region UserTrips
        Task<GenericResponse<UserTrip>> AddPassenger(UserTrip usertrip);
        Task<GenericResponse<UserTrip>> DeletePassenger(long tripid, long userid);
        Task<GenericResponse<bool>> DeletePassengers(long tripid);
        Task<GenericResponse<bool>> ForceDeletePassengers(long tripid);
        #endregion
    }
}
