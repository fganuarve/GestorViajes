using AutoMapper;
using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GestorViajes.Repositories.Users;

namespace GestorViajes.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<List<UserViewModel>>> List(Expression<Func<usuarios, bool>>? predicate = null)
        {
            var response = await _userRepository.List(predicate);

            if (!response.Success)
                return new GenericResponse<List<UserViewModel>>() { Error = response.Error };

            var usersViewModel = _mapper.Map<List<UserViewModel>>(response.Data);

            return new GenericResponse<List<UserViewModel>>() { Data = usersViewModel };
        }

        public async Task<GenericResponse<UserViewModel>> Get(long id)
        {
            var response = await _userRepository.Get(u => u.id == id);

            if (!response.Success)
                return new GenericResponse<UserViewModel>() { Error = response.Error };

            if (response.Data == null)
                return new GenericResponse<UserViewModel>() { Error = new ErrorResponse("Usuario no encontrado.") };

            var userViewModel = _mapper.Map<UserViewModel>(response.Data);

            return new GenericResponse<UserViewModel>() { Data = userViewModel };
        }

        public async Task<GenericResponse<UserViewModel>> Add(UserViewModel model)
        {
            var userEntity = _mapper.Map<usuarios>(model);

            var response = await _userRepository.Add(userEntity);

            if (!response.Success)
                return new GenericResponse<UserViewModel>() { Error = response.Error };

            var userMapped = _mapper.Map<UserViewModel>(response.Data);

            return new GenericResponse<UserViewModel>() { Data = userMapped };
        }

        public async Task<GenericResponse<UserViewModel>> Edit(UserViewModel model)
        {
            var userEntity = _mapper.Map<usuarios>(model);

            var response = await _userRepository.Edit(userEntity);

            if (!response.Success)
                return new GenericResponse<UserViewModel>() { Error = response.Error };

            var userMapped = _mapper.Map<UserViewModel>(response.Data);

            return new GenericResponse<UserViewModel>() { Data = userMapped };
        }

        public async Task<GenericResponse<bool>> Delete(long id)
        {
            // Llamar al repositorio
            var response = await _userRepository.Delete(id);

            // Verificamos si fue bien
            if (!response.Success)
                return new GenericResponse<bool>() { Error = response.Error };

            // Si fue exitosa, devolvemos 'true' como Data
            return new GenericResponse<bool>() { Data = true };
        }


        public async Task<GenericResponse<bool>> Exists(Expression<Func<usuarios, bool>> predicate)
        {
            try
            {
                var response = await _userRepository.Exists(predicate);
                return response;
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool>()
                {
                    Error = new ErrorResponse(ex)
                };
            }
        }
    }
}
