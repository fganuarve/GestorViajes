using System.Linq.Expressions;
using AutoMapper;
using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Trip;
using GestorViajes.Repositories.Trips;

namespace GestorViajes.Services.Trip
{
    public class TripService 
    {
        //private readonly IViajeRepository _viajeRepository;
        //private readonly IMapper _mapper;

        //public ViajeService(IViajeRepository viajeRepository, IMapper mapper)
        //{
        //    _viajeRepository = viajeRepository;
        //    _mapper = mapper;
        //}

        /*public async Task<GenericResponse<List<ViajeViewModel>>> List(Expression<Func<ViajeViewModel, bool>>? predicate = null)
        {
            // TODO: Implementar listado de viajes (posiblemente mapear entidad a ViewModel)
            return new GenericResponse<List<ViajeViewModel>>();
        }

        public async Task<GenericResponse<ViajeViewModel>> Get(int id)
        {
            // TODO: Implementar obtención de un viaje por id
            return new GenericResponse<ViajeViewModel>();
        }

        public async Task<GenericResponse<ViajeViewModel>> Add(CrearViajeViewModel model)
        {
            // TODO: Implementar creación de un nuevo viaje
            return new GenericResponse<ViajeViewModel>();
        }

        public async Task<GenericResponse<ViajeViewModel>> Edit(int id, EditarViajeViewModel model)
        {
            // TODO: Implementar edición de un viaje
            return new GenericResponse<ViajeViewModel>();
        }

        public async Task<GenericResponse<ViajeViewModel>> Delete(int id)
        {
            // TODO: Implementar eliminación de un viaje
            return new GenericResponse<ViajeViewModel>();
        }

        public async Task<GenericResponse<List<ViajeViewModel>>> ListByUser(string userId, string role, EstadoViaje estado)
        {
            // TODO: Implementar listado de viajes por usuario
            return new GenericResponse<List<ViajeViewModel>>();
        }

        public async Task<GenericResponse<List<UsuarioViewModel>>> ListPasajeros(int viajeId)
        {
            // TODO: Implementar listado de pasajeros de un viaje
            return new GenericResponse<List<UsuarioViewModel>>();
        }

        public async Task<GenericResponse<bool>> SolicitarUnirse(int usuarioId, int viajeId)
        {
            // TODO: Implementar solicitud de unirse a un viaje
            return new GenericResponse<bool>();
        }

        public async Task<GenericResponse<bool>> AceptarSolicitud(int usuarioViajeId)
        {
            // TODO: Implementar aceptación de solicitud de pasajero
            return new GenericResponse<bool>();
        }

        public async Task<GenericResponse<bool>> RechazarSolicitud(int usuarioId, int viajeId)
        {
            // TODO: Implementar rechazo de solicitud de pasajero
            return new GenericResponse<bool>();
        }

        public async Task<GenericResponse<bool>> Cancelar(int viajeId)
        {
            // TODO: Implementar cancelación de viaje
            return new GenericResponse<bool>();
        }

        public async Task<GenericResponse<bool>> Iniciar(int viajeId)
        {
            // TODO: Implementar inicio de viaje
            return new GenericResponse<bool>();
        }

        public async Task<GenericResponse<bool>> Finalizar(int viajeId)
        {
            // TODO: Implementar finalización de viaje
            return new GenericResponse<bool>();
        }*/
    }
}



/*public GenericResponse<CrearEditarViajeResponse> CrearViaje(CrearViajeRequest request)
{
    var response = new GenericResponse<CrearEditarViajeResponse>();

    // Validaciones manuales
    if (string.IsNullOrWhiteSpace(request.Origen))
        return response.SetError("El campo 'origen' no puede ser nulo o vacío");

    if (string.IsNullOrWhiteSpace(request.Destino))
        return response.SetError("El campo 'destino' no puede ser nulo o vacío");

    if (request.FechaSalida.Date < DateTime.Today)
        return response.SetError("La fecha de salida no puede ser anterior a la fecha actual");

    int plazas = request.Plazas.GetValueOrDefault();
    if (plazas < 1 || plazas > 3)
        plazas = 3;

    var usuario = _usuarioRepository.FindById(request.IdConductor);
    var vehiculo = _vehiculoRepository.FindById(request.IdVehiculo);

    if (usuario is null || vehiculo is null)
        return response.SetError("Conductor o Vehículo no encontrado");

    var nuevoViaje = _viajeMapper.CrearViajeRequestToViaje(request);
    nuevoViaje.Estado = EstadoViaje.SinEmpezar;
    nuevoViaje.Activo = true;
    nuevoViaje.Plazas = plazas;

    var viajeGuardado = _viajeRepository.Save(nuevoViaje);
    var dto = _viajeMapper.ViajeToCrearEditarViajeResponse(viajeGuardado);

    response.Data = dto;
    return response;
}*/


