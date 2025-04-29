using AutoMapper;
using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;

namespace GestorViajes.Automapper
{
    public class VehicleMapper : Profile
    {
        public VehicleMapper()
        {
            // Entidad → ViewModel
            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Plate, opt => opt.MapFrom(src => src.Plate))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.MaxSeats, opt => opt.MapFrom(src => src.MaxSeats))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            // ViewModel → Entidad
            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? 0))
                .ForMember(dest => dest.Plate, opt => opt.MapFrom(src => src.Plate))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.MaxSeats, opt => opt.MapFrom(src => src.MaxSeats))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                // Se carga desde contexto si es necesario
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.Trips, opt => opt.Ignore());
        }
    }
}
