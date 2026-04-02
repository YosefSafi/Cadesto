using Microsoft.EntityFrameworkCore;
using Cadesto.Data.Entities;

namespace Cadesto.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<House> Houses { get; set; } = null!;
    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<Listing> Listings { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<Invitation> Invitations { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships if needed, although EF conventions often handle simple cases
        modelBuilder.Entity<Unit>()
            .HasOne(u => u.House)
            .WithMany(h => h.Units)
            .HasForeignKey(u => u.HouseId);

        modelBuilder.Entity<Unit>()
            .HasOne(u => u.Listing)
            .WithOne(l => l.Unit)
            .HasForeignKey<Listing>(l => l.UnitId);

        modelBuilder.Entity<Tenant>()
            .HasOne(t => t.Unit)
            .WithMany(u => u.Tenants)
            .HasForeignKey(t => t.UnitId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Image>()
            .HasOne(i => i.House)
            .WithMany(h => h.Images)
            .HasForeignKey(i => i.HouseId);
    }
}
