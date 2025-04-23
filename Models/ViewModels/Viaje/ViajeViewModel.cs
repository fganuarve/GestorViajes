using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.ViewModels.Roadtrip
{
    public class ViajeViewModel : CommonFields
    {
        public long? Id { get; set; }

        [Required]
        [Display(Name = "Vehículo")]
        public long VehiculoId { get; set; }

        [Required]
        [Display(Name = "Conductor")]
        public long ConductorId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Origen")]
        public string? Origen { get; set; }

        [MaxLength(100)]
        [Display(Name = "Destino")]
        public string? Destino { get; set; }

        [Required]
        [Display(Name = "Fecha de Salida")]
        [DataType(DataType.Date)]
        public DateTime? FechaSalida { get; set; }

        [Required]
        [Display(Name = "Hora de Salida")]
        [DataType(DataType.Time)]
        public DateTime? HoraSalida { get; set; }

        [Range(1, 100)]
        [Display(Name = "Número de Plazas")]
        public int? Plazas { get; set; }

        [Display(Name = "Activo")]
        public bool? Activo { get; set; }

        [Display(Name = "Estado")]
        public string? Estado { get; set; } // Alternativamente, puedes usar un enum

        [Display(Name = "Creado Por")]
        public string? CreadoPor { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime? FechaCreacion { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime? FechaModificacion { get; set; }

        [Display(Name = "Modificado Por")]
        public string? ModificadoPor { get; set; }

        // Para selects en el formulario
        public List<SelectListItem> Conductores { get; set; } = new();
        public List<SelectListItem> Vehiculos { get; set; } = new();

        // Datos extra para mostrar
        public UsuarioResumenViewModel? Conductor { get; set; }
        public VehiculoResumenViewModel? Vehiculo { get; set; }
        public List<UsuarioResumenViewModel> Pasajeros { get; set; } = new();
    }

    public class UsuarioResumenViewModel
    {
        public long Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
    }

    public class VehiculoResumenViewModel
    {
        public long Id { get; set; }
        public string Descripcion { get; set; } = string.Empty; // ej. "Ford Fiesta - ABC123"
    }
}
