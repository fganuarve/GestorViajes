using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.GestionTurnos;

public partial class peticion_viaje
{
    public long id { get; set; }

    public sbyte? estado { get; set; }

    public long? usuario_id { get; set; }

    public long? viaje_id { get; set; }

    public string? creado_por { get; set; }

    public DateTime? fecha_creacion { get; set; }

    public DateTime? fecha_modificacion { get; set; }

    public string? modificado_por { get; set; }

    public virtual usuarios? usuario { get; set; }

    public virtual viajes? viaje { get; set; }
}
