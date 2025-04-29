using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.Rove;

public partial class Vehicle : CommonFields
{
    public long Id { get; set; }

    public bool Active { get; set; }

    public string Plate { get; set; }

    public string? Model { get; set; }

    public int MaxSeats { get; set; }

    public long UserId { get; set; }

    public virtual User Owner { get; set; } 

    public virtual ICollection<Trip> Trips { get; set; } = [];

}
