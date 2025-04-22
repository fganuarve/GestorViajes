using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.GestionTurnos;

public partial class usuarios
{
    public long id { get; set; }

    public ulong activo { get; set; }

    public string apellido1 { get; set; } = null!;

    public string? apellido2 { get; set; }

    public string? centro_trabajo { get; set; }

    public string contraseña { get; set; } = null!;

    public ulong? disponibilidad_horas_extras { get; set; }

    public string email { get; set; } = null!;

    public string? localidad { get; set; }

    public string nombre { get; set; } = null!;

    public string? preferencias_horarias { get; set; }

    public sbyte? puesto { get; set; }

    public string rol { get; set; } = null!;

    public string? telefono { get; set; }

    public string? creado_por { get; set; }

    public DateTime? fecha_creacion { get; set; }

    public DateTime? fecha_modificacion { get; set; }

    public string? modificado_por { get; set; }

    public virtual ICollection<peticion_viaje> peticion_viaje { get; set; } = new List<peticion_viaje>();

    public virtual ICollection<turnos> turnos { get; set; } = new List<turnos>();

    public virtual ICollection<usuario_viaje> usuario_viaje { get; set; } = new List<usuario_viaje>();

    public virtual ICollection<vehiculos> vehiculos { get; set; } = new List<vehiculos>();

    public virtual ICollection<viajes> viajes { get; set; } = new List<viajes>();
}
