using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.ViewModels.User.Create

{
    public class UserViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(50)]
        //Para sobreescribir el nombre de la vista, lo normal es usar un RESX de traduccion
        [Display(Name = "Nombre")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(50)]
        public string? LastName2 { get; set; }

        [MaxLength(20)]
        public string NationalId { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber2 { get; set; }
        public string? Email { get; set; }
        public string SelectedRol { get; set; } = string.Empty;
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
        public bool HasPets { get; set; }
    }
}
