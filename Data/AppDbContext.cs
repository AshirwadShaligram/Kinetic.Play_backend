using gamevault_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gamevault_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    // Database Model inport
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Deal> Deals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Deal)
            .WithMany(d=>d.Products)
            .HasForeignKey(p => p.DealId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Deal>()
            .HasOne(d=>d.CreatedByAdmin)
            .WithMany()
            .HasForeignKey(d=>d.CreatedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}