/*public async Task<GenericResponse<ViajeViewModel>> Edit(ViajeViewModel model)
{
    var entity = _mapper.Map<viajes>(model);

    var response = await _tripRepository.Edit(entity);

    if (!response.Success)
        return new GenericResponse<ViajeViewModel> { Error = response.Error };

    var updatedViewModel = _mapper.Map<ViajeViewModel>(response.Data);
    return new GenericResponse<ViajeViewModel> { Data = updatedViewModel };
}
/*public GenericResponse<CrearEditarViajeResponse> EditarViaje(long id, EditarViajeRequest request)
{
    var response = new GenericResponse<CrearEditarViajeResponse>();

    if (string.IsNullOrWhiteSpace(request.Origen))
        return response.SetError("El campo 'origen' no puede ser nulo o vacío");

    if (string.IsNullOrWhiteSpace(request.Destino))
        return response.SetError("El campo 'destino' no puede ser nulo o vacío");

    if (request.FechaSalida.Date < DateTime.Today)
        return response.SetError("La fecha de salida no puede ser anterior a la fecha actual");

    if (!request.Plazas.HasValue || request.Plazas <= 0)
        return response.SetError("Las plazas deben ser mayores a cero");

    var viaje = _viajeRepository.FindById(id);
    if (viaje is null)
        return response.SetError("Viaje no encontrado");

    if (viaje.Estado != EstadoViaje.SinEmpezar)
        return response.SetError("Solo se pueden editar viajes en estado SIN_EMPEZAR");

    _viajeMapper.UpdateViajeFromEditarRequest(request, viaje);

    var actualizado = _viajeRepository.Save(viaje);
    response.Data = _viajeMapper.ViajeToCrearEditarViajeResponse(actualizado);
    return response;
}*/


