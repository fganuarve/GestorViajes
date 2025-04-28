using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.ViewModels.User

{
    public class UserViewModel : CommonFields
    {
        
        public long Id { get; set; }

        
        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; } = string.Empty;

        
        [MaxLength(50)]
        public string LastName { get; set; }
        //El segundo apellido va a ser es opcional
        [MaxLength(50)]
        public string? LastName2 { get; set; }

        public string NationalId { get; set; }
        public string Password { get; set; } = null!;


        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
                
        // Rol
        [Required]
        [Display(Name = "Rol")]
        public string SelectedRol { get; set; } = string.Empty;


        // Lista de roles
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
         
    }
}

