using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace GestorViajes.Services.Vehicles
{
    public interface IVehicleService
    {
        Task<GenericResponse<List<VehicleViewModel>>> List(Expression<Func<Vehicle, bool>>? predicate = null);
        Task<GenericResponse<VehicleViewModel>> Get(long id);
        Task<GenericResponse<VehicleViewModel>> Add(VehicleViewModel model);
        Task<GenericResponse<VehicleViewModel>> Update(VehicleViewModel model);
		Task<GenericResponse<bool>> Toggle(long id);
		Task<GenericResponse<bool>> Delete(long id);
        //Para exists, trabajo sobre la entidad, no sobre el ViewModel
        Task<GenericResponse<bool>> Exists(Expression<Func<Vehicle, bool>> predicate);
        Task<GenericResponse<List<VehicleViewModel>>> ListByUser(long userId);
        Task<GenericResponse<List<SelectListItem>>> DropdownByUser();
    }
}
