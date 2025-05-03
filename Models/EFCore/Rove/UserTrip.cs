using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.Rove;

public partial class UserTrip
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long TripId { get; set; }
    public bool Active { get; set; }

    public virtual User User { get; set; }

    public virtual Trip Trip { get; set; }
}
