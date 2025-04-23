namespace GestorViajes.Models
{
    public class CommonFields
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public string LastUpdatedBy { get; set; } = string.Empty;
    }
}
