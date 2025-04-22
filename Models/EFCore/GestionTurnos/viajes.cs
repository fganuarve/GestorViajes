using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.GestionTurnos;

public partial class viajes
{
    public long id { get; set; }

    public string? destino { get; set; }

    public string? estado { get; set; }

    public DateTime? fecha { get; set; }

    public DateTime? hora { get; set; }

    public string? origen { get; set; }

    public int plazas { get; set; }

    public long usuario_id { get; set; }

    public long vehiculo_id { get; set; }

    public DateOnly? fecha_salida { get; set; }

    public TimeOnly? hora_salida { get; set; }

    public string? creado_por { get; set; }

    public DateTime? fecha_creacion { get; set; }

    public DateTime? fecha_modificacion { get; set; }

    public string? modificado_por { get; set; }

    public ulong? activo { get; set; }

    public virtual ICollection<peticion_viaje> peticion_viaje { get; set; } = new List<peticion_viaje>();

    public virtual usuarios usuario { get; set; } = null!;

    public virtual ICollection<usuario_viaje> usuario_viaje { get; set; } = new List<usuario_viaje>();

    public virtual vehiculos vehiculo { get; set; } = null!;
}
