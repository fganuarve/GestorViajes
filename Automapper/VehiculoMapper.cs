using AutoMapper;
using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models.ViewModels.Vehiculo;

namespace GestorViajes.Automapper
{
    public class VehiculoMapper : Profile
    {
        public VehiculoMapper()
        {
            CreateMap<vehiculos, VehiculoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Matricula, opt => opt.MapFrom(src => src.matricula ?? string.Empty))
                .ForMember(dest => dest.ModeloCoche, opt => opt.MapFrom(src => src.modelo_coche))
                .ForMember(dest => dest.Plazas, opt => opt.MapFrom(src => src.plazas))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.usuario_id))
                // conversion de ulong de la entidad a bool del viewModel
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.activo == 1UL)) 
                .ReverseMap()
                .ForMember(dest => dest.activo, opt => opt.MapFrom(src => src.Activo == true ? 1UL : 0UL))
                .ForMember(dest => dest.usuario, opt => opt.Ignore()) 
                .ForMember(dest => dest.viajes, opt => opt.Ignore()); 
        }
    }
}
