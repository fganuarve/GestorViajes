using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.ViewModels.Vehicle
{
    public class VehicleViewModel : CommonFields
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "La matrícula es un campo obligatorio")]
        [Display(Name = "Matrícula")]
        [MaxLength(20)]
        public string Plate { get; set; }
        [Display(Name = "Modelo")]
        public string? Model { get; set; }


        [Required(ErrorMessage = "Debe indicar el número de plazas")]
        [Range(1, 100, ErrorMessage = "Debe ingresar un número válido de plazas")]
        [Display(Name = "Número de Plazas")]
        public int MaxSeats { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public long UserId { get; set; }

        //vehiculo se crea activo por defecto
        [Display(Name = "Active")]
        public bool Active { get; set; } = true;        
    }
}
