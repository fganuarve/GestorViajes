using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.ViewModels.Trip
{
    public class TripViewModel : CommonFields
    {
        public long Id { get; set; }

        //Conductor

        [Required]
        [Display(Name = "Conductor")]
        public long DriverId { get; set; }

        //Vehiculo

        [Required]
        [Display(Name = "Vehiculo")]
        public long VehicleId { get; set; }

        [Range(1, 100)]
        [Display(Name = "Asientos")]
        public int Seats { get; set; }

        //Viaje

        [MaxLength(100)]
        [Display(Name = "Origen")]
        public string Origin { get; set; }

        [MaxLength(100)]
        [Display(Name = "Destino")]
        public string Destination { get; set; }

        [Display(Name = "Fecha")]
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public List<TripRequest> TripRequests { get; set; } = [];

        [Display(Name = "Estado")]
        public string? StatusDescription { get; set; }
        public int Status { get; set; }

        //Pasajeros
        //Representa la relacion entre usuarios y viajes en la bd (tabla intermedia UserTrip)
        public List<UserTrip> Passengers { get; set; } = [];
        public List<SelectListItem> Vehicles { get; set; } = [];
        public VehicleViewModel Vehicle { get; set; }
    }
}
