using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.GestionTurnos;

public partial class vehiculos
{
    public long id { get; set; }

    public ulong activo { get; set; }

    public string? matricula { get; set; }

    public string? modelo_coche { get; set; }

    public int plazas { get; set; }

    public long usuario_id { get; set; }

    public string? creado_por { get; set; }

    public DateTime? fecha_creacion { get; set; }

    public DateTime? fecha_modificacion { get; set; }

    public string? modificado_por { get; set; }

    public virtual usuarios usuario { get; set; } = null!;

    public virtual ICollection<viajes> viajes { get; set; } = new List<viajes>();
}
