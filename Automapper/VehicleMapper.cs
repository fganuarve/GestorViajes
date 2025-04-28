using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;

namespace GestorViajes.Automapper
{
    public class VehicleMapper : Profile
    {
        public VehicleMapper()
        {
            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(dest => dest.ModeloCoche, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Matricula, opt => opt.MapFrom(src => src.Plate))
                .ForMember(dest => dest.Plazas, opt => opt.MapFrom(src => src.MaxSeats))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Active))
                // Campos comunes
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.LastUpdateAt, opt => opt.MapFrom(src => src.LastUpdateAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => new UsuarioVehiculoViewModel
                {
                    Id = src.Owner.Id,
                    // solo para incluir datos del usuario
                    Nombre = src.Owner.Name 
                }))
                ;

            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.ModeloCoche))
                .ForMember(dest => dest.Plate, opt => opt.MapFrom(src => src.Matricula))
                .ForMember(dest => dest.MaxSeats, opt => opt.MapFrom(src => src.Plazas ?? 0))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Activo ?? true))
                // Campos comunes
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.LastUpdateAt, opt => opt.MapFrom(src => src.LastUpdateAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
                ;
        }
    }
}
