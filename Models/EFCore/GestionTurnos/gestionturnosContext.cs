using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace GestorViajes.Models.EFCore.GestionTurnos;

public partial class gestionturnosContext : DbContext
{
    public gestionturnosContext()
    {
    }

    public gestionturnosContext(DbContextOptions<gestionturnosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<peticion_viaje> peticion_viaje { get; set; }

    public virtual DbSet<turnos> turnos { get; set; }

    public virtual DbSet<usuario_viaje> usuario_viaje { get; set; }

    public virtual DbSet<usuarios> usuarios { get; set; }

    public virtual DbSet<vehiculos> vehiculos { get; set; }

    public virtual DbSet<viajes> viajes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=gestionturnos;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<peticion_viaje>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.usuario_id, "FK6tvmb2xim01fr2e6mk0j3y8qj");

            entity.HasIndex(e => e.viaje_id, "FK87bdqjp3vmlv44tscx67sec6l");

            entity.Property(e => e.id).HasColumnType("bigint(20)");
            entity.Property(e => e.creado_por).HasMaxLength(255);
            entity.Property(e => e.estado).HasColumnType("tinyint(4)");
            entity.Property(e => e.fecha_creacion).HasMaxLength(6);
            entity.Property(e => e.fecha_modificacion).HasMaxLength(6);
            entity.Property(e => e.modificado_por).HasMaxLength(255);
            entity.Property(e => e.usuario_id).HasColumnType("bigint(20)");
            entity.Property(e => e.viaje_id).HasColumnType("bigint(20)");

            entity.HasOne(d => d.usuario).WithMany(p => p.peticion_viaje)
                .HasForeignKey(d => d.usuario_id)
                .HasConstraintName("FK6tvmb2xim01fr2e6mk0j3y8qj");

