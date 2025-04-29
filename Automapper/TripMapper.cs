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
            // Entity → ViewModel
            CreateMap<Trip, TripViewModel>()
                .ForMember(dest => dest.Drivers, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore())
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore());

            // ViewModel → Entity
            CreateMap<TripViewModel, Trip>()
                .ForMember(dest => dest.Driver, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
                .ForMember(dest => dest.TripRequests, opt => opt.Ignore())
                .ForMember(dest => dest.Passengers, opt => opt.Ignore());
        }
    }
}

