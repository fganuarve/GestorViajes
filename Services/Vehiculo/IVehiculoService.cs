using GestorViajes.Models.ViewModels.Vehicle;
using System.Linq.Expressions;

namespace GestorViajes.Services.Vehiculo
{
    public interface IVehiculoService
    {
        Task<GenericResponse<List<VehiculoViewModel>>> List(Expression<Func<VehiculoViewModel, bool>>? predicate = null);
        Task<GenericResponse<VehiculoViewModel>> Get(long id);
        Task<GenericResponse<VehiculoViewModel>> Add(VehiculoViewModel model);
        Task<GenericResponse<VehiculoViewModel>> Edit(VehiculoViewModel model);
        Task<GenericResponse<VehiculoViewModel>> Delete(long id);
        Task<GenericResponse<bool>> Exists(Expression<Func<Vehiculo, bool>> predicate);
        Task<GenericResponse<List<VehiculoViewModel>>> ListByUser(long userId);
    }
}
