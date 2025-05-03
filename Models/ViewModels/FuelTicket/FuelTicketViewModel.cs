using System.ComponentModel.DataAnnotations;

namespace GestorViajes.ViewModels
{
    public class FuelTicketViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Por favor añada una descripción del Ticket")]
        [MaxLength(1000, ErrorMessage = "Las notas no pueden exceder los 1000 caracteres.")]
        [Display(Name = "Descripción del ticket")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public string Notes { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(0.01, 1000000, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        [Display(Name = "Monto del ticket")]
        public decimal Amount { get; set; }

        [Display(Name = "Fecha de subida")]
        public DateTime UploadedAt { get; set; } = DateTime.Now;

        [Display(Name = "Imagen del ticket")]
        public IFormFile ImageFile { get; set; }

        // opcional: para mostrar la imagen ya cargada al editar
        public string? Image { get; set; }
    }
}
