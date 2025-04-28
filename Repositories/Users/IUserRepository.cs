using System.Linq.Expressions;
using GestorViajes.Models;
using GestorViajes.Models.EFCore;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;

namespace GestorViajes.Repositories.Users{

    public interface IUserRepository
    {
        Task<GenericResponse<List<User>>> List(Expression<Func<User, bool>>? predicate = null);
        Task<GenericResponse<User>> GetById(long id);
        Task<GenericResponse<User>> Add(User user);
        Task<GenericResponse<User>> Update(User user);
        Task<GenericResponse<bool>> Delete(long id);
    }

}

