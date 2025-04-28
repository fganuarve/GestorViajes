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
            // De entidad User a UserViewModel
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName1))
                .ForMember(dest => dest.LastName2, opt => opt.MapFrom(src => src.LastName2))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.SelectedRol, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.NationalId))
                // No exponer password
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                // Roles dinamicos
                .ForMember(dest => dest.Roles, opt => opt.Ignore())    
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                
                .IncludeBase<CommonFields, CommonFields>();

            // De UserViewModel a entidad User
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName1, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.LastName2, opt => opt.MapFrom(src => src.LastName2))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.SelectedRol))
                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.NationalId))
                // No mapear passwords desde ViewModel por seguridad
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                // Activo por defecto
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => (ulong)1))
                .ForMember(dest => dest.TripRequests, opt => opt.Ignore())
                .ForMember(dest => dest.UserTrips, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore())
                .ForMember(dest => dest.Trips, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                // Hereda de Commonfields
                .IncludeBase<CommonFields, CommonFields>();
        }
    }
}
