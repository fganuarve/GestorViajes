using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.GestionTurnos;

public partial class usuario_viaje
{
    public long id { get; set; }

    public long? usuario_id { get; set; }

    public long? viaje_id { get; set; }

    public virtual usuarios? usuario { get; set; }

    public virtual viajes? viaje { get; set; }
}
