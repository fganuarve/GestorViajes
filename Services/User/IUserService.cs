using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;
using System.Linq.Expressions;

namespace GestorViajes.Services.User
{
    public interface IUserService
    {
        Task<GenericResponse<List<UserViewModel>>> List(Expression<Func<Models.EFCore.Rove.User, bool>>? predicate = null);
        Task<GenericResponse<UserViewModel>> GetById(long id);
        Task<GenericResponse<UserViewModel>> Add(UserViewModel model);
        Task<GenericResponse<UserViewModel>> Update(UserViewModel model);
        Task<GenericResponse<bool>> Delete(long id);
        //Para exists, trabajo sobre la entidad, no sobre el ViewModel
        Task<GenericResponse<bool>> Exists(Expression<Func<Models.EFCore.Rove.User, bool>> predicate);
    }
}
