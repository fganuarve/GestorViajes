using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace GestorViajes.Models.EFCore.Rove
{
    public class RoveDbContext : DbContext
    {
        private readonly IConfiguration _configuration;


        public RoveDbContext(DbContextOptions<RoveDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
       

        public DbSet<User> Users { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripRequest> TripRequests { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Rove");
                optionsBuilder.UseSqlServer(connectionString);
            }
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.NationalId).IsRequired();
                entity.Property(e => e.LastName1).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Role).IsRequired();

                entity.HasMany(u => u.TripRequests)
                     .WithOne(tr => tr.User)
                     .HasForeignKey(tr => tr.UserId)
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.UserTrips)
                      .WithOne(ut => ut.User)
                      .HasForeignKey(ut => ut.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Vehicles)
                      .WithOne(v => v.Owner)
                      .HasForeignKey(v => v.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Trips)
                      .WithOne(t => t.Driver)
                      .HasForeignKey(t => t.DriverId)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Destination).IsRequired();
                entity.Property(e => e.Origin).IsRequired();
                entity.Property(e => e.Seats).IsRequired();
                entity.Property(e => e.Active).IsRequired();

                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.ModifiedAt);
                entity.Property(e => e.ModifiedBy);


                entity.HasOne(t => t.Driver)
                      .WithMany(u => u.Trips)
                      .HasForeignKey(t => t.DriverId)
                      .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(t => t.Vehicle)
                      .WithMany(v => v.Trips)
                      .HasForeignKey(t => t.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);


                entity.HasMany(t => t.TripRequests)
                      .WithOne(tr => tr.Trip)
                      .HasForeignKey(tr => tr.TripId)
                      .OnDelete(DeleteBehavior.Cascade);


                entity.HasMany(t => t.Passengers)
                      .WithOne(ut => ut.Trip)
                      .HasForeignKey(ut => ut.TripId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TripRequest>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.ModifiedAt);
                entity.Property(e => e.ModifiedBy);


                entity.HasOne(tr => tr.User)
                      .WithMany(u => u.TripRequests)
                      .HasForeignKey(tr => tr.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tr => tr.Trip)
                      .WithMany(t => t.TripRequests)
                      .HasForeignKey(tr => tr.TripId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserTrip>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.HasOne(ut => ut.User)
                      .WithMany(u => u.UserTrips)
                      .HasForeignKey(ut => ut.UserId)
                      .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(ut => ut.Trip)
                      .WithMany(t => t.Passengers)
                      .HasForeignKey(ut => ut.TripId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Plate).IsRequired();
                entity.Property(e => e.MaxSeats).IsRequired();
                entity.Property(e => e.Active).IsRequired();

                entity.Property(e => e.Model);
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.ModifiedAt);
                entity.Property(e => e.ModifiedBy);


                entity.HasOne(v => v.Owner)
                      .WithMany(u => u.Vehicles)
                      .HasForeignKey(v => v.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(v => v.Trips)
                      .WithOne(t => t.Vehicle)
                      .HasForeignKey(t => t.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
