using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.Rove;

public partial class User : CommonFields
{
    public long Id { get; set; }
    public string Name { get; set; }

    public string NationalId { get; set; }

    public bool Active { get; set; }

    public string LastName1 { get; set; }

    public string? LastName2 { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public string? PhoneNumber { get; set; }
    //List, no Collection
    public virtual List<TripRequest> TripRequests { get; set; } = [];

    public virtual List<UserTrip> UserTrips { get; set; } = [];

    public virtual List<Vehicle> Vehicles { get; set; } = [];

    public virtual List<Trip> Trips { get; set; } = [];
}
