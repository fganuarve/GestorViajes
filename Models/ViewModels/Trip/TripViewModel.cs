using GestorViajes.Models.EFCore.Rove;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.ViewModels.Trip
{
    public class TripViewModel : CommonFields
    {
        public long? Id { get; set; }

        //Conductor

        [Required]
        [Display(Name = "Driver")]
        public long DriverId { get; set; }

        //Vehiculo

        [Required]
        [Display(Name = "Vehicle")]
        public long VehicleId { get; set; }

        [Range(1, 100)]
        [Display(Name = "Seats")]
        public int Seats { get; set; }

        //Viaje

        [MaxLength(100)]
        [Display(Name = "Origin")]
        public string Origin { get; set; }

        [MaxLength(100)]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public List<TripRequest> TripRequests { get; set; } = [];

        [Display(Name = "Status")]
        public string? Status { get; set; }

        //Pasajeros
        //Representa la relacion entre usuarios y viajes en la bd (tabla intermedia UserTrip)
        public  List<UserTrip> Passengers { get; set; } = [];   
       

        // Display info
        public DriverSummaryViewModel? Driver { get; set; }
        public VehicleSummaryViewModel? Vehicle { get; set; }
    }

    public class DriverSummaryViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
    }

    public class VehicleSummaryViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
    }
}
