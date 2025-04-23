using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.ViewModels.Vehicle
{
    public class VehiculoViewModel : CommonFields
    {
        public long? Id { get; set; }  // nullable por si es nuevo y aun no tiene ID ???

        [Display(Name = "Modelo del Coche")]
        [MaxLength(100)]
        public string? ModeloCoche { get; set; }

        [Required(ErrorMessage = "La matrícula es un campo obligatorio")]
        [MaxLength(20)]
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Debe ingresar un número válido de plazas")]
        [Display(Name = "Número de Plazas")]
        public int? Plazas { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public long UsuarioId { get; set; }

        //vehiculo se crea activo por defecto
        public bool? Activo { get; set; } = true;

        // para mostrar info del usuario asociado
        public UsuarioVehiculoViewModel? Usuario { get; set; }

        // Dropdown de ususario
        public List<SelectListItem> Usuarios { get; set; } = new List<SelectListItem>();
    }

    // version del usuario para incluir dentro del vehiculo
    public class UsuarioVehiculoViewModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
