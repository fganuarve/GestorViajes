using AutoMapper;
using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models.ViewModels.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorViajes.Automapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<usuarios, UserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.nombre))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.apellido1))
                .ForMember(dest => dest.LastName2, opt => opt.MapFrom(src => src.apellido2))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.telefono))
                .ForMember(dest => dest.SelectedRol, opt => opt.MapFrom(src => src.rol))
                .ReverseMap();
        }
    }
}
