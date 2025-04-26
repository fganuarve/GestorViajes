/*using System.Linq.Expressions;
using GestorViajes.Models;
using GestorViajes.Models.EFCore;

namespace GestorViajes.Repositories.Users
{
    public interface IUserRepository
    {
        Task<GenericResponse<List<User>>> List(Expression<Func<User, bool>>? predicate = null);
        Task<GenericResponse<User>> Add(User user);
        Task<GenericResponse<User>> Edit(User user);
        /// <summary>
        /// Performs a soft delete. Also allows a hard delete if the user has no pets attached and the parameter hardDelete is set to true
        /// </summary>
        /// <param name="id">User's ID</param>
        /// <param name="hardDelete">Allows a hard delete</param>
        /// <returns></returns>
        Task<GenericResponse<User>> Delete(string id, bool? hardDelete = null);
        Task<GenericResponse<User>> UserExists(string nationalId);
        Task<GenericResponse<User>> Get(Expression<Func<User, bool>>? predicate = null);
    }
}*/