/*public async Task<GenericResponse<ViajeViewModel>> Delete(long id)
{
    var response = await _tripRepository.Delete(id);

    if (!response.Success)
        return new GenericResponse<ViajeViewModel> { Error = response.Error };

    var deleted = _mapper.Map<ViajeViewModel>(response.Data);
    return new GenericResponse<ViajeViewModel> { Data = deleted };
}

public async Task<GenericResponse<bool>> Exists(Expression<Func<viajes, bool>> predicate)
{
    try
    {
        var response = await _tripRepository.Exists(predicate);
        return response;
    }
    catch (Exception ex)
    {
        return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
    }
}

public GenericResponse<object> CancelarViaje(long idViaje)
{
    var response = new GenericResponse<object>();

    var viaje = _viajeRepository.FindById(idViaje);
    if (viaje is null)
        return response.SetError("Viaje no encontrado");

    if (viaje.Estado == EstadoViaje.EnCurso)
        return response.SetError("No se puede cancelar un viaje que ya ha comenzado");

    viaje.Estado = EstadoViaje.Cancelado;
    _viajeRepository.Save(viaje);

    var solicitudes = _usuarioViajeRepository.FindByViajeIdAndEstado(idViaje, EstadoUsuarioViaje.Pendiente);
    _usuarioViajeRepository.DeleteAll(solicitudes);

    return response;
}

/* private final IViajeRepository viajeRepository;
 private final IUsuarioRepository usuarioRepository;
 private final IVehiculoRepository vehiculoRepository;
 private final IViajeMapper viajeMapper;
 private final Validator validator;
 private final IUsuarioViajeRepository usuarioViajeRepository;
  private final IUserMapper usuarioMapper;

public ViajeService(
 IViajeRepository viajeRepository,
 IUsuarioRepository usuarioRepository,
 IVehiculoRepository vehiculoRepository,
 IViajeMapper viajeMapper,
 Validator validator,
 IUsuarioViajeRepository usuarioViajeRepository,
 IUserMapper usuarioMapper
)
 {
     this.viajeRepository = viajeRepository;
     this.usuarioRepository = usuarioRepository;
     this.vehiculoRepository = vehiculoRepository;
     this.viajeMapper = viajeMapper;
     this.validator = validator;
     this.usuarioViajeRepository = usuarioViajeRepository;
     this.usuarioMapper = usuarioMapper;
 }

 public GenericResponse<CrearEditarViajeResponse> crearViaje(CrearViajeRequest request)
 {
     GenericResponse<CrearEditarViajeResponse> responseWrapper = new GenericResponse<>();

     // Validación de los campos usando las anotaciones del request
     Set<ConstraintViolation<CrearViajeRequest>> violations = validator.validate(request);
     if (!violations.isEmpty())
     {
         String errorMessage = violations.iterator().next().getMessage();
         responseWrapper.setError(new ErrorResponse(errorMessage));
         return responseWrapper;
     }

     // comprobamos que los campos origen y destino no sean nulos ni vacíos
     if (request.getOrigen() == null || request.getOrigen().trim().isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("El campo 'origen' no puede ser nulo o vacío"));
         return responseWrapper;
     }

     if (request.getDestino() == null || request.getDestino().trim().isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("El campo 'destino' no puede ser nulo o vacío"));
         return responseWrapper;
     }

     // aseguramos de que la fecha de salida no sea anterior a la fecha actual
     if (request.getFechaSalida().isBefore(LocalDate.now()))
     {
         responseWrapper.setError(new ErrorResponse("La fecha de salida no puede ser anterior a la fecha actual"));
         return responseWrapper;
     }

     // Las plazas serán entre 1 y 3
     Integer plazas = request.getPlazas();
     if (plazas == null || plazas < 1 || plazas > 3)
     {
         plazas = 3; // Si no se proporciona o el valor es inválido, asignamos 3 plazas por defecto
     }

     var usuarioOpt = usuarioRepository.findById(request.getIdConductor());
     var vehiculoOpt = vehiculoRepository.findById(request.getIdVehiculo());

     if (usuarioOpt.isEmpty() || vehiculoOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Conductor o Vehículo no encontrado"));
         return responseWrapper;
     }

     Viaje nuevoViaje = viajeMapper.crearViajeRequestToViaje(request);
     // Viaje es disponible y activo por defecto
     nuevoViaje.setEstado(EstadoViaje.SIN_EMPEZAR);
     nuevoViaje.setActivo(true);
     nuevoViaje.setPlazas(plazas); // Establecemos el número de plazas
     Viaje viajeGuardado = viajeRepository.save(nuevoViaje);
     CrearEditarViajeResponse response = viajeMapper.viajeToCrearEditarViajeResponse(viajeGuardado);

     responseWrapper.setData(response);
     return responseWrapper;
 }


 public GenericResponse<CrearEditarViajeResponse> editarViaje(Long id, EditarViajeRequest param)
 {
     GenericResponse<CrearEditarViajeResponse> responseWrapper = new GenericResponse<>();

     // Validación de los campos usando las anotaciones del request
     Set<ConstraintViolation<EditarViajeRequest>> violations = validator.validate(param);
     if (!violations.isEmpty())
     {
         String errorMessage = violations.iterator().next().getMessage();
         responseWrapper.setError(new ErrorResponse(errorMessage));
         return responseWrapper;
     }

     // asegurarse de que los campos origen y destino no sean nulos ni vacíos
     if (param.getOrigen() == null || param.getOrigen().trim().isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("El campo 'origen' no puede ser nulo o vacío"));
         return responseWrapper;
     }

     if (param.getDestino() == null || param.getDestino().trim().isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("El campo 'destino' no puede ser nulo o vacío"));
         return responseWrapper;
     }

     // asegurarse de que la fecha de salida no sea anterior a la fecha actual
     if (param.getFechaSalida().isBefore(LocalDate.now()))
     {
         responseWrapper.setError(new ErrorResponse("La fecha de salida no puede ser anterior a la fecha actual"));
         return responseWrapper;
     }

     // asegurarse de que las plazas sean mayores a cero
     if (param.getPlazas() == null || param.getPlazas() <= 0)
     {
         responseWrapper.setError(new ErrorResponse("Las plazas deben ser mayores a cero"));
         return responseWrapper;
     }

     var viajeOpt = viajeRepository.findById(id);
     if (viajeOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Viaje no encontrado"));
         return responseWrapper;
     }

     Viaje viaje = viajeOpt.get();
     if (viaje.getEstado() != EstadoViaje.SIN_EMPEZAR)
     {
         responseWrapper.setError(new ErrorResponse("Solo se pueden editar viajes en estado DISPONIBLE"));
         return responseWrapper;
     }

     // mapper para actualizar el viaje
     viajeMapper.updateViajeFromEditarRequest(param, viaje);

     Viaje actualizado = viajeRepository.save(viaje);
     responseWrapper.setData(viajeMapper.viajeToCrearEditarViajeResponse(actualizado));
     return responseWrapper;
 }

 // Cancelar viaje
 public GenericResponse<Void> cancelarViaje(Long idViaje)
 {
     GenericResponse<Void> response = new GenericResponse<>();

     var viajeOpt = viajeRepository.findById(idViaje);
     if (viajeOpt.isEmpty())
     {
         response.setError(new ErrorResponse("Viaje no encontrado"));
         return response;
     }

     Viaje viaje = viajeOpt.get();

     // Verificar si el viaje ya está en curso
     // no se puede cancelar un viaje que ya ha comenzado
     if (viaje.getEstado() == EstadoViaje.EN_CURSO)
     {
         response.setError(new ErrorResponse("No se puede cancelar un viaje que ya ha comenzado"));
         return response;
     }

     // Cambiar el estado a CANCELADO, para que no se puedan hacer nuevas solicitudes
     viaje.setEstado(EstadoViaje.CANCELADO);

     // Guardar el viaje cancelado
     viajeRepository.save(viaje);

     // Se eliminan todas las solicitudes pendientes de este viaje
     List<UsuarioViaje> solicitudesPendientes = usuarioViajeRepository.findByViajeIdAndEstado(idViaje, EstadoUsuarioViaje.PENDIENTE);
     usuarioViajeRepository.deleteAll(solicitudesPendientes);

     return response;
 }





 // cambiarEstadoViaje, listarViajes y listarDatosViaje no requieren validator
 //Listar todos los viajes (uso para admin)
 //Lista todos los viajes y hace filtrado por rol (admin) y por estado del viaje
 public GenericResponse<List<CrearEditarViajeResponse>> listarViajes(Long usuarioId, String rol, EstadoViaje estado)
 {
     GenericResponse<List<CrearEditarViajeResponse>> responseWrapper = new GenericResponse<>();
     List<Viaje> viajes;
     // Si el usuario es admin, puede ver todos los viajes
     if ("ADMIN".equalsIgnoreCase(rol))
     {
         viajes = viajeRepository.findAllViajesByEstado(estado);
     }
     else
     {
         // Si es un usuario normal, solo puede ver los viajes de su usuario y filtrados por estado
         viajes = viajeRepository.findViajesByUsuarioAndEstado(usuarioId, estado);
     }

     if (viajes.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("No se encontraron viajes"));
         return responseWrapper;
     }
     // Convertimos la lista de Viaje a CrearEditarViajeResponse usando el Mapper
     List<CrearEditarViajeResponse> listaDTO = viajes.stream()
             // Usamos el Mapper para convertir a CrearEditarViajeResponse
             .map(viajeMapper::viajeToCrearEditarViajeResponse)
     .collect(Collectors.toList());

     responseWrapper.setData(listaDTO);
     return responseWrapper;
 }

 public GenericResponse<ViajeResponse> listarDatosViaje(Long idViaje)
 {
     GenericResponse<ViajeResponse> responseWrapper = new GenericResponse<>();

     var viajeOpt = viajeRepository.findById(idViaje);
     if (viajeOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Viaje no encontrado"));
         return responseWrapper;
     }
     responseWrapper.setData(viajeMapper.viajeToViajeResponse(viajeOpt.get()));
     return responseWrapper;
 }

 public GenericResponse<Void> solicitarUnirseViaje(Long usuarioId, Long viajeId)
 {
     GenericResponse<Void> responseWrapper = new GenericResponse<>();

     // Verificar si el usuario y el viaje existen
     var usuarioOpt = usuarioRepository.findById(usuarioId);
     var viajeOpt = viajeRepository.findById(viajeId);

     if (usuarioOpt.isEmpty() || viajeOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Usuario o Viaje no encontrados"));
         return responseWrapper;
     }

     Viaje viaje = viajeOpt.get();

     // Verificar si el viaje ya está completo
     if (viaje.getPlazas() <= 0)
     {
         viaje.setEstado(EstadoViaje.COMPLETO); // Marcamos el viaje como completo si no hay plazas
         viajeRepository.save(viaje);
         responseWrapper.setError(new ErrorResponse("El viaje ya está completo"));
         return responseWrapper;
     }

     // Verificar si el viaje está cancelado
     if (viaje.getEstado() == EstadoViaje.CANCELADO)
     {
         responseWrapper.setError(new ErrorResponse("El viaje ha sido cancelado y no puedes unirte"));
         return responseWrapper;
     }

     // Verificar si el viaje está en un estado disponible para nuevas solicitudes
     if (viaje.getEstado() != EstadoViaje.SIN_EMPEZAR)
     {
         responseWrapper.setError(new ErrorResponse("El viaje no está disponible para nuevas solicitudes"));
         return responseWrapper;
     }

     // Verificar si el usuario ya ha solicitado unirse a este viaje
     boolean existeSolicitudPendiente = usuarioViajeRepository.existsByUsuarioAndViajeAndEstado(
             usuarioOpt.get(), viajeOpt.get(), EstadoUsuarioViaje.PENDIENTE);
     if (existeSolicitudPendiente)
     {
         responseWrapper.setError(new ErrorResponse("El usuario ya ha solicitado unirse a este viaje"));
         return responseWrapper;
     }

     // Verificar si el usuario ya está asignado a este viaje
     if (usuarioViajeRepository.existsByUsuarioAndViaje(usuarioOpt.get(), viajeOpt.get()))
     {
         responseWrapper.setError(new ErrorResponse("El usuario ya está asignado a este viaje"));
         return responseWrapper;
     }

     // Crear la solicitud para unirse al viaje con estado PENDIENTE
     UsuarioViaje usuarioViaje = new UsuarioViaje();
     usuarioViaje.setUsuario(usuarioOpt.get());
     usuarioViaje.setViaje(viajeOpt.get());
     usuarioViaje.setEstado(EstadoUsuarioViaje.PENDIENTE); // Estado PENDIENTE

     usuarioViajeRepository.save(usuarioViaje);

     return responseWrapper;
 }




 public GenericResponse<Void> aceptarSolicitud(Long usuarioViajeId)
 {
     GenericResponse<Void> responseWrapper = new GenericResponse<>();

     // Buscar la solicitud de UsuarioViaje
     var usuarioViajeOpt = usuarioViajeRepository.findById(usuarioViajeId);
     if (usuarioViajeOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Solicitud de viaje no encontrada"));
         return responseWrapper;
     }

     UsuarioViaje usuarioViaje = usuarioViajeOpt.get();

     // Verificar si la solicitud está en estado PENDIENTE
     if (usuarioViaje.getEstado() != EstadoUsuarioViaje.PENDIENTE)
     {
         responseWrapper.setError(new ErrorResponse("La solicitud ya fue procesada o no está en estado pendiente"));
         return responseWrapper;
     }

     // Verificar si hay plazas disponibles para el usuario
     Viaje viaje = usuarioViaje.getViaje();
     if (viaje.getPlazas() <= 0)
     {
         viaje.setEstado(EstadoViaje.COMPLETO); // Marcamos el viaje como completo si no hay plazas
         viajeRepository.save(viaje);
         responseWrapper.setError(new ErrorResponse("No hay plazas disponibles para aceptar la solicitud"));
         return responseWrapper;
     }

     // Cambiar el estado de la solicitud a ACEPTADA
     usuarioViaje.setEstado(EstadoUsuarioViaje.ACEPTADA);

     // Disminuir las plazas disponibles
     viaje.setPlazas(viaje.getPlazas() - 1);

     // Guardar la actualización de la solicitud y del viaje
     usuarioViajeRepository.save(usuarioViaje);
     viajeRepository.save(viaje);
     return responseWrapper;
 }



 public GenericResponse<Void> rechazarSolicitud(Long usuarioId, Long viajeId)
 {
     GenericResponse<Void> responseWrapper = new GenericResponse<>();

     // Buscar la solicitud de UsuarioViaje en estado PENDIENTE
     var usuarioViajeOpt = usuarioViajeRepository.findByUsuarioIdAndViajeIdAndEstado(
             usuarioId, viajeId, EstadoUsuarioViaje.PENDIENTE);

     if (usuarioViajeOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Solicitud no encontrada o ya procesada"));
         return responseWrapper;
     }

     UsuarioViaje usuarioViaje = usuarioViajeOpt.get();

     // Verificar si la solicitud ya ha sido procesada
     if (usuarioViaje.getEstado() != EstadoUsuarioViaje.PENDIENTE)
     {
         responseWrapper.setError(new ErrorResponse("La solicitud ya fue aceptada o rechazada"));
         return responseWrapper;
     }

     // Cambiar el estado de la solicitud a RECHAZADA
     usuarioViaje.setEstado(EstadoUsuarioViaje.RECHAZADA);

     // Guardar la actualización de la solicitud
     usuarioViajeRepository.save(usuarioViaje);

     // Opcional: Aquí podrías agregar notificación o algún mecanismo adicional si lo deseas.

     return responseWrapper;
 }


 //listar pasajeros de un viaje
 public GenericResponse<List<UsuarioResponse>> listarPasajeros(Long viajeId)
 {
     GenericResponse<List<UsuarioResponse>> responseWrapper = new GenericResponse<>();

     // Verificar si el viaje existe
     var viajeOpt = viajeRepository.findById(viajeId);
     if (viajeOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Viaje no encontrado"));
         return responseWrapper;
     }

     // Buscar los UsuarioViaje con estado ACEPTADA
     List<UsuarioViaje> usuarioViajes = usuarioViajeRepository.findByViajeIdAndEstado(
             viajeId, EstadoUsuarioViaje.ACEPTADA);

     if (usuarioViajes.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("No hay pasajeros en este viaje"));
         return responseWrapper;
     }

     // Mapeamos los usuarios a su respuesta
     List<UsuarioResponse> pasajeros = usuarioViajes.stream()
             .map(uv-> {
         // Verifica si el usuario no es null
         if (uv.getUsuario() != null)
         {
             return usuarioMapper.usuarioToUsuarioResponse(uv.getUsuario());
         }
         else
         {
             return null; // Podrías decidir devolver un default o hacer algo específico con los nulos
         }
     })
     .filter(p->p != null) // Filtramos los nulos
     .collect(Collectors.toList());

     // Enviar respuesta con la lista de pasajeros
     responseWrapper.setData(pasajeros);
     return responseWrapper;
 }

 public GenericResponse<Void> iniciarViaje(Long viajeId)
 {
     GenericResponse<Void> responseWrapper = new GenericResponse<>();

     var viajeOpt = viajeRepository.findById(viajeId);
     if (viajeOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Viaje no encontrado"));
         return responseWrapper;
     }

     Viaje viaje = viajeOpt.get();

     // Solo se puede iniciar un viaje que esté en estado SIN_EMPEZAR
     if (viaje.getEstado() != EstadoViaje.SIN_EMPEZAR)
     {
         responseWrapper.setError(new ErrorResponse("El viaje no está en un estado válido para ser iniciado"));
         return responseWrapper;
     }

     viaje.setEstado(EstadoViaje.EN_CURSO);
     viajeRepository.save(viaje);

     return responseWrapper;
 }

 public GenericResponse<Void> finalizarViaje(Long viajeId)
 {
     GenericResponse<Void> responseWrapper = new GenericResponse<>();

     var viajeOpt = viajeRepository.findById(viajeId);
     if (viajeOpt.isEmpty())
     {
         responseWrapper.setError(new ErrorResponse("Viaje no encontrado"));
         return responseWrapper;
     }

     Viaje viaje = viajeOpt.get();

     // Solo se puede finalizar un viaje que esté en estado EN_PROCESO
     if (viaje.getEstado() != EstadoViaje.EN_CURSO)
     {
         responseWrapper.setError(new ErrorResponse("El viaje no está en un estado válido para ser finalizado"));
         return responseWrapper;
     }

     viaje.setEstado(EstadoViaje.FINALIZADO);
     viajeRepository.save(viaje);

     return responseWrapper;
 }*/