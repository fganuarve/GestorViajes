using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels;
using GestorViajes.ViewModels;

namespace GestorViajes.Automapper
{
    public class FuelTicketMapper : Profile
    {
        public FuelTicketMapper()
        {
            CreateMap<FuelTicket, FuelTicketViewModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.Image));
            CreateMap<FuelTicketViewModel, FuelTicket>();
		}
    }

}
