using GestorViajes.Models;
using GestorViajes.Models.EFCore.Rove;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorViajes.Repositories.FuelTickets
{
    public class FuelTicketRepository : IFuelTicketRepository
    {
        private readonly IDbContextFactory<RoveDbContext> _contextFactory;

        public FuelTicketRepository(IDbContextFactory<RoveDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region FuelTicket CRUD

        public async Task<GenericResponse<List<FuelTicket>>> List(Expression<Func<FuelTicket, bool>>? predicate = null)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                var query = context.FuelTickets.AsQueryable();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return new GenericResponse<List<FuelTicket>> { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<FuelTicket>> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<FuelTicket>> Get(long id, bool? include = false)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                IQueryable<FuelTicket> query = context.FuelTickets;
                if (include == true)
                {
                    query = query.Include(t => t.Image).Include(t => t.User);
                }
                var ticket = await query.FirstOrDefaultAsync(t => t.Id == id);
                if (ticket == null)
                {
                    return new GenericResponse<FuelTicket> { Error = new ErrorResponse("Fuel ticket not found.") };
                }

                return new GenericResponse<FuelTicket> { Data = ticket };
            }
            catch (Exception ex)
            {
                return new GenericResponse<FuelTicket> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<FuelTicket>> Add(FuelTicket ticket)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                context.FuelTickets.Add(ticket);
                await context.SaveChangesAsync();

                return new GenericResponse<FuelTicket> { Data = ticket };
            }
            catch (Exception ex)
            {
                return new GenericResponse<FuelTicket> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<FuelTicket>> Update(FuelTicket ticket)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                context.FuelTickets.Update(ticket);
                await context.SaveChangesAsync();

                return new GenericResponse<FuelTicket> { Data = ticket };
            }
            catch (Exception ex)
            {
                return new GenericResponse<FuelTicket> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Delete(long id)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                var ticket = await context.FuelTickets.FindAsync(id);

                if (ticket == null)
                {
                    return new GenericResponse<bool> { Error = new ErrorResponse("Fuel ticket not found.") };
                }

                context.FuelTickets.Remove(ticket);
                await context.SaveChangesAsync();

                return new GenericResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }

        public async Task<GenericResponse<bool>> Exists(Expression<Func<FuelTicket, bool>> predicate)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                var exists = await context.FuelTickets.AnyAsync(predicate);
                return new GenericResponse<bool> { Data = exists };
            }
            catch (Exception ex)
            {
                return new GenericResponse<bool> { Error = new ErrorResponse(ex) };
            }
        }

        #endregion

        #region Image
        public async Task<GenericResponse<FuelTicketImage>> UploadImage(long id, string base64)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();

                var ticket = await context.FuelTickets
                    .Include(t => t.Image)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (ticket == null)
                {
                    return new GenericResponse<FuelTicketImage> { Error = new ErrorResponse("Fuel ticket not found.") };
                }

                // Remove existing image if it exists
                if (ticket.Image != null)
                {
                    context.FuelTicketImage.Remove(ticket.Image);
                }

                // Add new image
                var image = new FuelTicketImage
                {
                    FuelTicketId = id,
                    Image = "data:image/jpeg;base64," + base64
                };
                context.FuelTicketImage.Add(image);

                await context.SaveChangesAsync();

                return new GenericResponse<FuelTicketImage> { Data = image };
            }
            catch (Exception ex)
            {
                return new GenericResponse<FuelTicketImage> { Error = new ErrorResponse(ex) };
            }
        }

        #endregion
    }
}
