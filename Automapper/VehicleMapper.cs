using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;

namespace GestorViajes.Automapper
{
    public class VehicleMapper : Profile
    {
        public VehicleMapper()
        {
            // Entity → ViewModel
            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore());

            // ViewModel → Entity
            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.Trips, opt => opt.Ignore());
        }
    }
}
