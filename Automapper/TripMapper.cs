using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Trip;

namespace GestorViajes.Automapper
{
    public class TripMapper : Profile
    {
        public TripMapper()
        {
            CreateMap<Trip, TripViewModel>()
                .ForMember(dest => dest.ConductorId, opt => opt.MapFrom(src => src.DriverId))
                .ForMember(dest => dest.VehiculoId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.Plazas, opt => opt.MapFrom(src => src.Seats))
                .ForMember(dest => dest.Origen, opt => opt.MapFrom(src => src.Origin))
                .ForMember(dest => dest.Destino, opt => opt.MapFrom(src => src.Destination))
                .ForMember(dest => dest.FechaSalida, opt => opt.MapFrom(src => src.Date.HasValue ? src.Date.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.HoraSalida, opt => opt.MapFrom(src => src.Date.HasValue ? src.Date.Value : (DateTime?)null))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.CreadoPor, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.ModifiedAt))
                .ForMember(dest => dest.ModificadoPor, opt => opt.MapFrom(src => src.ModifiedBy));

            CreateMap<TripViewModel, Trip>()
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.ConductorId))
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehiculoId))
                .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Plazas ?? 0))
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Origen))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destino))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src =>
                    src.FechaSalida.HasValue && src.HoraSalida.HasValue
                        ? new DateTime(src.FechaSalida.Value.Year, src.FechaSalida.Value.Month, src.FechaSalida.Value.Day,
                                       src.HoraSalida.Value.Hour, src.HoraSalida.Value.Minute, 0)
                        : (DateTime?)null))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Activo ?? true))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreadoPor))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModificadoPor));
        }
    }
}

