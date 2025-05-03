using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Trip;

namespace GestorViajes.Automapper
{
    public class TripMapper : Profile
    {
        public TripMapper()
        {
            // Entidad → ViewModel
            CreateMap<Trip, TripViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats))
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Origin))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.TripRequests, opt => opt.MapFrom(src => src.TripRequests))
                .ForMember(dest => dest.Passengers, opt => opt.MapFrom(src => src.Passengers))
                .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.Status.ToString()))
                ;

            // ViewModel → Entidad
            CreateMap<TripViewModel, Trip>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats))
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Origin))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.TripRequests, opt => opt.Ignore())
                .ForMember(dest => dest.Passengers, opt => opt.Ignore())
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore());
        }
    }
}