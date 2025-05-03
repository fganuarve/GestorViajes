namespace GestorViajes.Models.ViewModels.User
{
    public class UserTripViewModel
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public long TripId { get; set; }

        // Para mostrar en vistas
        public string UserFullName { get; set; }
        public string TripSummary { get; set; } 
    }
}
