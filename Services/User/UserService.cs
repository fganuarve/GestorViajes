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

namespace GestorViajes.Services.User
{

    namespace GestorViajes.Services.User
    {
        public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;
            private readonly RoveDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<UserService> _logger;

            public UserService(
                IUserRepository userRepository,
                RoveDbContext context,
                IMapper mapper,
                ILogger<UserService> logger)
            {
                _userRepository = userRepository;
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }
            // Metodo para autenticar usuario
            public async Task<bool> AuthenticateUserAsync(LoginViewModel loginModel)
            {
                var user = await _userRepository.GetUserByEmailAsync(loginModel.Email);

                if (user == null)
                {
                    _logger.LogWarning($"Intento de login fallido: el usuario con email {loginModel.Email} no existe.");
                    return false; // Usuario no encontrado
                }

                // Validar contraseña
                if (!VerifyPassword(user.Password, loginModel.Password))
                {
                    _logger.LogWarning($"Intento de login fallido: contraseña incorrecta para el usuario {loginModel.Email}.");
                    return false; // Contraseña incorrecta
                }

                return true; // Autenticacion exitosa
            }

            // Metodo para verificar la contraseña usando un hash (suponiendo que las contraseñas estan almacenadas de forma segura)
            //VerifyPassword es un metodo de apoyo interno y no necesita ser parte de la interfaz IUserService (por eso no aparece en IUserService)
            private bool VerifyPassword(string storedPassword, string inputPassword)
            {
                // Aqui usar el algoritmo de hash que  se utiliza en la base de datos
                // Suponiendo que las contraseñas estan almacenadas como SHA256 por ejemplo
                using (var sha256 = SHA256.Create())
                {
                    var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                    var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                    return storedPassword == hashString;
                }
            }


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
                    var user = _mapper.Map<Models.EFCore.Rove.User>(model);

                    var result = await _userRepository.Add(user);
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
