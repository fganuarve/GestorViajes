using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.ViewModels.User.Create
{
    public class CreateUserStep2ViewModel
    {
        [MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Number { get; set; } = string.Empty;

        [MaxLength(20)]
        public string City { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Province { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Country { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string? Notes { get; set; }
    }
}
}
