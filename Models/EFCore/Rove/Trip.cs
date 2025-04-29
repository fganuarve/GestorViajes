using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.Rove;

public partial class Trip : CommonFields
{
    public long Id { get; set; }

    //Conductor
    public long DriverId { get; set; }
    public virtual User Driver { get; set; }

    //Vehiculo

    public long VehicleId { get; set; }

    public int Seats { get; set; }

    public virtual Vehicle Vehicle { get; set; }

    //Viaje
    public string Destination { get; set; }

    public string Origin { get; set; }

    public DateTime? Date { get; set; }    

    public bool Active { get; set; }

    public virtual List<TripRequest> TripRequests { get; set; } = [];    

    public virtual List<UserTrip> Passengers { get; set; } = [];
    
}
