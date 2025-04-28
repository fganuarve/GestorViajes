using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GestorViajes.Repositories.Users;

namespace GestorViajes.Services.User
{

    namespace GestorViajes.Services.User
    {
        public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;
            private readonly RoveDbContext _context;
            private readonly IMapper _mapper;

            public UserService(
                IUserRepository userRepository,
                RoveDbContext context,
                IMapper mapper)
            {
                _userRepository = userRepository;
                _context = context;
                _mapper = mapper;
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
