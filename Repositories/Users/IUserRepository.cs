using System.Linq.Expressions;
using GestorViajes.Models;
using GestorViajes.Models.EFCore;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;

namespace GestorViajes.Repositories.Users
{
    public interface IUserRepository
    {
        //Para login
        //Puede ser nullable:   User?
        //T? — el primer elemento que cumple la condición, o null si no existe.
        Task<GenericResponse<User>> Get(Expression<Func<User, bool>> predicate);
        Task<GenericResponse<List<User>>> List(Expression<Func<User, bool>>? predicate = null);
        Task<GenericResponse<User>> GetCurrentUser(long id);
        Task<GenericResponse<User>> Add(User user);
        Task<GenericResponse<User>> Update(User user);
        Task<GenericResponse<bool>> Delete(long id);
        Task<GenericResponse<bool>> Exists(Expression<Func<User, bool>> predicate);
    }
}
