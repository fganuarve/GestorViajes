using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;
using System.Linq.Expressions;

namespace GestorViajes.Services.Vehicle
{
    public interface IVehicleService
    {
        Task<GenericResponse<List<VehicleViewModel>>> List();
        Task<GenericResponse<VehicleViewModel>> Get(long id);
        Task<GenericResponse<VehicleViewModel>> Add(VehicleViewModel model);
        Task<GenericResponse<VehicleViewModel>> Update(VehicleViewModel model);
        Task<GenericResponse<bool>> Delete(long id);
        Task<GenericResponse<List<VehicleViewModel>>> ListByUser(long userId);
        //Para exists, trabajo sobre la entidad, no sobre el ViewModel
        Task<GenericResponse<bool>> Exists(Expression<Func<Models.EFCore.Rove.Vehicle, bool>> predicate);
    }
}
