using System.Linq.Expressions;
using GestorViajes.Models.ViewModels.Trip;
using GestorViajes.Models;
using GestorViajes.Models.EFCore;
using Azure;

namespace GestorViajes.Services.Trip
{
    public interface ITripService
    {
        Task<GenericResponse<List<TripViewModel>>> List();
        Task<GenericResponse<TripViewModel>> Get(long id);
        Task<GenericResponse<TripViewModel>> Add(TripViewModel model);
        Task<GenericResponse<TripViewModel>> Edit(TripViewModel model);
        Task<GenericResponse<bool>> Delete(long id);
        Task<GenericResponse<bool>> Terminate(long id);
        Task<GenericResponse<List<TripViewModel>>> ListByDriver(long driverId);




        /*Task<GenericResponse<List<ViajeViewModel>>> List(Expression<Func<ViajeViewModel, bool>>? predicate = null);
        Task<GenericResponse<ViajeViewModel>> Get(int id);
        Task<GenericResponse<ViajeViewModel>> Add(CrearViajeViewModel model);
        Task<GenericResponse<ViajeViewModel>> Edit(int id, EditarViajeViewModel model);
        Task<GenericResponse<ViajeViewModel>> Delete(int id);
        Task<GenericResponse<List<ViajeViewModel>>> ListByUser(string userId, string role, EstadoViaje estado);
        Task<GenericResponse<List<UsuarioViewModel>>> ListPasajeros(int viajeId);
        Task<GenericResponse<bool>> SolicitarUnirse(int usuarioId, int viajeId);
        Task<GenericResponse<bool>> AceptarSolicitud(int usuarioViajeId);
        Task<GenericResponse<bool>> RechazarSolicitud(int usuarioId, int viajeId);
        Task<GenericResponse<bool>> Cancelar(int viajeId);
        Task<GenericResponse<bool>> Iniciar(int viajeId);
        Task<GenericResponse<bool>> Finalizar(int viajeId);*/
    }
}
