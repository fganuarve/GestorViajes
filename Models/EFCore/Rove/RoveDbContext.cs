using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace GestorViajes.Models.EFCore.Rove
{
    public class RoveDbContext : DbContext
    {
        public RoveDbContext(DbContextOptions<RoveDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

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

        }
    }
}
