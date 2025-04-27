using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.Rove;

public partial class TripRequest
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long TripId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual User User { get; set; }

    public virtual Trip Trip { get; set; }
}
