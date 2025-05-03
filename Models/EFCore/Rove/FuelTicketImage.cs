namespace GestorViajes.Models.EFCore.Rove
{
    public class FuelTicketImage
    {
        public long Id { get; set; }
        public long FuelTicketId { get; set; }
        public string Image { get; set; } = string.Empty;

        public virtual FuelTicket FuelTicket { get; set; }
    }
}
