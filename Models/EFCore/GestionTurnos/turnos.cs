using System;
using System.Collections.Generic;

namespace GestorViajes.Models.EFCore.GestionTurnos;

public partial class turnos
{
    public long id { get; set; }

    public ulong? activo { get; set; }

    public string? creado_por { get; set; }

    public string estado_turno { get; set; } = null!;

    public DateTime? fecha_creacion { get; set; }

    public DateTime? fecha_modificacion { get; set; }

    public DateTime hora_fin { get; set; }

    public DateTime hora_inicio { get; set; }

    public string? modificado_por { get; set; }

    public string? notas_peticion { get; set; }

    public string peticion_turno { get; set; } = null!;

    public long usuario_id { get; set; }

    public virtual usuarios usuario { get; set; } = null!;
}
