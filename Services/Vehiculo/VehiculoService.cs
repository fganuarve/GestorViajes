using GestorViajes.Models;
using GestorViajes.Repositories.Users;
using System.Linq.Expressions;
using GestorViajes.Models.ViewModels.Vehiculo;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Repositories.Vehiculos;
using AutoMapper;

namespace GestorViajes.Services.Vehiculo
{
    public class VehiculoService 
    {
        //private readonly IVehiculoRepository _vehiculoRepository;
        //private readonly IUserRepository _userRepository;
        //private readonly IMapper _mapper;

        //public VehiculoService(IVehiculoRepository vehiculoRepository, IUserRepository userRepository, IMapper mapper)
        //{
        //    _vehiculoRepository = vehiculoRepository;
        //    _userRepository = userRepository;
        //    _mapper = mapper;
        //}

        //public async Task<GenericResponse<List<VehiculoViewModel>>> List(Expression<Func<VehiculoViewModel, bool>>? predicate = null)
        //{
        //    var response = await _vehiculoRepository.List();

        //    if (!response.Success)
        //        return new GenericResponse<List<VehiculoViewModel>> { Error = response.Error };

        //    var result = _mapper.Map<List<VehiculoViewModel>>(response.Data);
        //    return new GenericResponse<List<VehiculoViewModel>> { Data = result };
        //}

        //public async Task<GenericResponse<List<VehiculoViewModel>>> ListByUser(long userId)
        //{
        //    var response = await _vehiculoRepository.List(v => v.usuario_id == userId);

        //    if (!response.Success)
        //        return new GenericResponse<List<VehiculoViewModel>> { Error = response.Error };

        //    var result = _mapper.Map<List<VehiculoViewModel>>(response.Data);
        //    return new GenericResponse<List<VehiculoViewModel>> { Data = result };
        //}

        //public async Task<GenericResponse<VehiculoViewModel>> Get(long id)
        //{
        //    var response = await _vehiculoRepository.Get(v => v.id == id);

        //    if (!response.Success || response.Data == null)
        //        return new GenericResponse<VehiculoViewModel> { Error = response.Error };

        //    var viewModel = _mapper.Map<VehiculoViewModel>(response.Data);
        //    return new GenericResponse<VehiculoViewModel> { Data = viewModel };
        //}

        //public async Task<GenericResponse<VehiculoViewModel>> Add(VehiculoViewModel model)
        //{
        //    var existsResponse = await _vehiculoRepository.Exists(v => v.matricula == model.Matricula);
        //    if (!existsResponse.Success || existsResponse.Data)
        //        return new GenericResponse<VehiculoViewModel> { Error = new ErrorResponse("Ya existe un vehículo con esa matrícula.") };

        //    var userExists = await _userRepository.Get(u => u.id == model.UsuarioId);
        //    if (!userExists.Success || userExists.Data == null)
        //        return new GenericResponse<VehiculoViewModel> { Error = new ErrorResponse("El usuario no existe.") };

        //    var vehiculoEntity = _mapper.Map<vehiculos>(model);
        //    var response = await _vehiculoRepository.Add(vehiculoEntity);

        //    if (!response.Success)
        //        return new GenericResponse<VehiculoViewModel> { Error = response.Error };

        //    var result = _mapper.Map<VehiculoViewModel>(response.Data);
        //    return new GenericResponse<VehiculoViewModel> { Data = result };
        //}

        //public async Task<GenericResponse<bool>> Exists(Expression<Func<vehiculos, bool>> predicate)
        //{
        //    try
        //    {
        //        return await _vehiculoRepository.Exists(predicate);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
        //    }
        //}

        //public async Task<GenericResponse<VehiculoViewModel>> Edit(VehiculoViewModel model)
        //{
        //    var entity = _mapper.Map<vehiculos>(model);
        //    var response = await _vehiculoRepository.Edit(entity);

        //    if (!response.Success)
        //        return new GenericResponse<VehiculoViewModel> { Error = response.Error };

        //    var result = _mapper.Map<VehiculoViewModel>(response.Data);
        //    return new GenericResponse<VehiculoViewModel> { Data = result };
        //}

        //public async Task<GenericResponse<VehiculoViewModel>> Delete(long id)
        //{
        //    var response = await _vehiculoRepository.Delete(id);

        //    if (!response.Success)
        //        return new GenericResponse<VehiculoViewModel> { Error = response.Error };

        //    var viewModel = _mapper.Map<VehiculoViewModel>(response.Data);
        //    return new GenericResponse<VehiculoViewModel> { Data = viewModel };
        //}
    }
}
