/*using GestorViajes.Models.EFCore.GestionTurnos;
using Microsoft.EntityFrameworkCore;
using GestorViajes.Models;

namespace GestorViajes.Models.EFCore
{
    public class GestionTurnosContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public GestionTurnosContext(DbContextOptions<GestionTurnosContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        // DbSets para las entidades
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Cliente> Clientes { get; set; } 
        public DbSet<Conductor> Conductores { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("GestionTurnos");
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)));
            }
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        // Configuración de las relaciones entre entidades
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // para la entidad Viaje
            builder.Entity<Viaje>(b =>
            {
                b.HasKey(v => v.Id);
                b.Property(v => v.FechaInicio).IsRequired();
                b.Property(v => v.FechaFin).IsRequired();
                b.Property(v => v.Destino).HasMaxLength(100).IsRequired();
                b.HasOne(v => v.Cliente)  // Relación con Cliente
                  .WithMany(c => c.Viajes)
                  .HasForeignKey(v => v.ClienteId)
                  .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(v => v.Conductor) // Relación con Conductor
                  .WithMany(c => c.Viajes)
                  .HasForeignKey(v => v.ConductorId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            // para la entidad Vehiculo
            builder.Entity<Vehiculo>(b =>
            {
                b.HasKey(v => v.Id);
                b.Property(v => v.Placa).IsRequired().HasMaxLength(20);
                b.Property(v => v.Modelo).HasMaxLength(50);
                b.Property(v => v.Capacidad).IsRequired();
                b.HasMany(v => v.Viajes)
                  .WithOne(v => v.Vehiculo)
                  .HasForeignKey(v => v.VehiculoId);
            });

            //para la entidad Turno
            builder.Entity<Turno>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.HoraInicio).IsRequired();
                b.Property(t => t.HoraFin).IsRequired();
                b.HasOne(t => t.Vehiculo) // Relación con Vehiculo
                  .WithMany(v => v.Turnos)
                  .HasForeignKey(t => t.VehiculoId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            // para Cliente (si se necesita)
            builder.Entity<Cliente>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
                b.Property(c => c.Direccion).HasMaxLength(150);
                b.HasMany(c => c.Viajes)
                  .WithOne(v => v.Cliente)
                  .HasForeignKey(v => v.ClienteId);
            });

            //para Conductor
            builder.Entity<Conductor>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
                b.Property(c => c.Licencia).HasMaxLength(50).IsRequired();
                b.HasMany(c => c.Viajes)
                  .WithOne(v => v.Conductor)
                  .HasForeignKey(v => v.ConductorId);
            });
        }
    }
}*/
