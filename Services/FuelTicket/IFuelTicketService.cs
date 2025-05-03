using GestorViajes.Models;
using GestorViajes.Models.ViewModels;
using GestorViajes.ViewModels;
using System.Linq.Expressions;

namespace GestorViajes.Services.FuelTicket
{
    public interface IFuelTicketService
    {
        Task<GenericResponse<List<FuelTicketViewModel>>> List(Expression<Func<Models.EFCore.Rove.FuelTicket, bool>>? predicate = null);
        Task<GenericResponse<FuelTicketViewModel>> Get(long id, bool? include = false);
        Task<GenericResponse<FuelTicketViewModel>> Add(FuelTicketViewModel model);
        Task<GenericResponse<FuelTicketViewModel>> Update(FuelTicketViewModel model);
        Task<GenericResponse<bool>> Delete(long id);
        Task<GenericResponse<bool>> Exists(Expression<Func<Models.EFCore.Rove.FuelTicket, bool>> predicate);
    }
}
