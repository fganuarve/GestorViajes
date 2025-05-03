namespace GestorViajes.Models.EFCore.Rove
{
    public class FuelTicket : CommonFields
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Notes { get; set; }
        public decimal Amount { get; set; }
        public DateTime UploadedAt { get; set; }

        public virtual FuelTicketImage Image { get; set; }
		public virtual User User { get; set; }
	}
}

