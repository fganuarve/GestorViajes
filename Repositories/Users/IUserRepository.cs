using System.Linq.Expressions;
using GestorViajes.Models;
using GestorViajes.Models.EFCore;
using GestorViajes.Models.ViewModels.User;

namespace GestorViajes.Repositories.Users
{
    public interface IUserRepository
    {

        #region User

        Task<GenericResponse<List<User>>> List(Expression<Func<User, bool>>? predicate = null);

        Task<GenericResponse<User>> Add(User user);

        Task<GenericResponse<User>> Edit(User user);

        Task<GenericResponse<User>> Delete(int id);

        Task<GenericResponse<User>> Get(Expression<Func<User, bool>>? predicate = null);

        Task<GenericResponse<bool>> Exists(Expression<Func<User, bool>> predicate);

        #endregion

    }
}

