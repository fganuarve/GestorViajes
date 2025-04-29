using AutoMapper;
using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorViajes.Automapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            // De entidad a ViewModel
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName1))
                .ForMember(dest => dest.LastName2, opt => opt.MapFrom(src => src.LastName2))
                .ForMember(dest => dest.SelectedRol, opt => opt.MapFrom(src => src.Role))
                // Se gestiona en el controlador
                .ForMember(dest => dest.Roles, opt => opt.Ignore()) 
                .IncludeBase<CommonFields, CommonFields>();

            // De ViewModel a entidad
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName1, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.LastName2, opt => opt.MapFrom(src => src.LastName2))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.SelectedRol))
                .ForMember(dest => dest.TripRequests, opt => opt.Ignore())
                .ForMember(dest => dest.UserTrips, opt => opt.Ignore())
                .ForMember(dest => dest.Trips, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore())
                .IncludeBase<CommonFields, CommonFields>();
        }
    }
}
