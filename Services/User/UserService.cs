using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GestorViajes.Repositories.Users;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Castle.Components.DictionaryAdapter.Xml;
using GestorViajes.Models.ViewModels.Trip;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using GestorViajes.Repositories.Vehicles;

namespace GestorViajes.Services.User
{

    namespace GestorViajes.Services.User
    {
        public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;
            private readonly IVehicleRepository _vehicleRepository;
            private readonly RoveDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<UserService> _logger;

            public UserService(
                IUserRepository userRepository,
                RoveDbContext context,
                IMapper mapper,
                ILogger<UserService> logger,
                IVehicleRepository vehicleRepository)
            {
                _userRepository = userRepository;
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _vehicleRepository = vehicleRepository;
            }
            // Metodo para autenticar usuario
            // en lugar de usar user.Password == password
            //se utiliza una forma mejor de comparar que no haya espacios en la contraseña
            public async Task<bool> AuthenticateUserAsync(LoginViewModel loginModel)
            {
                var user = await _userRepository.GetUserByEmailAsync(loginModel.Email);

                if (user == null)
                {
                    _logger.LogWarning($"Intento de login fallido: el usuario con email {loginModel.Email} no existe.");
                    return false;
                }

                if (!string.Equals(user.Password?.Trim(), loginModel.Password?.Trim(), StringComparison.Ordinal))
                {
                    _logger.LogWarning($"Intento de login fallido: contraseña incorrecta para el usuario {loginModel.Email}.");
                    return false;
                }

                // Usuario autenticado
                return true;
            }

            public async Task<Models.EFCore.Rove.User?> GetUserByCredentialsAsync(string email, string password)
            {
                var user = await _userRepository.GetUserByEmailAsync(email);

                if (user != null && string.Equals(user.Password?.Trim(), password?.Trim(), StringComparison.Ordinal))
                {
                    return user;
                }

                return null;
            }
            //Listar usuarios
            //no sirve para poblar un dropdown de conductores
            public async Task<GenericResponse<List<UserViewModel>>> List(Expression<Func<Models.EFCore.Rove.User, bool>>? predicate = null)
            {
                var response = new GenericResponse<List<UserViewModel>>();
                try
                {
                    var result = await _userRepository.List(predicate);
                    if (result.Error != null)
                    {
                        response.Error = result.Error;
                        return response;
                    }

                    response.Data = _mapper.Map<List<UserViewModel>>(result.Data);
                }
                catch (Exception ex)
                {
                    response.Error = new ErrorResponse(ex);
                }
                return response;
            }                    


            public async Task<UserViewModel?> GetUserByEmailAsync(string email)
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                return user == null ? null : _mapper.Map<UserViewModel>(user);
            }



            public async Task<GenericResponse<UserViewModel>> GetById(long id)
            {
                var response = new GenericResponse<UserViewModel>();
                try
                {
                    var result = await _userRepository.GetById(id);
                    if (result.Error != null)
                    {
                        response.Error = result.Error;
                        return response;
                    }

                    response.Data = _mapper.Map<UserViewModel>(result.Data);
                }
                catch (Exception ex)
                {
                    response.Error = new ErrorResponse(ex);
                }
                return response;
            }

            public async Task<GenericResponse<UserViewModel>> Add(UserViewModel model)
            {
                var response = new GenericResponse<UserViewModel>();
                try
                {
                    // Mapeo del modelo ViewModel a la entidad User
                    var user = _mapper.Map<Models.EFCore.Rove.User>(model);


                    // El rol se preasigno en el viremodel
                    //le digo que es activo a true
                    user.Active = true;
                    user.CreatedBy = "self";
                    user.LastUpdatedBy = "self";

                    // Llamada al repositorio para guardar el usuario
                    var result = await _userRepository.Add(user);

                    if (result.Error != null)
                    {
                        response.Error = result.Error;  // En caso de error
                        return response;
                    }

                    // Mapeo de la entidad User de vuelta a UserViewModel
                    response.Data = _mapper.Map<UserViewModel>(result.Data);
                }
                catch (Exception ex)
                {
                    response.Error = new ErrorResponse(ex);  // Manejo de excepciones
                }
                return response;
            }


            public async Task<GenericResponse<UserViewModel>> Update(UserViewModel model)
            {
                var response = new GenericResponse<UserViewModel>();
                try
                {
                    var user = _mapper.Map<Models.EFCore.Rove.User>(model);

                    var result = await _userRepository.Update(user);
                    if (result.Error != null)
                    {
                        response.Error = result.Error;
                        return response;
                    }

                    response.Data = _mapper.Map<UserViewModel>(result.Data);
                }
                catch (Exception ex)
                {
                    response.Error = new ErrorResponse(ex);
                }
                return response;
            }
            



            //borrado total, solo deberia ser realizado por admin
            public async Task<GenericResponse<bool>> Delete(long id)
            {
                var response = new GenericResponse<bool>();
                try
                {
                    var result = await _userRepository.Delete(id);
                    if (result.Error != null)
                    {
                        response.Error = result.Error;
                        return response;
                    }

                    response.Data = result.Data;
                }
                catch (Exception ex)
                {
                    response.Error = new ErrorResponse(ex);
                }
                return response;
            }

            public async Task<GenericResponse<bool>> Exists(Expression<Func<Models.EFCore.Rove.User, bool>> predicate)
            {
                var response = new GenericResponse<bool>();
                try
                {
                    response.Data = await _context.Users.AnyAsync(predicate);
                }
                catch (Exception ex)
                {
                    response.Error = new ErrorResponse(ex);
                }
                return response;
            }


        }

    }
}
