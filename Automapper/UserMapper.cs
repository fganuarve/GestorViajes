/*using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using GestorViajes.Models.EFCore;
using GestorViajes.Models.ViewModels;

namespace GestorViajes.Automapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(
                uvm => uvm.HasPets,
                u => u.MapFrom(x => x.Pets.Any()))
            .ReverseMap();

            CreateMap<User, CreateUserViewModel>()
                .ForMember(or => or.Id, des => des.MapFrom(x => x.Id))
                .ForMember(or => or.FirstName, des => des.MapFrom(x => x.FirstName))
                .ForMember(or => or.LastName, des => des.MapFrom(x => x.LastName))
                .ForMember(or => or.LastName2, des => des.MapFrom(x => x.LastName2))
                .ForMember(or => or.NationalId, des => des.MapFrom(x => x.NationalId))
                .ForMember(or => or.PhoneNumber, des => des.MapFrom(x => x.PhoneNumber))
                .ForMember(or => or.PhoneNumber2, des => des.MapFrom(x => x.PhoneNumber2))
                .ForMember(or => or.Email, des => des.MapFrom(x => x.Email))


                .ForMember(or => or.Id, des => des.MapFrom(x => x.Id))
                .ForMember(or => or.Id, des => des.MapFrom(x => x.Id))
                .ForMember(or => or.Id, des => des.MapFrom(x => x.Id))
                .ForMember(or => or.Id, des => des.MapFrom(x => x.Id))
                .ForMember(or => or.Id, des => des.MapFrom(x => x.Id))
                .ForMember(or => or.Id, des => des.MapFrom(x => x.Id))
                .ForMember(or => or.Id, des => des.MapFrom(x => x.Id))
                ;

        }
    }
}*/