            entity.HasOne(d => d.viaje).WithMany(p => p.peticion_viaje)
                .HasForeignKey(d => d.viaje_id)
                .HasConstraintName("FK87bdqjp3vmlv44tscx67sec6l");
        });

        modelBuilder.Entity<turnos>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.usuario_id, "FKb7mrbbhqfbs65k24il9nc7dl5");

            entity.Property(e => e.id).HasColumnType("bigint(20)");
            entity.Property(e => e.activo).HasColumnType("bit(1)");
            entity.Property(e => e.creado_por).HasMaxLength(255);
            entity.Property(e => e.estado_turno).HasColumnType("enum('EN_CURSO','FINALIZADO','SIN_EMPEZAR')");
            entity.Property(e => e.fecha_creacion).HasMaxLength(6);
            entity.Property(e => e.fecha_modificacion).HasMaxLength(6);
            entity.Property(e => e.hora_fin).HasMaxLength(6);
            entity.Property(e => e.hora_inicio).HasMaxLength(6);
            entity.Property(e => e.modificado_por).HasMaxLength(255);
            entity.Property(e => e.notas_peticion).HasMaxLength(255);
            entity.Property(e => e.peticion_turno).HasColumnType("enum('ACEPTADA','PENDIENTE','RECHAZADA')");
            entity.Property(e => e.usuario_id).HasColumnType("bigint(20)");

            entity.HasOne(d => d.usuario).WithMany(p => p.turnos)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKb7mrbbhqfbs65k24il9nc7dl5");
        });

        modelBuilder.Entity<usuario_viaje>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.usuario_id, "FK5m0tmhop0hybdlqh8qehffprf");

            entity.HasIndex(e => e.viaje_id, "FKmxcia239kt1r5oar1igdyabgv");

            entity.Property(e => e.id).HasColumnType("bigint(20)");
            entity.Property(e => e.usuario_id).HasColumnType("bigint(20)");
            entity.Property(e => e.viaje_id).HasColumnType("bigint(20)");

            entity.HasOne(d => d.usuario).WithMany(p => p.usuario_viaje)
                .HasForeignKey(d => d.usuario_id)
                .HasConstraintName("FK5m0tmhop0hybdlqh8qehffprf");

            entity.HasOne(d => d.viaje).WithMany(p => p.usuario_viaje)
                .HasForeignKey(d => d.viaje_id)
                .HasConstraintName("FKmxcia239kt1r5oar1igdyabgv");
        });

        modelBuilder.Entity<usuarios>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.id).HasColumnType("bigint(20)");
            entity.Property(e => e.activo).HasColumnType("bit(1)");
            entity.Property(e => e.apellido1).HasMaxLength(255);
            entity.Property(e => e.apellido2).HasMaxLength(255);
            entity.Property(e => e.centro_trabajo).HasMaxLength(255);
            entity.Property(e => e.contraseña).HasMaxLength(255);
            entity.Property(e => e.creado_por).HasMaxLength(255);
            entity.Property(e => e.disponibilidad_horas_extras).HasColumnType("bit(1)");
            entity.Property(e => e.email).HasMaxLength(255);
            entity.Property(e => e.fecha_creacion).HasMaxLength(6);
            entity.Property(e => e.fecha_modificacion).HasMaxLength(6);
            entity.Property(e => e.localidad).HasMaxLength(255);
            entity.Property(e => e.modificado_por).HasMaxLength(255);
            entity.Property(e => e.nombre).HasMaxLength(255);
            entity.Property(e => e.preferencias_horarias).HasMaxLength(255);
            entity.Property(e => e.puesto).HasColumnType("tinyint(4)");
            entity.Property(e => e.rol).HasMaxLength(255);
            entity.Property(e => e.telefono).HasMaxLength(255);
        });

        modelBuilder.Entity<vehiculos>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.usuario_id, "FKdhjyx02lnwglmr38720d2bpbo");

            entity.HasIndex(e => e.matricula, "UKwidpc0i9uotdrlq5xxlklr0d").IsUnique();

            entity.Property(e => e.id).HasColumnType("bigint(20)");
            entity.Property(e => e.activo).HasColumnType("bit(1)");
            entity.Property(e => e.creado_por).HasMaxLength(255);
            entity.Property(e => e.fecha_creacion).HasMaxLength(6);
            entity.Property(e => e.fecha_modificacion).HasMaxLength(6);
            entity.Property(e => e.modelo_coche).HasMaxLength(255);
            entity.Property(e => e.modificado_por).HasMaxLength(255);
            entity.Property(e => e.plazas).HasColumnType("int(11)");
            entity.Property(e => e.usuario_id).HasColumnType("bigint(20)");

            entity.HasOne(d => d.usuario).WithMany(p => p.vehiculos)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKdhjyx02lnwglmr38720d2bpbo");
        });

        modelBuilder.Entity<viajes>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.vehiculo_id, "FKgeve1bs56hqqbbvocuufbr89h");

            entity.HasIndex(e => e.usuario_id, "FKgoq4qi4euu9uov10eyjfct3uf");

            entity.Property(e => e.id).HasColumnType("bigint(20)");
            entity.Property(e => e.activo).HasColumnType("bit(1)");
            entity.Property(e => e.creado_por).HasMaxLength(255);
            entity.Property(e => e.destino).HasMaxLength(255);
            entity.Property(e => e.estado).HasColumnType("enum('DISPONIBLE','EN_CURSO','FINALIZADO')");
            entity.Property(e => e.fecha).HasMaxLength(6);
            entity.Property(e => e.fecha_creacion).HasMaxLength(6);
            entity.Property(e => e.fecha_modificacion).HasMaxLength(6);
            entity.Property(e => e.hora).HasMaxLength(6);
            entity.Property(e => e.hora_salida).HasMaxLength(6);
            entity.Property(e => e.modificado_por).HasMaxLength(255);
            entity.Property(e => e.origen).HasMaxLength(255);
            entity.Property(e => e.plazas).HasColumnType("int(11)");
            entity.Property(e => e.usuario_id).HasColumnType("bigint(20)");
            entity.Property(e => e.vehiculo_id).HasColumnType("bigint(20)");

            entity.HasOne(d => d.usuario).WithMany(p => p.viajes)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKgoq4qi4euu9uov10eyjfct3uf");

            entity.HasOne(d => d.vehiculo).WithMany(p => p.viajes)
                .HasForeignKey(d => d.vehiculo_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKgeve1bs56hqqbbvocuufbr89h");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
