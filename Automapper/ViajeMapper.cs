using AutoMapper;
using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models.ViewModels.ViajeViewModel;

namespace GestorViajes.Automapper
{
    public class ViajeMapper : Profile
    {
        public ViajeMapper()
        {
            CreateMap<viajes, ViajeViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.VehiculoId, opt => opt.MapFrom(src => src.vehiculo_id))
                .ForMember(dest => dest.ConductorId, opt => opt.MapFrom(src => src.usuario_id))
                .ForMember(dest => dest.Origen, opt => opt.MapFrom(src => src.origen))
                .ForMember(dest => dest.Destino, opt => opt.MapFrom(src => src.destino))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.estado))
                .ForMember(dest => dest.FechaSalida, opt => opt.MapFrom(src => src.fecha_salida.HasValue ? src.fecha_salida.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null))
                .ForMember(dest => dest.HoraSalida, opt => opt.MapFrom(src => src.hora_salida.HasValue ? DateTime.Today.Add(src.hora_salida.Value.ToTimeSpan()) : (DateTime?)null))
                .ForMember(dest => dest.Plazas, opt => opt.MapFrom(src => src.plazas))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.activo.HasValue ? src.activo == 1 : (bool?)null))
                .ForMember(dest => dest.CreadoPor, opt => opt.MapFrom(src => src.creado_por))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.fecha_creacion))
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.fecha_modificacion))
                .ForMember(dest => dest.ModificadoPor, opt => opt.MapFrom(src => src.modificado_por))
                .ForMember(dest => dest.Conductor, opt => opt.Ignore())
                .ForMember(dest => dest.Vehiculo, opt => opt.Ignore())
                .ForMember(dest => dest.Pasajeros, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.vehiculo_id, opt => opt.MapFrom(src => src.VehiculoId))
                .ForMember(dest => dest.usuario_id, opt => opt.MapFrom(src => src.ConductorId))
                .ForMember(dest => dest.origen, opt => opt.MapFrom(src => src.Origen))
                .ForMember(dest => dest.destino, opt => opt.MapFrom(src => src.Destino))
                .ForMember(dest => dest.estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.fecha_salida, opt => opt.Ignore())
                .ForMember(dest => dest.hora_salida, opt => opt.Ignore())
                .ForMember(dest => dest.plazas, opt => opt.MapFrom(src => src.Plazas ?? 0))
                .ForMember(dest => dest.activo, opt => opt.MapFrom(src => src.Activo.HasValue ? (ulong?)(src.Activo.Value ? 1UL : 0UL) : null))
                .ForMember(dest => dest.creado_por, opt => opt.MapFrom(src => src.CreadoPor))
                .ForMember(dest => dest.fecha_creacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.fecha_modificacion, opt => opt.MapFrom(src => src.FechaModificacion))
                .ForMember(dest => dest.modificado_por, opt => opt.MapFrom(src => src.ModificadoPor))
                .ForMember(dest => dest.usuario, opt => opt.Ignore())
                .ForMember(dest => dest.vehiculo, opt => opt.Ignore())
                .ForMember(dest => dest.peticion_viaje, opt => opt.Ignore())
                .ForMember(dest => dest.usuario_viaje, opt => opt.Ignore());
        }
    }
}
