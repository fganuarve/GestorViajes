using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.Rove;

public partial class User
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public string NationalId { get; set; }

    public ulong Active { get; set; }

    public string LastName1 { get; set; } = null!;

    public string? LastName2 { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public virtual ICollection<TripRequest> TripRequests { get; set; } = [];

    public virtual ICollection<UserTrip> UserTrips { get; set; } = [];

    public virtual ICollection<Vehicle> Vehicles { get; set; } = [];

    public virtual ICollection<Trip> Trips { get; set; } = [];
}
