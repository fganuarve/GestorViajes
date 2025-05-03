using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;
using System.Linq.Expressions;

namespace GestorViajes.Services.Users
{
    public interface IUserService
    {
        Task<GenericResponse<List<UserViewModel>>> List(Expression<Func<User, bool>>? predicate = null);
        Task<GenericResponse<UserViewModel>> Get(Expression<Func<User, bool>> predicate);
        Task<GenericResponse<UserViewModel>> Add(UserViewModel model);
        Task<GenericResponse<UserViewModel>> Update(UserViewModel model);
        Task<GenericResponse<bool>> Delete(long id);
        Task<GenericResponse<User>> AuthenticateUserAsync(string email, string password);
        Task<GenericResponse<bool>> Toggle(long id);

        #region Subscription
        Task<GenericResponse<bool>> ChangeSubscriptionAsync(long id, SubscriptionType newPlan);
        #endregion


        #region Current User
        /// <summary>
        /// Garantizado que existe el usuario si se ha logueado.
        /// </summary>
        /// <returns>Datos completos del usuario logueado</returns>
        Task<User?> CurrentUser();
        #endregion
    }
}
