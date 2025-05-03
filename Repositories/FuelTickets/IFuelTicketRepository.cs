using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.FuelTickets
{
    public interface IFuelTicketRepository
    {
        Task<GenericResponse<List<FuelTicket>>> List(Expression<Func<FuelTicket, bool>>? predicate = null);
        Task<GenericResponse<FuelTicket>> Get(long id, bool? include = false);
        Task<GenericResponse<FuelTicket>> Add(FuelTicket ticket);
        Task<GenericResponse<FuelTicket>> Update(FuelTicket ticket);
        Task<GenericResponse<bool>> Delete(long id);
        Task<GenericResponse<bool>> Exists(Expression<Func<FuelTicket, bool>> predicate);
        Task<GenericResponse<FuelTicketImage>> UploadImage(long id, string base64);
    }
}
