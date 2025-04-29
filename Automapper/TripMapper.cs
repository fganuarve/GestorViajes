using AutoMapper;
using GestorViajes.Models;
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
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => new DriverSummaryViewModel
                {
                    Id = src.Driver.Id,
                    FullName = $"{src.Driver.Name} {src.Driver.LastName1} {src.Driver.LastName2}".Trim()
                }))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => new VehicleSummaryViewModel
                {
                    Id = src.Vehicle.Id,
                    Description = $"{src.Vehicle.Plate} - {src.Vehicle.Model}"
                }))
                .IncludeBase<CommonFields, CommonFields>();

            // ViewModel → Entidad
            CreateMap<TripViewModel, Trip>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? 0))
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
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
                .IncludeBase<CommonFields, CommonFields>();

        }
    }
}

